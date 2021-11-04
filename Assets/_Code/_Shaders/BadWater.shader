Shader "Custom_Kiyoshi/Kiyoshi_PerlinVertex"
{
    Properties
    {
        _Color1 ("Tint", Color) = (1,1,1,1)
        _Color2 ("Tint", Color) = (0.5,0.5,0.5, 1)
        _CellSize("Cell Size", Range(0.001,20)) = 1
        _ExtrusionAmplitude1("Extrusion amp 1", Float) = 1
        _ExtrusionAmplitude2("Extrusion amp 2", Float) = 1
        _ExtrusionOffset1("Extrusion offset 1", Float) = 1
        _ExtrusionOffset2("Extrusion offset 2", Float) = 1
        _OscillationSpeed1("Oscillation Speed 1", Vector) = (1,1,1,1)
        _OscillationSpeed2("Oscillation Speed 2", Vector) = (1,1,1,1)
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
        float4 _Color2;
        float _CellSize;
        float _ExtrusionAmplitude1;
        float _ExtrusionAmplitude2;
        float _ExtrusionOffset1;
        float _ExtrusionOffset2;
        float4 _OscillationSpeed1;
        float4 _OscillationSpeed2;

        struct Input
        {
            float3 worldPos;
            float3 vertexNormal;
        };

        #include "Assets/_Code/_Shaders/Noise.cginc"

        float TimedNoise3dTo1d(float3 vectorValue, float4 scale = 1)
        {
            return PerlinNoise3D(vectorValue + scale.xyz * _Time.yyy / 3);
        }

        void vert(inout appdata_full v, out Input o)
        {
            UNITY_INITIALIZE_OUTPUT(Input, o);
            o.worldPos = mul(unity_ObjectToWorld, v.vertex);
            v.normal = normalize(v.normal);

            float3 normalDir = normalize(v.normal.xyz);
            float timedNoise1 = TimedNoise3dTo1d(o.worldPos / _CellSize, _OscillationSpeed1);
            float timedNoise2 = TimedNoise3dTo1d(o.worldPos / _CellSize, _OscillationSpeed2);

            float wave1 = _ExtrusionOffset1 + timedNoise1 * _ExtrusionAmplitude1;
            float wave2 = _ExtrusionOffset2 + timedNoise2 * _ExtrusionAmplitude2;

            float upperCapCheck = dot(normalDir, float3(0,1,0));

            float waveResult = upperCapCheck * (wave1 + wave2) / 2;
            
            v.vertex.xyz += normalDir * waveResult;
            o.vertexNormal = normalDir;
        }

        void surf(Input i, inout SurfaceOutputStandard o)
        {
            float timedNoise1 = TimedNoise3dTo1d(i.worldPos, _OscillationSpeed1/ _CellSize);
            float timedNoise2 = TimedNoise3dTo1d(i.worldPos, _OscillationSpeed2/ _CellSize);

            float wave1 = _ExtrusionOffset1 + timedNoise1 * _ExtrusionAmplitude1;
            float wave2 = _ExtrusionOffset2 + timedNoise2 * _ExtrusionAmplitude2;
            
            float waveResult = (wave1 + wave2) * 0.5f;
            
            o.Albedo = lerp(_Color1,_Color2,waveResult);
        }
        ENDCG
    }
    FallBack "Standard"
}