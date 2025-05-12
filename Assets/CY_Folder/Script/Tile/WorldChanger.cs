using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;

public class WorldChanger : MonoBehaviour
{
    public static WorldChanger Instance;
    public Tilemap tilemap;

    [Header("타일 매핑")]
    public TileBase grassTile;
    public TileBase waterTile;
    public TileBase netherrackTile;
    public TileBase lavaTile;
    public TileBase stoneTile;
    public TileBase dirtTile;
    public TileBase sandTile;
    public TileBase GoldTile;
    public TileBase SilverTile;
    public TileBase DaiTile;

    public TileBase N_stoneTile;
    public TileBase N_dirtTile;
    public TileBase N_sandTile;
    public TileBase N_JuaLTile;
    public TileBase NGressTile;
    public TileBase EnderTile;

    public float delayBetweenTiles = 0.01f; // 순차적으로 바뀌게 할 딜레이

    private void Awake()
    {
        Instance = this; //  전역 접근용
    }

    public void StartWorldTransition()
    {
        StartCoroutine(ChangeTiles());
    }

    private IEnumerator ChangeTiles()
    {
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

        for (int x = bounds.size.x - 1; x >= 0; x--)
        {
            for (int y = bounds.size.y - 1; y >= 0; y--)
            {
                Vector3Int tilePos = new Vector3Int(bounds.x + x, bounds.y + y, 0);
                TileBase currentTile = tilemap.GetTile(tilePos);

                if (currentTile == grassTile)
                {
                    tilemap.SetTile(tilePos, netherrackTile);
                }
                else if (currentTile == waterTile)
                {
                    tilemap.SetTile(tilePos, lavaTile);
                }
                else if (currentTile == stoneTile)
                {
                    tilemap.SetTile(tilePos, N_stoneTile);
                }
                else if (currentTile == dirtTile)
                {
                    tilemap.SetTile(tilePos, N_dirtTile);
                }
                else if (currentTile == sandTile)
                {
                    tilemap.SetTile(tilePos, N_sandTile);
                }
                else if (currentTile == GoldTile)
                {
                    tilemap.SetTile(tilePos, N_JuaLTile);
                }
                else if (currentTile == SilverTile)
                {
                    tilemap.SetTile(tilePos, N_JuaLTile);
                }
                else if (currentTile == DaiTile)
                {
                    tilemap.SetTile(tilePos, N_JuaLTile);
                }
                else if (currentTile == NGressTile)
                {
                    tilemap.SetTile(tilePos, EnderTile);
                }
                
                
                
                yield return new WaitForSeconds(delayBetweenTiles); // 순차적 변경
            }
        }
    }
}
