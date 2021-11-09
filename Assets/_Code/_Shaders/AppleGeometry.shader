Shader "Unlit/AppleGeometry"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _AppleAxis("Apple Axis", Vector) = (0,1,0,0)
        _BumpIntensity("Bump Intensity", Float) = 1.0
        _BumpCurvature("Bump Curvature", Float) = 1.0
        _LightPower("Light Power", Float) = 1.0
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Opaque" "Queue"="Transparent"
        }
        
        Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows vertex:vert addshadow alpha:fade

        float4 _Color;
        float3 _AppleAxis;
        float _BumpIntensity;
        float _BumpCurvature;
        float _LightPower;

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
            float3 sphereNormalDirection = normalize(v.normal.xyz);

            _AppleAxis = normalize(_AppleAxis.xyz);

            float angleToAppleAxis = dot(sphereNormalDirection, _AppleAxis);
            float unsignedAngleToAppleAxis = abs(angleToAppleAxis);

            float unsignedAngleOnPow = pow(unsignedAngleToAppleAxis,_BumpCurvature);

            float appleBump = - _BumpIntensity * unsignedAngleOnPow;

            v.vertex.xyz += sphereNormalDirection * appleBump;
            
            float3 centerAppleNormal = normalize(float3(- sphereNormalDirection.x,(1 - unsignedAngleToAppleAxis) * sign(angleToAppleAxis),- sphereNormalDirection.z));
            float3 finalNormal = lerp(sphereNormalDirection,centerAppleNormal,unsignedAngleOnPow);
            
            v.normal = finalNormal;
        }

        void surf(Input i, inout SurfaceOutputStandard o)
        {
            o.Albedo = _Color;
            o.Alpha = _Color.a;
        }
        ENDCG
    }
    // FallBack "Standard"
}