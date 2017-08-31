// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "UI/Glass/GaussianBlur2"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		
		_StencilComp ("Stencil Comparison", Float) = 8
		_Stencil ("Stencil ID", Float) = 0
		_StencilOp ("Stencil Operation", Float) = 0
		_StencilWriteMask ("Stencil Write Mask", Float) = 255
		_StencilReadMask ("Stencil Read Mask", Float) = 255

		_ColorMask ("Color Mask", Float) = 15

		[Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0


		//
		_Brightness("Brightness", Range(0, 64)) = 1

		_OffsetX("Offset X", range(0.0001, 5)) = 1
		_OffsetY("Offset Y", range(0.0001, 5)) = 1

		_Distortion("Distortion", range(0,512)) = 10
		_Normalmap("Normalmap", 2D) = "bump" {}
	}



	SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			"PreviewType" = "Plane"
			"CanUseSpriteAtlas" = "True"
		}

		Stencil
		{
			Ref[_Stencil]
			Comp[_StencilComp]
			Pass[_StencilOp]
			ReadMask[_StencilReadMask]
			WriteMask[_StencilWriteMask]
		}



		Cull Off
		Lighting Off
		ZWrite Off
		ZTest[unity_GUIZTestMode]
		Blend SrcAlpha OneMinusSrcAlpha
		ColorMask[_ColorMask]



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

				float scale = -_ProjectionParams.x;
/*#if UNITY_UV_STARTS_AT_TOP
				float scale = -1.0;
#else
				float scale = 1.0;
#endif*/
				OUT.uvgrab.xy = (float2(OUT.vertex.x, OUT.vertex.y*scale) + OUT.vertex.w) * 0.5;
				OUT.uvgrab.zw = OUT.vertex.zw;
				OUT.a = IN.color.a;
				return OUT;
			}

			sampler2D _GrabTexture;
			//float4 _GrabTexture_TexelSize;
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
            Tags { "LightMode" = "Always" }
        }
		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
			#pragma multi_compile __ UNITY_UI_ALPHACLIP

			#include "UnityCG.cginc"
			#include "UnityUI.cginc"


			struct appdata_t_
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f_
			{
				float4 vertex   : SV_POSITION;
				fixed4 color : COLOR;
				half2 texcoord  : TEXCOORD0;
				half4 worldPosition : TEXCOORD1;

				half4 uvgrab : TEXCOORD2;

				half2 uvbump : TEXCOORD3;
			};

			fixed4 _Color;
			fixed4 _TextureSampleAdd;
			float4 _ClipRect;

			float _Distortion;
			sampler2D _Normalmap;
			float4 _Normalmap_ST;

			v2f_ vert(appdata_t_ IN)
			{
				v2f_ OUT;
				OUT.worldPosition = IN.vertex;
				OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);

				OUT.texcoord = IN.texcoord;

				#ifdef UNITY_HALF_TEXEL_OFFSET
				OUT.vertex.xy += (_ScreenParams.zw - 1.0)*float2(-1,1);
				#endif

				OUT.color = IN.color * _Color;


				//
				float scale = -_ProjectionParams.x;
/*#if UNITY_UV_STARTS_AT_TOP
				float scale = -1.0;
#else
				float scale = 1.0;
#endif*/
				OUT.uvgrab.xy = (float2(OUT.vertex.x, OUT.vertex.y*scale) + OUT.vertex.w) * 0.5;
				OUT.uvgrab.zw = OUT.vertex.zw;

				OUT.uvbump = TRANSFORM_TEX(OUT.texcoord, _Normalmap);

				return OUT;
			}


			//
			float _Brightness;
			sampler2D _MainTex;
			//float4 _GrabTexture_TexelSize;

			fixed4 frag(v2f_ IN) : SV_Target
			{
				half4 color = (tex2D(_MainTex, IN.texcoord) + _TextureSampleAdd) * IN.color;

				color.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);

				#ifdef UNITY_UI_ALPHACLIP
				clip(color.a - 0.001);
				#endif


				//
				{
					// calculate perturbed coordinates
					half2 bump = UnpackNormal(tex2D(_Normalmap, IN.uvbump)).rg; // we could optimize this by just reading the x & y without reconstructing the Z
					half2 offset = bump * _Distortion;// *_GrabTexture_TexelSize.xy;
					IN.uvgrab.xy = offset /** IN.uvgrab.z*/ + IN.uvgrab.xy;
					half3 col = tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(IN.uvgrab)).rgb;
					
					color.rgb *= col.rgb * _Brightness;
				}

				return color;
			}
		ENDCG
		}
	}


	FallBack "UI/Default"
}
