Shader "Custom/Sprite/Alpha Sprite" {
	Properties {
		_Color ("Color", Color) = (0,0,0,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque+10" "Queue" = "Overlay+10" }
		LOD 200
		
		Lighting Off ZTest Always ZWrite Off
		
		Blend SrcAlpha OneMinusSrcAlpha
		
		CGPROGRAM
		#pragma surface surf Sprite

		sampler2D _MainTex;
		float4 _Color;

		struct Input {
			float2 uv_MainTex;
		};
		
		half4 LightingSprite(SurfaceOutput s, half3 lightDir, half atten){
			half4 c;
			c.rgb = s.Albedo;
			c.a = s.Alpha;
			
			return c;
		}

		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Albedo = _Color.rgb;
			o.Alpha = c.a * _Color.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
