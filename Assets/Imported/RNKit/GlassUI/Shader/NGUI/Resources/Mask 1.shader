// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Hidden/UI/GlassNGUI/Mask 1"
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

				float4 uvgrab : TEXCOORD2;
			};

			v2f vert (appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.color = IN.color;
				OUT.texcoord = IN.texcoord;
				OUT.worldPos = IN.vertex.xy * _ClipRange0.zw + _ClipRange0.xy;


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

			half4 frag (v2f IN) : COLOR
			{
				// Softness factor
				float2 factor = (float2(1.0, 1.0) - abs(IN.worldPos)) * _ClipArgs0;
			
				// Sample the texture
				half4 color = tex2D(_MainTex, IN.texcoord) * IN.color;
				color.a *= clamp( min(factor.x, factor.y), 0.0, 1.0);


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
