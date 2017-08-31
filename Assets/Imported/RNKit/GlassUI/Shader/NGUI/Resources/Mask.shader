// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "UI/GlassNGUI/Mask"
{
	Properties
	{
		_MainTex ("Base (RGB), Alpha (A)", 2D) = "black" {}

		//
		_Brightness("Brightness", Range(0, 64)) = 1

		_GridSize("Grid Size", range(0.01, 10)) = 2.5
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

				float4 uvgrab : TEXCOORD1;
			};
	
			v2f vert (appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color;


				//
#if UNITY_UV_STARTS_AT_TOP
				float scale = -1.0;
#else
				float scale = 1.0;
#endif
				OUT.uvgrab.xy = (float2(OUT.vertex.x, OUT.vertex.y*scale) + OUT.vertex.w) * 0.5;
				OUT.uvgrab.zw = OUT.vertex.zw;

				return OUT;
			}

			//
			sampler2D _GrabTexture;

			float _Brightness;

			float _GridSize;

			fixed4 frag (v2f IN) : COLOR
			{
				fixed4 color = tex2D(_MainTex, IN.texcoord) * IN.color;

				//
				{
					float4 uvgrab = IN.uvgrab;

					float yuX = uvgrab.x % (_GridSize * _ScreenParams.y * 0.001);
					//uvgrab.x -= yuX * 0.5f;
					uvgrab.x -= yuX - _GridSize * 0.5f;

					float yuY = uvgrab.y % (_GridSize * _ScreenParams.x * 0.001);
					//uvgrab.y -= yuY * 0.5f;
					uvgrab.y -= yuY - _GridSize * 0.5f;

					float3 bcolor = tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(uvgrab)).rgb;

					color.rgb *= bcolor.rgb * _Brightness;
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
