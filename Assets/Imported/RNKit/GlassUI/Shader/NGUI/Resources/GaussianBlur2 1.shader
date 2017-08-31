// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Hidden/UI/GlassNGUI/GaussianBlur2 1"
{
	Properties
	{
		_MainTex ("Base (RGB), Alpha (A)", 2D) = "black" {}

		//
		_Brightness("Brightness", Range(0, 64)) = 1

		_OffsetX("Offset X", range(0.0001, 5)) = 1
		_OffsetY("Offset Y", range(0.0001, 5)) = 1

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

		CGINCLUDE

			#include "UnityCG.cginc"

			float4 _ClipRange0 = float4(0.0, 0.0, 1.0, 1.0);
			float2 _ClipArgs0 = float2(1000.0, 1000.0);

			struct _appdata_t
			{
				float4 vertex : POSITION;
				float4 color    : COLOR;
			};

			struct _v2f
			{
				float4 vertex : POSITION;
				float2 worldPos : TEXCOORD0;
				float4 uvgrab : TEXCOORD1;
				float a : TEXCOORD2;
			};

			_v2f vert(_appdata_t IN)
			{
				_v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);

				OUT.worldPos = IN.vertex.xy * _ClipRange0.zw + _ClipRange0.xy;

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

				half4 color = 1;
				color.rgb = sum;



				// Softness factor
				float2 factor = (float2(1.0, 1.0) - abs(IN.worldPos)) * _ClipArgs0;
				color.a *= clamp(min(factor.x, factor.y), 0.0, 1.0);

				return color;
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

				half4 color = 1;
				color.rgb = sum;

				// Softness factor
				float2 factor = (float2(1.0, 1.0) - abs(IN.worldPos)) * _ClipArgs0;
				color.a *= clamp(min(factor.x, factor.y), 0.0, 1.0);

				return color;
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

			Cull Off
			Lighting Off
			ZWrite Off
			Fog{ Mode Off }
			ColorMask RGB
			Blend SrcAlpha OneMinusSrcAlpha

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

			Cull Off
			Lighting Off
			ZWrite Off
			Fog{ Mode Off }
			ColorMask RGB
			Blend SrcAlpha OneMinusSrcAlpha

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

			Cull Off
			Lighting Off
			ZWrite Off
			Fog{ Mode Off }
			ColorMask RGB
			Blend SrcAlpha OneMinusSrcAlpha

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

			Cull Off
			Lighting Off
			ZWrite Off
			Fog{ Mode Off }
			ColorMask RGB
			Blend SrcAlpha OneMinusSrcAlpha

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
			Offset -1, -1
			Fog { Mode Off }
			ColorMask RGB
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			sampler2D _MainTex;

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
				half2 uvbump : TEXCOORD3;
			};

			float _Distortion;
			sampler2D _Normalmap;
			float4 _Normalmap_ST;

			float4 _GrabTexture_TexelSize;

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

				OUT.uvbump = TRANSFORM_TEX(OUT.texcoord, _Normalmap);

				return OUT;
			}

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
