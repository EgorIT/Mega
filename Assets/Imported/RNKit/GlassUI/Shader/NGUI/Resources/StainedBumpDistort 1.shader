// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Hidden/UI/GlassNGUI/StainedBumpDistort 1"
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
			Offset -1, -1
			Fog { Mode Off }
			ColorMask RGB
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			sampler2D _MainTex;
			float4 _ClipRange0 = float4(0.0, 0.0, 1.0, 1.0);
			float2 _ClipArgs0 = float2(1000.0, 1000.0);

			struct appdata_t
			{
				float4 vertex : POSITION;
				half4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : POSITION;
				half4 color : COLOR;
				float2 texcoord : TEXCOORD0;
				float2 worldPos : TEXCOORD1;

				float2 uvtex : TEXCOORD2;
				float2 uvbump : TEXCOORD3;

				float4 uvgrab : TEXCOORD4;
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
				OUT.color = IN.color;
				OUT.texcoord = IN.texcoord;
				OUT.worldPos = IN.vertex.xy * _ClipRange0.zw + _ClipRange0.xy;

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

			half4 frag (v2f IN) : COLOR
			{
				// Softness factor
				float2 factor = (float2(1.0, 1.0) - abs(IN.worldPos)) * _ClipArgs0;
			
				// Sample the texture
				half4 color = tex2D(_MainTex, IN.texcoord) * IN.color;
				color.a *= clamp( min(factor.x, factor.y), 0.0, 1.0);


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
