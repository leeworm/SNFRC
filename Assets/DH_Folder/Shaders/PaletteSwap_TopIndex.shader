
Shader "Custom/PaletteSwap_TopIndex"
{
    Properties
    {
        _MainTex("Main Texture", 2D) = "white" {}
        _ReferencePalette("Reference Palette", 2D) = "white" {}
        _SwapPalette("Swap Palette", 2D) = "white" {}
        _SwapIndex("Swap Index", Float) = 0
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            sampler2D _ReferencePalette;
            sampler2D _SwapPalette;
            float _SwapIndex;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float3 GetSwapColor(float3 inputColor)
            {
                float minDistance = 99999.0;
                int bestIndex = 0;

                // 가장 가까운 기준 색상 찾기
                for (int i = 0; i < 16; i++)
                {
                    float2 uvRef = float2((i + 0.5) / 16.0, 0.5);
                    float3 refColor = tex2D(_ReferencePalette, uvRef).rgb;
                    float d = distance(refColor, inputColor);
                    if (d < minDistance)
                    {
                        minDistance = d;
                        bestIndex = i;
                    }
                }

                // swap palette의 해당 줄에서 색상 추출 (위쪽이 0)
                float2 uvSwap;
                uvSwap.x = (bestIndex + 0.5) / 16.0;
                uvSwap.y = (_SwapIndex + 0.5) / 12.0;
                return tex2D(_SwapPalette, uvSwap).rgb;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float3 inputColor = tex2D(_MainTex, i.uv).rgb;
                float3 outputColor = GetSwapColor(inputColor);
                return float4(outputColor, 1.0);
            }
            ENDCG
        }
    }
}
