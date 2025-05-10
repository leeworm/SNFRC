// File: Assets/Editor/IndexTextureGenerator.cs
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

public static class IndexTextureGenerator
{
    [MenuItem("Tools/Generate Index Texture")]
    public static void GenerateIndexTex()
    {
        Texture2D source = Selection.activeObject as Texture2D;

        if (source == null)
        {
            Debug.LogError("Select a source texture in Project view.");
            return;
        }

        string path = AssetDatabase.GetAssetPath(source);
        string fullPath = Path.Combine(Application.dataPath.Replace("Assets", ""), path);
        Texture2D readableTex = AssetDatabase.LoadAssetAtPath<Texture2D>(path);

        Texture2D tex = new Texture2D(readableTex.width, readableTex.height, TextureFormat.RGB24, false);
        tex.filterMode = FilterMode.Point;

        // 수동으로 정의된 팔레트
        Color32[] palette = new Color32[]
        {
            new Color32(99, 207, 99, 255),
            new Color32(0, 0, 0, 255),
            new Color32(57, 51, 255, 255),
            new Color32(191, 115, 0, 255),
            new Color32(220, 255, 255, 255),
            new Color32(51, 0, 134, 255),
            new Color32(0, 207, 255, 255),
            new Color32(147, 0, 0, 255),
            new Color32(239, 235, 180, 255),
            new Color32(81, 255, 0, 255),
            new Color32(89, 140, 242, 255),
            new Color32(188, 17, 164, 255),
            new Color32(255, 172, 0, 255),
            new Color32(182, 0, 159, 255),
            new Color32(131, 220, 0, 255),
            new Color32(255, 255, 255, 255)
        };

        Color32[] pixels = readableTex.GetPixels32();

        for (int y = 0; y < tex.height; y++)
        {
            for (int x = 0; x < tex.width; x++)
            {
                Color32 color = pixels[y * tex.width + x];
                int index = FindClosestColorIndex(color, palette);
                float indexFloat = index / 15.0f; // Normalize to 0~1
                tex.SetPixel(x, y, new Color(indexFloat, 0, 0));
            }
        }

        tex.Apply();

        string outPath = Path.GetDirectoryName(path) + "/" + source.name + "_IndexTex.png";
        File.WriteAllBytes(outPath, tex.EncodeToPNG());
        AssetDatabase.Refresh();

        Debug.Log("Index Texture saved to: " + outPath);
    }

    private static int FindClosestColorIndex(Color32 c, Color32[] palette)
    {
        int bestIndex = 0;
        int bestDist = int.MaxValue;

        for (int i = 0; i < palette.Length; i++)
        {
            int dist =
                (c.r - palette[i].r) * (c.r - palette[i].r) +
                (c.g - palette[i].g) * (c.g - palette[i].g) +
                (c.b - palette[i].b) * (c.b - palette[i].b);

            if (dist < bestDist)
            {
                bestDist = dist;
                bestIndex = i;
            }
        }

        return bestIndex;
    }
}
