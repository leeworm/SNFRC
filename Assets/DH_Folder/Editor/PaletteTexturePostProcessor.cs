using UnityEngine;
using UnityEditor;

public class PaletteTexturePostProcessor : AssetPostprocessor
{
    void OnPreprocessTexture()
    {
        if (assetPath.Contains("Generated_LUT.png"))
        {
            TextureImporter importer = (TextureImporter)assetImporter;
            importer.textureType = TextureImporterType.Default;
            importer.filterMode = FilterMode.Point;
            importer.wrapMode = TextureWrapMode.Clamp;
            importer.mipmapEnabled = false;
            importer.alphaSource = TextureImporterAlphaSource.FromInput;
        }
    }

    void OnPostprocessTexture(Texture2D texture)
    {
        if (assetPath.Contains("Generated_LUT.png"))
        {
            Debug.Log("Palette LUT texture imported with pixel-perfect settings.");
        }
    }
}
