// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "UI/Glass/DisplacementScreen"
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


		_DispMap("Displacement Map (RG)", 2D) = "white" {}
		_SpeedX("Speed X", Range(-10, 10)) = 0
		_SpeedY("Speed Y", Range(-10, 10)) = -1
		_OffsetX("Offset X", Range(-10, 10)) = 1
		_OffsetY("Offset Y", Range(-10, 10)) = -1
	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}
		
		Stencil
		{
			Ref [_Stencil]
			Comp [_StencilComp]
			Pass [_StencilOp] 
			ReadMask [_StencilReadMask]
			WriteMask [_StencilWriteMask]
		}

		Cull Off
		Lighting Off
		ZWrite Off
		ZTest [unity_GUIZTestMode]
		Blend SrcAlpha OneMinusSrcAlpha
		ColorMask [_ColorMask]

		GrabPass
		{
			Name "BASE"
			Tags{ "LightMode" = "Always" }
		}

		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
			#include "UnityUI.cginc"

			#pragma multi_compile __ UNITY_UI_ALPHACLIP
			
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				half2 texcoord  : TEXCOORD0;
				float4 worldPosition : TEXCOORD1;

				float2 texcoord1 : TEXCOORD2;
				float4 uvgrab : TEXCOORD3;
			};
			
			fixed4 _Color;
			fixed4 _TextureSampleAdd;
			float4 _ClipRect;

			sampler2D _DispMap;
			float4 _DispMap_ST;

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				UNITY_SETUP_INSTANCE_ID(IN);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
				OUT.worldPosition = IN.vertex;
				OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);

				OUT.texcoord = IN.texcoord;

				OUT.color = IN.color * _Color;


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


			sampler2D _MainTex;

			sampler2D _GrabTexture;
			//float4 _GrabTexture_TexelSize;

			float _Brightness;

			half _OffsetX;
			half _OffsetY;

			half _SpeedX;
			half _SpeedY;


			fixed4 frag(v2f IN) : SV_Target
			{
				half4 color = (tex2D(_MainTex, IN.texcoord) + _TextureSampleAdd) * IN.color;
				
				color.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);
				
				#ifdef UNITY_UI_ALPHACLIP
				clip (color.a - 0.001);
				#endif

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



	FallBack "UI/Default"
}
