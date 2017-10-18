Shader "Custom/FloorGood" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_Mask ("Mask RGB", 2D) = "white" {}
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_MainTex1 ("Albedo (RGB)", 2D) = "white" {}
		_MainTex2 ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _Mask;
		sampler2D _MainTex;
		sampler2D _MainTex1;
		sampler2D _MainTex2;

		struct Input {
			float2 uv_Mask;
			float2 uv_MainTex;
			float2 uv_MainTex1;
			float2 uv_MainTex2;

		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_CBUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_CBUFFER_END

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 t = tex2D (_MainTex, IN.uv_MainTex);
			fixed4 t1 = tex2D (_MainTex1, IN.uv_MainTex1);
			fixed4 t2 = tex2D (_MainTex2, IN.uv_MainTex2);
			fixed4 c = tex2D (_Mask, IN.uv_Mask);
			fixed4 combined = c.r * t + c.g * t1 + c.b * t2;

			o.Albedo = combined.rgb * _Color;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
