// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "UI/GlassNGUI/StainedBumpDistort"
{
	Properties
	{
		_MainTex ("Base (RGB), Alpha (A)", 2D) = "black" {}

		//
		_Brightness("Brightness", Range(0, 64)) = 1

		_Distortion("Distortion", range(0,512)) = 10
		_Texture("Texture (RGB)", 2D) = "white" {}
		_Normalmap("Normalmap", 2D) = "bump" {}
	}
	
	SubShader
	{
		LOD 200

		Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
		}

		GrabPass
		{
			Name "BASE"
			Tags{ "LightMode" = "Always" }
		}

		Pass
		{
			Cull Off
			Lighting Off
			ZWrite Off
			Fog { Mode Off }
			Offset -1, -1
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag			
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			float4 _MainTex_ST;
	
			struct appdata_t
			{
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
				fixed4 color : COLOR;
			};
	
			struct v2f
			{
				float4 vertex : SV_POSITION;
				half2 texcoord : TEXCOORD0;
				fixed4 color : COLOR;

				float2 uvtex : TEXCOORD1;
				float2 uvbump : TEXCOORD2;

				float4 uvgrab : TEXCOORD3;
			};

			float _Distortion;
			sampler2D _Texture;
			float4 _Texture_ST;
			sampler2D _Normalmap;
			float4 _Normalmap_ST;

			v2f vert (appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color;

				//
				{
#if UNITY_UV_STARTS_AT_TOP
					float scale = -1.0;
#else
					float scale = 1.0;
#endif
					OUT.uvgrab.xy = (float2(OUT.vertex.x, OUT.vertex.y*scale) + OUT.vertex.w) * 0.5;
					OUT.uvgrab.zw = OUT.vertex.zw;

					//float2 uv = OUT.uvgrab.xy * _ScreenParams.xy * 0.00001;
					float2 uv = OUT.texcoord;
					OUT.uvtex = TRANSFORM_TEX(uv, _Texture);
					OUT.uvbump = TRANSFORM_TEX(uv, _Normalmap);
				}

				return OUT;
			}

			sampler2D _GrabTexture;
			float4 _GrabTexture_TexelSize;

			float _Brightness;

			fixed4 frag (v2f IN) : COLOR
			{
				fixed4 color = tex2D(_MainTex, IN.texcoord) * IN.color;

				//
				{
					// calculate perturbed coordinates
					half2 bump = UnpackNormal(tex2D(_Normalmap, IN.uvbump)).rg; // we could optimize this by just reading the x & y without reconstructing the Z
					half2 offset = bump * _Distortion * _GrabTexture_TexelSize.xy;
					IN.uvgrab.xy = offset * IN.uvgrab.z + IN.uvgrab.xy;
					half3 col = tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(IN.uvgrab)).rgb;

					col *= tex2D(_Texture, IN.uvtex).rgb;

					color.rgb *= col.rgb * _Brightness;
				}

				return color;
			}
			ENDCG
		}
	}

	SubShader
	{
		LOD 100

		Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
		}
		
		Pass
		{
			Cull Off
			Lighting Off
			ZWrite Off
			Fog { Mode Off }
			Offset -1, -1
			ColorMask RGB
			Blend SrcAlpha OneMinusSrcAlpha
			ColorMaterial AmbientAndDiffuse
			
			SetTexture [_MainTex]
			{
				Combine Texture * Primary
			}
		}
	}
}
