// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Hidden/UI/GlassNGUI/DisplacementScreen 1"
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

			sampler2D _DispMap;
			float4 _DispMap_ST;

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

				float2 texcoord1 : TEXCOORD2;
				float4 uvgrab : TEXCOORD3;
			};

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

			half4 frag (v2f IN) : COLOR
			{
				// Softness factor
				float2 factor = (float2(1.0, 1.0) - abs(IN.worldPos)) * _ClipArgs0;
			
				// Sample the texture
				half4 color = tex2D(_MainTex, IN.texcoord) * IN.color;
				color.a *= clamp( min(factor.x, factor.y), 0.0, 1.0);


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
