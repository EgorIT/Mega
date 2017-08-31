// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "UI/Glass/MobileBlurUI2"
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

		_blurTex("blur Texture (RGB)", 2D) = "white" {}

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

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color : COLOR;
				float2 texcoord  : TEXCOORD0;
				float4 worldPosition : TEXCOORD1;

				float4 uvgrab : TEXCOORD2;

				float2 uvbump : TEXCOORD3;
			};

			fixed4 _Color;
			fixed4 _TextureSampleAdd;
			float4 _ClipRect;

			float _Distortion;
			sampler2D _Normalmap;
			float4 _Normalmap_ST;

			v2f vert(appdata_t_ IN)
			{
				v2f OUT;
				UNITY_SETUP_INSTANCE_ID(IN);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
				OUT.worldPosition = IN.vertex;
				OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);

				OUT.texcoord = IN.texcoord;

				OUT.color = IN.color * _Color;


				//
				OUT.uvgrab.xy = (float2(OUT.vertex.x, OUT.vertex.y * _ProjectionParams.x) + OUT.vertex.w) * 0.5;
				OUT.uvgrab.w = OUT.vertex.w;
				OUT.uvgrab.z = 0;

				OUT.uvbump = TRANSFORM_TEX(OUT.texcoord, _Normalmap);

				return OUT;
			}


			//
			float _Brightness;
			sampler2D _MainTex;
			sampler2D _blurTex;
			//float4 _blurTex_TexelSize;

			fixed4 frag(v2f IN) : SV_Target
			{
				half4 color = (tex2D(_MainTex, IN.texcoord) + _TextureSampleAdd) * IN.color;

				color.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);

				#ifdef UNITY_UI_ALPHACLIP
				clip(color.a - 0.001);
				#endif


				//
				{
					// calculate perturbed coordinates
					fixed3 bump = UnpackNormal(tex2D(_Normalmap, IN.uvbump)).rgb; // we could optimize this by just reading the x & y without reconstructing the Z
					half2 offset = bump.rg * _Distortion;// *_ScreenParams.zx;
					IN.uvgrab.xy = offset /** IN.uvgrab.z*/ + IN.uvgrab.xy;
					half3 col = tex2Dproj(_blurTex, UNITY_PROJ_COORD(IN.uvgrab)).rgb;
					
					color.rgb *= col.rgb * _Brightness;
				}
				
				return color;
			}
		ENDCG
		}
	}


	FallBack "UI/Default"
}
