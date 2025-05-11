using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

public class WorldChanger_W : MonoBehaviour
{
    public Tilemap tilemap;

    public TileBase ATile;
    public TileBase BTile;
    public TileBase AATile;
    public TileBase BBTile;
    public TileBase CTile;
    public TileBase DTile;
    public TileBase CCTile; 
    public TileBase DDTile;
    public TileBase ETile;
    public TileBase EETile;


    public float delayBetweenTiles = 0.01f;

    public void StartWorldTransition()
    {
        StartCoroutine(ChangeTiles());
    }

    private IEnumerator ChangeTiles()
    {
        BoundsInt bounds = tilemap.cellBounds;

       for (int x = bounds.xMax - 1; x >= bounds.xMin; x--)  // 오른쪽 → 왼쪽
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)   // 아래 → 위
            {
                Vector3Int tilePos = new Vector3Int(x, y, 0);
                TileBase currentTile = tilemap.GetTile(tilePos);

                if (currentTile == null)
                    continue;
                    
                    Vector3Int pos = new Vector3Int(x, y, 0);
                    TileBase current = tilemap.GetTile(pos);

                if (current == ATile)
                    tilemap.SetTile(pos, AATile);
                else if (current == BTile)
                    tilemap.SetTile(pos, BBTile);
                else if (current == CTile)
                    tilemap.SetTile(pos, CCTile);
                else if (current == DTile)
                    tilemap.SetTile(pos, DDTile);
                else if (current == ETile)
                    tilemap.SetTile(pos, EETile);


                yield return new WaitForSeconds(delayBetweenTiles);


                
            }
        }
    }

    

}
