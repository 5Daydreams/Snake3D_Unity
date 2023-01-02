Shader "Unlit/Checkerboard"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color1 ("Color 1", Color) = (1,1,1,1)
        _Color2 ("Color 2", Color) = (0,0,0,1)
        _TileSize ("Tile Size", Float) = 1
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Opaque" "Queue" = "Geometry"
        }
        
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _TileSize;
            float4 _Color1;
            float4 _Color2;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float3 chessboard = floor(i.worldPos/ _TileSize);
                float chessboardResult = 2 * frac((chessboard.x + chessboard.y + chessboard.z) * 0.5);

                float4 result = lerp(_Color1,_Color2,chessboardResult);
                
                return result;
            }
            ENDCG
        }
    }
    Fallback "Standard"
}