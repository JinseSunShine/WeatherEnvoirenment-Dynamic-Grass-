Shader "Custom/LeafSwing" {
	Properties {
		_MainTex("Base(RGB)", 2D) = "white"{}
		_Dir("Direction", Vector)= (0,0,0,0)
		_TimeScale("TimeScale", float)=1
	}
	SubShader {
		Tags { "RenderType"="Opaque" "Queue"="Transparent"}
		LOD 100
		
		CGPROGRAM
		#pragma surface surf Lambert vertex:vert alpha 

		sampler2D _MainTex;
		fixed4 _Dir;
		half _TimeScale;

		struct Input {
			// float2 uv_MainTex;
			half2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}

		void vert(inout appdata_full v)
		{
			half dis = distance(v.vertex ,(0,0,0,0));
			half time = _Time.y * _TimeScale;
			v.vertex.xyz += dis*(sin(time)*cos(time*2/3) +1) * _Dir.xyz;
		}

		ENDCG
	} 
	FallBack "Diffuse"
}
