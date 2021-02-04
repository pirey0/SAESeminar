Shader "Luca/NewUnlitShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _SecondTexture("Texture2", 2D) = "white" {}
        Alpha ("Alpha", Range(0,1)) = 1
        Color1 ("Color1", Color) = (1,1,1,1)
        Color2 ("Color2", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
        LOD 100
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vertexShader
            #pragma fragment fragmentShader

            #include "UnityCG.cginc"

            struct vertexData
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct fragmentData
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 objectPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _SecondTexture;
            float Alpha;
            float4 Color1;
            float4 Color2;

            fragmentData vertexShader(vertexData v)
            {
                fragmentData o;
                o.vertex = UnityObjectToClipPos(v.vertex) + float4(sin(_Time.w),0,0,0);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.objectPos = v.vertex.xyz;


                return o;
            }

            fixed4 fragmentShader(fragmentData i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                
                col *= lerp(Color1, Color2, (sin(_Time.w*10 + i.objectPos.y*100) +1)*0.5);
                //col.rgb = i.objectPos;

                //col.a *= Alpha;

                return saturate(col);
            }
            ENDCG
        }
    }
}
