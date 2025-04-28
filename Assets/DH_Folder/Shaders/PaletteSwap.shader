Shader "Unlit/PaletteSwap"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _IndexTex ("Index Texture", 2D) = "white" {}
        _PaletteTex ("Palette Texture (16x12)", 2D) = "white" {}
        _FrameIndex ("Palette Frame Index (0-11)", Float) = 0
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
            sampler2D _IndexTex;
            sampler2D _PaletteTex;
            float _FrameIndex;

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;

                // Sample index from index texture (assume grayscale 0.0 ~ 1.0 → scaled to 0~15)
                float index = tex2D(_IndexTex, uv).r * 15.999;

                // Calculate UV to sample from palette
                float2 paletteUV;
                paletteUV.x = (floor(index) + 0.5) / 16.0;
                paletteUV.y = (_FrameIndex + 0.5) / 12.0;

                fixed4 newColor = tex2D(_PaletteTex, paletteUV);
                return newColor;
            }
            ENDCG
        }
    }
}