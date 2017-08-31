// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "UI/GlassNGUI/DisplacementScreen"
{
	Properties
	{
		_MainTex ("Base (RGB), Alpha (A)", 2D) = "black" {}


		//
		_Brightness("Brightness", Range(0, 64)) = 1

		_DispMap("Displacement Map (RG)", 2D) = "white" {}
		_SpeedX("Speed X", Range(0, 10)) = 0
		_SpeedY("Speed Y", Range(0, 10)) = -1
		_OffsetX("Offset X", Range(0, 10)) = 1
		_OffsetY("Offset Y", Range(0, 10)) = -1
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

			sampler2D _DispMap;
			float4 _DispMap_ST;

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

				float2 texcoord1 : TEXCOORD1;
				float4 uvgrab : TEXCOORD2;
			};
	
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
					OUT.texcoord1 = TRANSFORM_TEX(uv, _DispMap);
				}
				return OUT;
			}

			sampler2D _GrabTexture;
			//float4 _GrabTexture_TexelSize;

			float _Brightness;

			half _OffsetX;
			half _OffsetY;

			half _SpeedX;
			half _SpeedY;

			fixed4 frag (v2f IN) : COLOR
			{
				fixed4 color = tex2D(_MainTex, IN.texcoord) * IN.color;

				//
				{
					//scroll displacement map.
					half2 mapoft = half2(_Time.y * _SpeedX, _Time.y * _SpeedY);

					//get displacement color
					half4 offsetColor = tex2D(_DispMap, IN.texcoord1 + mapoft);

					//get offset
					IN.uvgrab.xy += offsetColor.rg * half2(_OffsetX, _OffsetY) * IN.texcoord1.xy;// *_GrabTexture_TexelSize.xy;

					half3 gtColor = tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(IN.uvgrab)).rgb;

					color.rgb *= gtColor * _Brightness;
					//color.rgb *= gtColor.a;
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
