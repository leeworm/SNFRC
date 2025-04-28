using UnityEngine;

public static class PaletteLUTGenerator
{
    public static Texture2D Generate16ColorLUT(Color32[] colors)
    {
        Texture2D tex = new Texture2D(16, 1, TextureFormat.RGBA32, false);
        tex.filterMode = FilterMode.Point;
        tex.wrapMode = TextureWrapMode.Clamp;

        for (int i = 0; i < 16; i++)
        {
            tex.SetPixel(i, 0, i < colors.Length ? colors[i] : Color.magenta);
        }

        tex.Apply();
        return tex;
    }
}
