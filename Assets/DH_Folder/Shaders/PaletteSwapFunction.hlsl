void PaletteSwapFunction_float(float2 uv, float indexTex, float2 paletteSize, float FrameIndex, out float4 OutColor)
{
    // 인덱스 값 추출 (0~1 range → 0~15)
    float gray = indexTex;
    float xIndex = floor(gray * (paletteSize.x - 1) + 0.5); // 0 ~ 15

    // 프레임 인덱스를 y 좌표로 사용 (0~11)
    float2 paletteUV;
    paletteUV.x = (xIndex + 0.5) / paletteSize.x;
    paletteUV.y = (FrameIndex + 0.5) / paletteSize.y;

    OutColor = SAMPLE_TEXTURE2D(_PaletteTex, sampler_PaletteTex, paletteUV);
}
