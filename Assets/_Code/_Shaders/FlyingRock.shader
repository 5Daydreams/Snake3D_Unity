Shader "Custom_Kiyoshi/FlyingRock"
{
    Properties
    {
        _Color1 ("Tint", Color) = (1,1,1,1)
        _NoiseCellSize("Noise-Cell Size", Range(0.001,20)) = 1
        _ExtrusionAmplitude1("Extrusion amp 1", Float) = 1
        _ExtrusionAmplitude2("Extrusion amp 2", Float) = 1
        _NoiseDensity1("Noise Density 1", Float) = 1
        _NoiseDensity2("Noise Density 2", Float) = 1
        _OscillationOffset("Oscillation Offset Value", Vector) = (1,1,1,1)
        _Occlusion("Occlusion", Range(0,1)) = 1
        _Smoothness("Smoothness", Range(0,1)) = 1
        _Metallic("Metallic", Range(0,1)) = 1
        _Debug("Debug", Float) = 1
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Opaque" "Queue"="Geometry"
        }

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows vertex:vert addshadow

        float4 _Color1;
        float _NoiseCellSize;
        float _ExtrusionAmplitude1;
        float _ExtrusionAmplitude2;
        float _NoiseDensity1;
        float _NoiseDensity2;
        float _Debug;
        float _Smoothness;
        float _Metallic;
        float _Occlusion;
        float4 _OscillationOffset;

        struct Input
        {
            float3 worldPos;
            float3 vertexNormal;
        };

        #include "Assets/_Code/_Shaders/Noise.cginc"

        float TimedNoise3dTo1d(float3 vectorValue, float4 scale = 1)
        {
            return PerlinNoise3D(vectorValue + scale.xyz);
        }

        void vert(inout appdata_full v, out Input o)
        {
            UNITY_INITIALIZE_OUTPUT(Input, o);
            o.worldPos = mul(unity_ObjectToWorld, v.vertex);
            // v.normal = normalize(v.normal);

            float3 normalDir = normalize(v.normal.xyz);
            float timedNoise1 = TimedNoise3dTo1d(_NoiseDensity1 * v.vertex );
            float timedNoise2 = TimedNoise3dTo1d(_NoiseDensity2 * v.vertex + _OscillationOffset);

            float wave1 = timedNoise1 * _ExtrusionAmplitude1;
            float wave2 = timedNoise2 * _ExtrusionAmplitude2;

            float waveResult = (wave1 + wave2) / 2;
            
            v.vertex.xyz += normalDir * waveResult;
            v.normal = v.vertex.xyz;
        }

        void surf(Input i, inout SurfaceOutputStandard o)
        {
            o.Albedo = _Color1;
            o.Smoothness = _Smoothness;
            o.Metallic = _Metallic;
            o.Occlusion = _Occlusion;
        }
        ENDCG
    }
    FallBack "Standard"
}