// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "UI/Glass/MobileHexagonUI"
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


		//
		_Shrink("Shrink", float) = 0.15
		_TileSize("Tile size", Vector) = (60, 36, 0, 0)
		_Angle("Angle", float) = 0.0

		//
		_DispMap("Displacement Map (RG)", 2D) = "white" {}
		_DispMapColor("DispMap Color", Color) = (1,1,1,0.5)

		_SpeedX("Speed X", Range(-10, 10)) = 0
		_SpeedY("Speed Y", Range(-10, 10)) = -1

		_OffsetX("Offset X", Range(-10, 10)) = 4
		_OffsetY("Offset Y", Range(-10, 10)) = -4
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


		//-----------------------------------------------------------------------------------------
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
				float4 worldPosition : TEXCOORD1;

				float4 uvgrab : TEXCOORD2;

				float2 uvDisp : TEXCOORD3;
			};

			fixed4 _Color;
			fixed4 _TextureSampleAdd;
			float4 _ClipRect;

			sampler2D _DispMap;
			float4 _DispMap_ST;

			v2f_ vert(appdata_t_ IN)
			{
				v2f_ OUT;
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

				//
				//OUT.uvHexagon = IN.texcoord;
				//OUT.uvHexagon.y *= _ProjectionParams.x;
				OUT.uvDisp = TRANSFORM_TEX(IN.texcoord, _DispMap);

				return OUT;
			}


			//
			float _Brightness;
			sampler2D _MainTex;
			sampler2D _blurTex;


			float _Shrink;
			float2 _TileSize;
			float _Angle;


			fixed4 _DispMapColor;
			half _SpeedX;
			half _SpeedY;

			half _OffsetX;
			half _OffsetY;

			float2 rotate_point(float cx, float cy, float angle, float2 p)
			{
				float s = sin(angle);
				float c = cos(angle);

				p.x -= cx;
				p.y -= cy;

				float xnew = p.x * c - p.y * s;
				float ynew = p.x * s + p.y * c;

				p.x = xnew + cx;
				p.y = ynew + cy;
				return p;
			}


			fixed4 frag(v2f_ IN) : SV_Target
			{
				//
				float shrink = 1.0 - _Shrink;

				float hexagonAngle = 2.0 * atan(_TileSize.y / _TileSize.x);
				float hexagonWidth = _TileSize.x - (_TileSize.y * 0.5 / tan(hexagonAngle)) * 2.0;
				float hexagonHeight = hexagonWidth * tan((radians(180) - hexagonAngle) * 0.5);

				//float2 posInPixels = IN.vertex.xy;
				float2 posInPixels = IN.texcoord * _ScreenParams.xy;
				float2 posInPixelsRot = rotate_point(0.5 *_ScreenParams.x, 0.5 * _ScreenParams.y, -_Angle, posInPixels);

				float2 blockIndex1 = floor(posInPixelsRot / _TileSize.xy);
				float2 blockIndex2 = round(posInPixelsRot / _TileSize.xy);

				float2 blockCenter1 = blockIndex1 * _TileSize.xy + 0.5 * _TileSize;
				float2 blockCenter2 = blockIndex2 * _TileSize.xy;

				float2 d1 = (blockCenter1 - posInPixelsRot) * 2.0;
				float2 d2 = (blockCenter2 - posInPixelsRot) * 2.0;

				float2 dSquared1 = d1.x * d1.x + d1.y * d1.y;
				float2 dSquared2 = d2.x * d2.x + d2.y * d2.y;

				uint d2IsClosest = step(dSquared2, dSquared1);
				float2 closestCenter = d2IsClosest * blockCenter2 + (1 - d2IsClosest) * blockCenter1;
				float2 farthestCenter = (1.0 - d2IsClosest) * blockCenter2 + (d2IsClosest)* blockCenter1;
				float2 shortestDistance = (d2IsClosest * d2 + (1 - d2IsClosest) * d1);
				float2 shortestDistanceAbs = abs(shortestDistance / _TileSize.xy);

				float2 distToDiagonalVector = abs(shortestDistance / float2(hexagonWidth, hexagonHeight));
				float distToDiagonal = (distToDiagonalVector.x + distToDiagonalVector.y);

				float2 inHexagonHorizontal = 1.0 - length(smoothstep(shrink, shrink + 3.0 / _TileSize.xy, shortestDistanceAbs));
				float inHexagonDiagonal = 1.0 - smoothstep(shrink, shrink + (6.0 / max(_TileSize.x, _TileSize.y)), distToDiagonal);
				float inHexagon = inHexagonHorizontal * inHexagonDiagonal;





				//
				half4 color = (tex2D(_MainTex, IN.texcoord) + _TextureSampleAdd) * IN.color;

				color.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);

				#ifdef UNITY_UI_ALPHACLIP
				clip(color.a - 0.001);
				#endif

				//
				{
					float3 bcolor = tex2Dproj(_blurTex, UNITY_PROJ_COORD(IN.uvgrab)).rgb;

					color.rgb *= bcolor.rgb * _Brightness;
				}


				{
					//scroll displacement map.
					half2 mapoft = half2(_Time.y * _SpeedX, _Time.y * _SpeedY);

					//get displacement color
					half4 offsetColor = tex2D(_DispMap, IN.uvDisp + mapoft);

					//get offset
					IN.uvgrab.xy += offsetColor.xy * half2(_OffsetX, _OffsetY) * IN.uvDisp.xy;// *_GrabTexture_TexelSize.xy;

					half3 dColor = tex2Dproj(_blurTex, UNITY_PROJ_COORD(IN.uvgrab)).rgb * _DispMapColor.rgb * _DispMapColor.a * 2;

					color.rgb = lerp(dColor, color.rgb, inHexagon);
				}

				return color;
			}
		ENDCG
		}
		
	}


	FallBack "UI/Default"
}
