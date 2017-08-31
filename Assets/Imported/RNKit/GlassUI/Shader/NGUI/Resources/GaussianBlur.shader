// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "UI/GlassNGUI/GaussianBlur"
{
	Properties
	{
		_MainTex ("Base (RGB), Alpha (A)", 2D) = "black" {}

		//
		_Brightness("Brightness", Range(0, 64)) = 1

		_OffsetX("Offset X", range(0.0001, 5)) = 1
		_OffsetY("Offset Y", range(0.0001, 5)) = 1
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

		CGINCLUDE

			#include "UnityCG.cginc"

			struct _appdata_t
			{
				float4 vertex : POSITION;
				float4 color    : COLOR;
			};

			struct _v2f
			{
				float4 vertex : POSITION;
				float4 uvgrab : TEXCOORD0;
				float a : TEXCOORD1;
			};

			_v2f vert(_appdata_t IN)
			{
				_v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
#if UNITY_UV_STARTS_AT_TOP
				float scale = -1.0;
#else
				float scale = 1.0;
#endif
				OUT.uvgrab.xy = (float2(OUT.vertex.x, OUT.vertex.y*scale) + OUT.vertex.w) * 0.5;
				OUT.uvgrab.zw = OUT.vertex.zw;
				OUT.a = IN.color.a;
				return OUT;
			}

			sampler2D _GrabTexture;
			//float4 _GrabTexture_TexelSize;
			float _Brightness;
			float _OffsetX;
			float _OffsetY;


			half3 grabPixelY(float4 uvgrab, float weight, float offset)
			{
				uvgrab.y += offset;
				return tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(uvgrab)).rgb * weight;
			}
			half4 _verticalFrag(_v2f IN, float offset) : COLOR
			{
				half3 sum = 0;

				offset *= IN.a;
				sum += grabPixelY(IN.uvgrab, 0.05, -4.0 * offset);
				sum += grabPixelY(IN.uvgrab, 0.09, -3.0 * offset);
				sum += grabPixelY(IN.uvgrab, 0.12, -2.0 * offset);
				sum += grabPixelY(IN.uvgrab, 0.15, -1.0 * offset);
				sum += grabPixelY(IN.uvgrab, 0.18,  0.0 * offset);
				sum += grabPixelY(IN.uvgrab, 0.15, +1.0 * offset);
				sum += grabPixelY(IN.uvgrab, 0.12, +2.0 * offset);
				sum += grabPixelY(IN.uvgrab, 0.09, +3.0 * offset);
				sum += grabPixelY(IN.uvgrab, 0.05, +4.0 * offset);

				half4 c = 1;
				c.rgb = sum;
				return c;
			}


			half3 grabPixelX(float4 uvgrab, float weight, float offset)
			{
				uvgrab.x += offset;
				//uvgrab.x += _GrabTexture_TexelSize.x * offset * _OffsetX;
				return tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(uvgrab)).rgb * weight;
			}
			half4 _horizontalFrag(_v2f IN, float offset) : COLOR
			{
				half3 sum = 0;

				offset *= IN.a;
				sum += grabPixelX(IN.uvgrab, 0.05, -4.0 * offset);
				sum += grabPixelX(IN.uvgrab, 0.09, -3.0 * offset);
				sum += grabPixelX(IN.uvgrab, 0.12, -2.0 * offset);
				sum += grabPixelX(IN.uvgrab, 0.15, -1.0 * offset);
				sum += grabPixelX(IN.uvgrab, 0.18,  0.0 * offset);
				sum += grabPixelX(IN.uvgrab, 0.15, +1.0 * offset);
				sum += grabPixelX(IN.uvgrab, 0.12, +2.0 * offset);
				sum += grabPixelX(IN.uvgrab, 0.09, +3.0 * offset);
				sum += grabPixelX(IN.uvgrab, 0.05, +4.0 * offset);

				half4 c = 1;
				c.rgb = sum;
				return c;
			}
		ENDCG


		//one
		//-----------------------------------------------------------------------------------------
		//Vertical
		GrabPass
		{
			Name "BASE"
			Tags{ "LightMode" = "Always" }
		}
		Pass
		{
			Tags{ "LightMode" = "Always" }

			CGPROGRAM
				#pragma vertex vert
				#pragma fragment verticalFrag
				#pragma fragmentoption ARB_precision_hint_fastest

				half4 verticalFrag(_v2f IN) : COLOR
				{
					return _verticalFrag(IN, _OffsetY * 1.61);
				}
			ENDCG
		}

		//Horizontal
		GrabPass
		{
			Name "BASE"
			Tags{ "LightMode" = "Always" }
		}
		Pass
		{
			Tags{ "LightMode" = "Always" }

			CGPROGRAM
				#pragma vertex vert
				#pragma fragment horizontalFrag
				#pragma fragmentoption ARB_precision_hint_fastest

				half4 horizontalFrag(_v2f IN) : COLOR
				{
					return _horizontalFrag(IN, _OffsetX * 1.61);
				}
			ENDCG
		}




		//two
		//-----------------------------------------------------------------------------------------
		//Vertical
		GrabPass
		{
			Name "BASE"
			Tags{ "LightMode" = "Always" }
		}
		Pass
		{
			Tags{ "LightMode" = "Always" }

			CGPROGRAM
				#pragma vertex vert
				#pragma fragment verticalFrag
				#pragma fragmentoption ARB_precision_hint_fastest

				half4 verticalFrag(_v2f IN) : COLOR
				{
					return _verticalFrag(IN, _OffsetY * 1);
				}
			ENDCG
		}

		//Horizontal
		GrabPass
		{
			Name "BASE"
			Tags{ "LightMode" = "Always" }
		}
		Pass
		{
			Tags{ "LightMode" = "Always" }

			CGPROGRAM
				#pragma vertex vert
				#pragma fragment horizontalFrag
				#pragma fragmentoption ARB_precision_hint_fastest

				half4 horizontalFrag(_v2f IN) : COLOR
				{
					return _horizontalFrag(IN, _OffsetX * 1);
				}
			ENDCG
		}



		//-----------------------------------------------------------------------------------------
		GrabPass
		{
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
				
			fixed4 frag (v2f IN) : COLOR
			{
				fixed4 color = tex2D(_MainTex, IN.texcoord) * IN.color;

				//
				{
					float3 bcolor = tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(IN.uvgrab)).rgb;

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
