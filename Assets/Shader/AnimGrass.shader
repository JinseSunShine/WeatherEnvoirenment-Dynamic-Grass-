Shader "Custom/AnimGrass" {
    Properties {
        _MainTex ("Grass Texture", 2D) = "white" {}
        _Force("Force",Vector)=(0,0,0,0)
        _strength("Strength", Range(0.1,1)) = 0.1
        _position("Force Pos", Vector) =(0,0,0,0)
        _maxDis("Max Distance", float) = 3
    }
 
    SubShader{
        Tags{"Queue"="Transparent" "RenderType"="Opaque" "IgnoreProject"="True"}
        Pass{
            Tags{"LightMode"="ForwardBase"}
 
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
 
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
 
            sampler2D _MainTex; sampler2D _MainTex_ST;
            fixed4 _Force;
            fixed4 _position;
            half _strength;
            half _maxDis;
 
            struct a2v {
                fixed4 vertex : POSITION;
                fixed4 texcoord : TEXCOORD0;
            };
             
            struct v2f {
                fixed4 pos : SV_POSITION;
                fixed2 uv : TEXCOORD0;
            };
 
 
            v2f vert(a2v v){
                v2f o;

	            fixed4 offset = fixed4(0,0,0,0);

                half fdis = abs( distance(v.vertex.xz, _position.xz) ) ;
                fdis = 1 - fdis/_maxDis;
                if (fdis < 0)
                {
                    fdis = 0;
                }

	            offset.xyz = _Force.xyz * v.texcoord.y * _strength * fdis; //

                o.pos = mul(UNITY_MATRIX_MVP, v.vertex + offset);
                o.uv = v.texcoord.xy; 
                return o;
            }
        			
            fixed4 frag(v2f i) :SV_Target {
                return tex2D(_MainTex, i.uv);
            }
 
            ENDCG
        }
    }
    FallBack Off
}