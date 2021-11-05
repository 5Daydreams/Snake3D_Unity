Shader "Unlit/AppleGeometry"
{
    Properties
    {
        _AppleColor ("Tint", Color) = (1,1,1,1)
        _AppleAxis("Apple Axis", Vector) = (0,1,0,0)
        _FloatTest("_FloatTest", Float) = 1.0
        _FloatTest2("_FloatTest 2", Float) = 1.0
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Opaque" "Queue"="Geometry"
        }

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows vertex:vert addshadow

        float4 _AppleColor;
        float3 _AppleAxis;
        float _FloatTest;
        float _FloatTest2;

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

            _AppleAxis = normalize(_AppleAxis);

            float thing = abs(dot(normalDir, _AppleAxis));

            // float anotherThing = thing * thing;
            float anotherThing = pow(thing,_FloatTest2);
            
            float appleBump = - _FloatTest * anotherThing;
            
            // float inflexValue =  (_FloatTest- _FloatTest2 * );

            v.vertex.xyz += normalDir * appleBump;
            o.vertexNormal = normalDir;
        }

        void surf(Input i, inout SurfaceOutputStandard o)
        {
            o.Albedo = _AppleColor;
        }
        ENDCG
    }
    FallBack "Standard"
}