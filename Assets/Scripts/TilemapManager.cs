using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : MonoBehaviour
{
    public BoundsInt area;
    public Sprite sprite;
    public TerrainDef emptyTerrainDef;
    public GameObject hexTileContainer;

    private HexTile originalTile;
    private HexTile emptyTile;
    private Tilemap tilemap;


    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        emptyTile = HexTile.CreateInstance<HexTile>();
        emptyTile.gameObject = hexTileContainer;

        tilemap.origin = area.position;
        tilemap.size = area.size;
        tilemap.ResizeBounds();

        FillEmptyTiles();
    }


    public void FillEmptyTiles()
    {
        for (int x = 0; x < tilemap.size.x; x++)
        {
            for (int y = 0; y < tilemap.size.y; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                if (!tilemap.HasTile(pos))
                {
                    tilemap.SetTile(pos, emptyTile);
                    GameObject obj = tilemap.GetInstantiatedObject(pos);
                    obj.GetComponent<HexTileContainer>().Setup(x, y, emptyTerrainDef, 0);
                }
            }
        }
    }

    public void RefreshHexCoordinates()
    {
        for (int x = 0; x < tilemap.size.x; x++)
        {
            for (int y = 0; y < tilemap.size.y; y++)
            {
                HexTileContainer hexTileContainer = getHexAtPosition(x, y);
                if (hexTileContainer != null)
                {
                    hexTileContainer.xCoord = x;
                    hexTileContainer.yCoord = y;
                }
            }
        }
    }

    public HexTileContainer getHexAtPosition(int x, int y)
    {
        Vector3Int pos = new Vector3Int(x, y, 0);
        GameObject obj = tilemap.GetInstantiatedObject(pos);
        if (obj == null)
        {
            Debug.Log("No object at position: " + pos);
            return null;
        }

        HexTileContainer hexTileContainer = obj.GetComponent<HexTileContainer>();
        return hexTileContainer;
    }


    // USE THIS WITH LOADING FROM SAVE
    //public void loadTiles()
    //{
    //    Vector3Int[] positions = new Vector3Int[tilemap.size.x * tilemap.size.y];
    //    TileBase[] tileArray = new TileBase[positions.Length];

    //    for (int i = 0; i < positions.Length; i++)
    //    {
    //        positions[i] = new Vector3Int(i % tilemap.size.x, i / tilemap.size.y, 0);
    //        tileArray[i] = newTile;
    //    }

    //    tilemap.SetTiles(positions, tileArray);
    //}
}
