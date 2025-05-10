// 파일 위치: Assets/Editor/CreateBasicTile.cs

using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class CreateBasicTile
{
    [MenuItem("Assets/Create/2D/Tile (Manual)")]
    public static void CreateTile()
    {
        Tile tile = ScriptableObject.CreateInstance<Tile>();
        string path = AssetDatabase.GenerateUniqueAssetPath("Assets/NewBasicTile.asset");
        AssetDatabase.CreateAsset(tile, path);
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();
        Selection.activeObject = tile;
    }
}
