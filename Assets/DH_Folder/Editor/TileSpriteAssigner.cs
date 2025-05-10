using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;
using System.Collections.Generic;

public class TileSpriteAssigner : EditorWindow
{
    [MenuItem("Tools/Assign Sprites to Tiles")]
    public static void AssignSpritesToTiles()
    {
        string tilePath = "Assets/DH_Folder/Graphics/Tiles";
        string spriteSheetPath = "Assets/DH_Folder/Graphics/Sprites/FX/SexyJutsuEffect.png";

        // 1. 스프라이트 시트 내 모든 조각 가져오기
        Object[] sprites = AssetDatabase.LoadAllAssetRepresentationsAtPath(spriteSheetPath);
        Dictionary<string, Sprite> spriteDict = new();

        foreach (Object obj in sprites)
        {
            if (obj is Sprite sprite)
            {
                spriteDict[sprite.name] = sprite;
            }
        }

        if (spriteDict.Count == 0)
        {
            Debug.LogError("❌ 스프라이트 시트에서 조각을 불러오지 못했어요.");
            return;
        }

        // 2. 타일들 가져와서 이름으로 매칭
        string[] guids = AssetDatabase.FindAssets("t:Tile", new[] { tilePath });
        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            Tile tile = AssetDatabase.LoadAssetAtPath<Tile>(assetPath);
            if (tile == null) continue;

            string tileName = Path.GetFileNameWithoutExtension(assetPath);
            // 예: EroEffect_Main_17 → 17
            if (!int.TryParse(tileName.Replace("EroEffect_Main1_", ""), out int index)) continue;

            string spriteName = $"SexyJutsuEffect_{index}";
            if (spriteDict.TryGetValue(spriteName, out Sprite sprite))
            {
                tile.sprite = sprite;
                EditorUtility.SetDirty(tile);
                Debug.Log($"✅ {tileName} 에 {spriteName} 할당 완료");
            }
            else
            {
                Debug.LogWarning($"⚠️ {spriteName} 스프라이트 못 찾음");
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
