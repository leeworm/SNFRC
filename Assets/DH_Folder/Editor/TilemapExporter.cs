// Assets/Editor/TilemapExporter.cs
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

public class TilemapExporter
{
    [MenuItem("Tools/Export Tilemap To PNG")]
    public static void ExportTilemapToPNG()
    {
        // 선택한 오브젝트에서 Tilemap 컴포넌트 꺼내기
        var go = Selection.activeGameObject;
        if (go == null)
        {
            Debug.LogError("Tilemap GameObject를 선택해주세요!");
            return;
        }

        var tilemap = go.GetComponent<Tilemap>();
        if (tilemap == null)
        {
            Debug.LogError("선택한 게임오브젝트에 Tilemap 컴포넌트가 없습니다!");
            return;
        }

        // 타일맵 범위와 해상도 계산
        var bounds = tilemap.cellBounds;
        // sprite.pixelsPerUnit 정보 가져오기 (타일들이 동일한 PPU라고 가정)
        TileBase anyTile = tilemap.GetTile(bounds.min);
        float ppu = 100f;  // float로 바꿔줌
        if (anyTile is Tile t && t.sprite != null)
            ppu = t.sprite.pixelsPerUnit;
        int texWidth = Mathf.RoundToInt(bounds.size.x * tilemap.cellSize.x * ppu);
        int texHeight = Mathf.RoundToInt(bounds.size.y * tilemap.cellSize.y * ppu);

        // 가상 카메라 세팅
        var camGO = new GameObject("TempTilemapCam");
        var cam = camGO.AddComponent<Camera>();
        cam.orthographic = true;
        cam.clearFlags = CameraClearFlags.SolidColor;
        cam.backgroundColor = new Color(0, 0, 0, 0); // 투명 배경
        cam.orthographicSize = bounds.size.y * tilemap.cellSize.y / 2f;
        cam.transform.position = new Vector3(
            bounds.center.x * tilemap.cellSize.x,
            bounds.center.y * tilemap.cellSize.y,
            -10f
        );

        // RenderTexture 준비
        var rt = new RenderTexture(texWidth, texHeight, 24);
        cam.targetTexture = rt;

        // 렌더링 & ReadPixels
        cam.Render();
        RenderTexture.active = rt;
        var tex = new Texture2D(texWidth, texHeight, TextureFormat.RGBA32, false);
        tex.ReadPixels(new Rect(0, 0, texWidth, texHeight), 0, 0);
        tex.Apply();

        // PNG로 인코딩 & 파일 쓰기
        var bytes = tex.EncodeToPNG();
        var path = Application.dataPath + "/TilemapExport.png";
        System.IO.File.WriteAllBytes(path, bytes);
        Debug.Log($"✅ Tilemap이 {path} 에 저장되었습니다!");

        // 정리
        RenderTexture.active = null;
        cam.targetTexture = null;
        Object.DestroyImmediate(rt);
        Object.DestroyImmediate(camGO);
        AssetDatabase.Refresh();
    }
}
