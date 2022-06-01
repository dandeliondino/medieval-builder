using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : MonoBehaviour
{
    public BoundsInt area;
    public Sprite sprite;
    public TerrainDef emptyTerrainDef;
    public GameObject hexObj;

    private Tile emptyTile;
    private Tilemap tilemap;

    public static TilemapManager instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        emptyTile = Tile.CreateInstance<Tile>();
        emptyTile.gameObject = hexObj;

        InitializeTilemap();
        FillEmptyTiles();
    }

    public void InitializeTilemap()
    {
        tilemap.origin = Vector3Int.zero;
        tilemap.size = area.size;
        tilemap.ResizeBounds();
    }


    public void FillEmptyTiles()
    {
        for (int x = 0; x < tilemap.size.x; x++)
        {
            for (int y = 0; y < tilemap.size.y; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                Debug.Log("Pos: " + pos + " hasTile=" + tilemap.HasTile(pos));
                if (!tilemap.HasTile(pos))
                {
                    tilemap.SetTile(pos, emptyTile);
                    GameObject obj = tilemap.GetInstantiatedObject(pos);
                    obj.GetComponent<Hex>().Setup(x, y, emptyTerrainDef, 0);
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
                Hex hexTileContainer = getHexAtPosition(x, y);
                if (hexTileContainer != null)
                {
                    hexTileContainer.xCoord = x;
                    hexTileContainer.yCoord = y;
                }
            }
        }
    }


    public Hex getHexAtPosition(int x, int y)
    {
        Vector3Int pos = new Vector3Int(x, y, 0);
        GameObject obj = tilemap.GetInstantiatedObject(pos);
        if (obj == null)
        {
            Debug.Log("No object at position: " + pos);
            return null;
        }

        return obj.GetComponent<Hex>();
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
