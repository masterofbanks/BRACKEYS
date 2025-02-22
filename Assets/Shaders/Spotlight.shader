Shader "Custom/SpotlightShader" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _HoleCenter ("Hole Center", Vector) = (0.5, 0.5, 0, 0)
        _HoleRadius ("Hole Radius", Float) = 0.1
        _Feather ("Feather", Float) = 0.05
    }
    SubShader {
        Tags { "Queue"="Overlay" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _HoleCenter;
            float _HoleRadius;
            float _Feather;

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                // Compute distance from the hole center (using UVs which should be normalized)
                float dist = distance(i.uv, _HoleCenter.xy);
                // Use smoothstep for a soft edge: fully transparent inside the radius, opaque outside
                float alpha = smoothstep(_HoleRadius, _HoleRadius + _Feather, dist);
                return fixed4(0, 0, 0, alpha);
            }
            ENDCG
        }
    }
}
