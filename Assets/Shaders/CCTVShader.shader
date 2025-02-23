Shader "Custom/CCTVShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _EffectStrength ("Effect Strength", Range(0,1)) = 1.0
        _ScanlineIntensity ("Scanline Intensity", Range(0,1)) = 0.5
        _NoiseIntensity ("Noise Intensity", Range(0,1)) = 0.3
        _DistortionAmount ("Distortion", Range(0,1)) = 0.2
        _HighFrequencyDistortion ("High Frequency Distortion", Range(0,1)) = 0.1
        _FlickerSpeed ("Flicker Speed", Range(0,10)) = 5.0
        _TintColor ("Tint Color", Color) = (0.502, 0.588, 0.318, 1.0)
        _RippleProgress ("Ripple Progress", Range(0,1)) = 0.0
        _RippleSize ("Ripple Size", Range(0,2)) = 0.5
        _EdgeDistortion ("Edge Distortion", Range(0,1)) = 0.2
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float _EffectStrength;
            float _ScanlineIntensity;
            float _NoiseIntensity;
            float _DistortionAmount;
            float _HighFrequencyDistortion;
            float _FlickerSpeed;
            float4 _TintColor;
            float _RippleProgress;
            float _RippleSize;
            float _EdgeDistortion;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float rand(float2 co)
            {
                return frac(sin(dot(co.xy, float2(12.9898,78.233))) * 43758.5453);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 origUV = i.uv;
                float2 center = float2(0.5, 0.5);

                // Base sine distortion (lower frequency)
                float sine = sin(origUV.y * 10.0 + _Time.y * 2.0);
                float2 sineOffset = float2(sine * _DistortionAmount * _EffectStrength, 0);

                // Additional high-frequency distortion (more waves, weaker amplitude)
                float highFreqSine = sin(origUV.y * 40.0 + _Time.y * 2.0);
                float2 highFreqOffset = float2(highFreqSine * _HighFrequencyDistortion * _EffectStrength, 0);

                // Combine both distortions (they act "on top" of each other)
                float2 combinedSineOffset = sineOffset + highFreqOffset;

                // Compute edge distortion offset
                float2 edgeDir = origUV - center;
                float edgeDist = length(edgeDir);
                float2 edgeOffset = edgeDir * _EdgeDistortion * (edgeDist * edgeDist);

                // Combine offsets on top of the original coordinates.
                float2 uv = origUV + combinedSineOffset + edgeOffset;

                // Use original UVs for the ripple mask (fixes the timing issue)
                float ripple = smoothstep(_RippleProgress - _RippleSize, _RippleProgress, distance(origUV, center));

                fixed4 col = tex2D(_MainTex, uv);

                // Desaturate via grayscale mix.
                float gray = dot(col.rgb, float3(0.299, 0.587, 0.114));
                col.rgb = lerp(col.rgb, gray.xxx, _EffectStrength);

                // Add scanlines (using original UV)
                float scanline = sin(origUV.y * 800.0) * _ScanlineIntensity;
                col.rgb += scanline * _EffectStrength;

                // Add noise.
                float noise = rand(origUV * _Time.y) * _NoiseIntensity;
                col.rgb += noise * _EffectStrength;

                // Apply flickering.
                float flicker = sin(_Time.y * _FlickerSpeed) * 0.4 + 1.0;
                col.rgb *= lerp(1.0, flicker, _EffectStrength);

                // Apply tint.
                col.rgb *= _TintColor.rgb;

                // Blend between filtered and original based on ripple.
                fixed4 originalCol = tex2D(_MainTex, origUV);
                col = lerp(originalCol, col, ripple);

                return col;
            }
            ENDCG
        }
    }
}
