Shader "Unlit/TransparencyPostProcessing"
{
    Properties
    {
        [HideInInspector]_MainTex ("Texture", 2D) = "white" {}
        _DepthOfFieldThreshold ("Depth of Field Thresold", Float) = 1.0
        _AlphaReduction ("Alpha Reduction", Float) = 0.5
    }


    SubShader
    {
        Tags
        {
            "RenderType" = "Transparent" "Queue" = "Transparent"
        }

        Cull Off
        ZWrite Off
        ZTest Always

        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #include "UnityCG.cginc"

            #pragma vertex vert
            #pragma fragment frag

            sampler2D _MainTex;
            sampler2D _CameraDepthTexture;
            float _DepthOfFieldThreshold;
            float _AlphaReduction;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 position : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.position = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_TARGET
            {
                float depth = tex2D(_CameraDepthTexture, i.uv).r;

                depth = Linear01Depth(depth);

                depth = depth * _ProjectionParams.z;

                fixed4 source = tex2D(_MainTex, i.uv);

                // return source color if we're at the skybox
                if (depth >= _ProjectionParams.z)
                    return source;

                float reduceAlpha = step(depth, _DepthOfFieldThreshold);

                // idea: if depth <= Threshold, multiply the alpha by 0.5f or something

                float finalAlpha = 1 - reduceAlpha * _AlphaReduction;
                
                //mix wave into source color
                fixed4 col = fixed4(source.xyz, finalAlpha);

                return col;
            }
            ENDCG
        }
    }
}