using UnityEditor;
using UnityEngine;
using System.IO;
using UnityEngine.Tilemaps;

public class TileRenamer : EditorWindow
{
    [MenuItem("Tools/Rename Tiles")]
    public static void RenameTiles()
    {
        string tilesFolderPath = "Assets/DH_Folder/Graphics/Tiles";
        string[] guids = AssetDatabase.FindAssets("t:Tile", new[] { tilesFolderPath });

        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            string oldName = Path.GetFileNameWithoutExtension(assetPath);

            // 예: EroEffect_Main_17 → 17
            if (!int.TryParse(oldName.Replace("SexyEffect_", ""), out int number)) continue;

            string newName = $"SexyJutsuEffect_{number:D2}"; // 2자리 숫자 패딩
            string newPath = Path.Combine(Path.GetDirectoryName(assetPath), newName + ".asset");

            // 이름 충돌 방지
            if (AssetDatabase.LoadAssetAtPath<Tile>(newPath) != null)
            {
                Debug.LogWarning($"❗ {newName} 은 이미 존재해. 건너뜀");
                continue;
            }

            AssetDatabase.RenameAsset(assetPath, newName);
            Debug.Log($"✅ {oldName} → {newName}");
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
