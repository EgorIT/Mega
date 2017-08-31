// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "UI/GlassNGUI/MobileBlurUI2"
{
	Properties
	{
		_MainTex ("Base (RGB), Alpha (A)", 2D) = "black" {}

		//
		_Brightness("Brightness", Range(0, 64)) = 1

		_blurTex("blur Texture (RGB)", 2D) = "white" {}

		_Distortion("Distortion", range(0,512)) = 10
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

				float4 uvgrab : TEXCOORD1;
				half2 uvbump : TEXCOORD2;
			};

			float _Distortion;
			sampler2D _Normalmap;
			float4 _Normalmap_ST;


			v2f vert (appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color;

				//
				OUT.uvgrab.xy = (float2(OUT.vertex.x, OUT.vertex.y * _ProjectionParams.x) + OUT.vertex.w) * 0.5;
				OUT.uvgrab.w = OUT.vertex.w;
				OUT.uvgrab.z = 0;

				OUT.uvbump = TRANSFORM_TEX(OUT.texcoord, _Normalmap);

				return OUT;
			}

			float _Brightness;
			sampler2D _blurTex;
			float4 _blurTex_TexelSize;

			fixed4 frag (v2f IN) : COLOR
			{
				fixed4 color = tex2D(_MainTex, IN.texcoord) * IN.color;

				//
				{
					// calculate perturbed coordinates
					half2 bump = UnpackNormal(tex2D(_Normalmap, IN.uvbump)).rg; // we could optimize this by just reading the x & y without reconstructing the Z
					half2 offset = bump * _Distortion * _blurTex_TexelSize.xy;
					IN.uvgrab.xy = offset /** IN.uvgrab.z*/ + IN.uvgrab.xy;
					half3 col = tex2Dproj(_blurTex, UNITY_PROJ_COORD(IN.uvgrab)).rgb;

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
