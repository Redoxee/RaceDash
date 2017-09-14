Shader "Custom/FakeHorizon" {
	Properties{
		_MainTex("Texture", 2D) = "white" {}
	}

	SubShader{
		Tags{ "RenderType" = "Opaque" }
		CGPROGRAM

#pragma surface surf Lambert vertex:vert
	
		struct Input {
			float4 pos : SV_POSITION;
			float2 uv_MainTex;
		};

		//float4 _MainTex_ST;
		sampler2D _MainTex;


		void vert(inout appdata_full v, out Input o) {
			float dist = v.vertex.z - _WorldSpaceCameraPos.z;
			v.vertex.y += dist * dist * -.00006;
			o.pos = UnityObjectToClipPos(v.vertex);
			o.uv_MainTex = v.texcoord;
			
		}

		void surf(Input IN, inout SurfaceOutput o) {
			o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
		}

		ENDCG
	}
	Fallback "Diffuse"
}