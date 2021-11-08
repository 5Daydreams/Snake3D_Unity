Shader "Custom_Kiyoshi/FlyingRock"
{
    Properties
    {
        _Color1 ("Tint", Color) = (1,1,1,1)
        _NoiseCellSize("Noise-Cell Size", Range(0.001,20)) = 1
        _ExtrusionAmplitude1("Extrusion amp 1", Float) = 1
        _ExtrusionAmplitude2("Extrusion amp 2", Float) = 1
        _OscillationOffset("Oscillation Offset Value", Vector) = (1,1,1,1)
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
        float4 _OscillationOffset;

        struct Input
        {
            float3 worldPos;
            float3 vertexNormal;
        };

        #include "Assets/_Code/_Shaders/Noise.cginc"

        float TimedNoise3dTo1d(float3 vectorValue, float4 scale = 1)
        {
            return PerlinNoise3D(vectorValue + scale.xyz * _OscillationOffset / 3);
        }

        void vert(inout appdata_full v, out Input o)
        {
            UNITY_INITIALIZE_OUTPUT(Input, o);
            o.worldPos = mul(unity_ObjectToWorld, v.vertex);
            v.normal = normalize(v.normal);

            float3 normalDir = normalize(v.normal.xyz);
            float timedNoise1 = TimedNoise3dTo1d(o.worldPos / _NoiseCellSize);
            float timedNoise2 = TimedNoise3dTo1d(-o.worldPos / _NoiseCellSize);

            float wave1 = timedNoise1 * _ExtrusionAmplitude1;
            float wave2 = timedNoise2 * _ExtrusionAmplitude2;

            float upperCapCheck = dot(normalDir, float3(0,1,0));

            float waveResult = upperCapCheck * (wave1 + wave2) / 2;
            
            v.vertex.xyz += normalDir * waveResult;
            o.vertexNormal = normalDir;
        }

        void surf(Input i, inout SurfaceOutputStandard o)
        {
            o.Albedo = _Color1;
            o.Smoothness = 0.4;
            o.Metallic = 0.3;
        }
        ENDCG
    }
    FallBack "Standard"
}