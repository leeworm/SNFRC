// File: Assets/Editor/PaletteAndIndexGenerator.cs
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;
using System.Collections.Generic;

public static class PaletteAndIndexGenerator
{
    [MenuItem("Tools/Generate Palette + Index Texture")]
    public static void GeneratePaletteAndIndex()
    {
        Texture2D source = Selection.activeObject as Texture2D;

        if (source == null)
        {
            Debug.LogError("Select a source texture in Project view.");
            return;
        }

        string path = AssetDatabase.GetAssetPath(source);
        TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;

        if (!importer.isReadable)
        {
            importer.isReadable = true;
            importer.textureCompression = TextureImporterCompression.Uncompressed;
            importer.filterMode = FilterMode.Point;
            importer.mipmapEnabled = false;
            AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
        }

        Texture2D readableTex = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
        Color32[] pixels = readableTex.GetPixels32();

        // 1. 추출: 상위 16색
        var grouped = pixels.GroupBy(c => c)
                            .OrderByDescending(g => g.Count())
                            .Take(16)
                            .Select(g => g.Key)
                            .ToArray();

        Debug.Log($"Palette extracted from {source.name}, total: {grouped.Length} colors.");

        // 2. 팔레트 텍스처 만들기
        Texture2D paletteTex = new Texture2D(16, 1, TextureFormat.RGBA32, false);
        paletteTex.filterMode = FilterMode.Point;
        for (int i = 0; i < 16; i++)
            paletteTex.SetPixel(i, 0, i < grouped.Length ? grouped[i] : Color.magenta);
        paletteTex.Apply();

        // 저장
        string baseDir = Path.GetDirectoryName(path);
        string palettePath = Path.Combine(baseDir, source.name + "_PaletteLUT.png");
        File.WriteAllBytes(palettePath, paletteTex.EncodeToPNG());

        // 3. 인덱스 텍스처 생성
        Texture2D indexTex = new Texture2D(readableTex.width, readableTex.height, TextureFormat.RGBA32, false);
        indexTex.filterMode = FilterMode.Point;

        for (int y = 0; y < indexTex.height; y++)
        {
            for (int x = 0; x < indexTex.width; x++)
            {
                Color32 color = readableTex.GetPixel(x, y);
                int index = FindClosestIndex(color, grouped);
                float encoded = index / 15.0f;
                indexTex.SetPixel(x, y, new Color(encoded, 0, 0, 1));
            }
        }

        indexTex.Apply();
        string indexPath = Path.Combine(baseDir, source.name + "_IndexTex.png");
        File.WriteAllBytes(indexPath, indexTex.EncodeToPNG());

        AssetDatabase.Refresh();

        Debug.Log("✅ Palette LUT and IndexTex saved: → " + palettePath + "→ " + indexPath);
    }

    private static int FindClosestIndex(Color32 c, Color32[] palette)
    {
        int bestIndex = 0;
        int bestDist = int.MaxValue;

        for (int i = 0; i < palette.Length; i++)
        {
            int dist = (c.r - palette[i].r) * (c.r - palette[i].r)
                     + (c.g - palette[i].g) * (c.g - palette[i].g)
                     + (c.b - palette[i].b) * (c.b - palette[i].b);
            if (dist < bestDist)
            {
                bestDist = dist;
                bestIndex = i;
            }
        }

        return bestIndex;
    }
}