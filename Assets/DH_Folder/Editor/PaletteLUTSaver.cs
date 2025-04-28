// File: Assets/Editor/PaletteLUTSaver.cs
using UnityEngine;
using UnityEditor;
using System.IO;

public static class PaletteLUTSaver
{
    [MenuItem("Tools/Save Palette LUT as PNG")]
    public static void SaveLUT()
    {
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
            new Color32(255, 255, 255, 255), // 마지막 여분
        };

        Texture2D lut = PaletteLUTGenerator.Generate16ColorLUT(palette);
        byte[] pngData = lut.EncodeToPNG();

        string path = "Assets/Generated_LUT.png";
        File.WriteAllBytes(path, pngData);
        AssetDatabase.Refresh();

        Debug.Log($"Saved LUT texture to: {path}");
    }
}
