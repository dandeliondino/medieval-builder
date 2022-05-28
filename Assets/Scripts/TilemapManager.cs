using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : MonoBehaviour
{
    public BoundsInt area;
    public Sprite sprite;
    public TerrainDef oldTileTerrain;
    public TerrainDef newTileTerrain;
    public GameObject hexTileContainer;

    private HexTile originalTile;
    private HexTile newTile;
    private Tilemap tilemap;


    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        newTile = HexTile.CreateInstance<HexTile>();
        originalTile = HexTile.CreateInstance<HexTile>();

        newTile.gameObject = hexTileContainer;
        originalTile.gameObject = hexTileContainer;


        tilemap.origin = area.position;
        tilemap.size = area.size;
        tilemap.ResizeBounds();

        TileBase[] tileArray = new TileBase[area.size.x * area.size.y * area.size.z];

        for (int i = 0; i < tileArray.Length; i++)
        {
            tileArray[i] = newTile;
        }

        tilemap.SetTilesBlock(area, tileArray);

        IncreaseSize();

        Debug.Log("Tilemap size: " + tilemap.size);
        Debug.Log("Tilemap origin: " + tilemap.origin);
        //Debug.Log("Tilemap cell: " + tilemap.GetTile(new Vector3Int(0, 0, 0)));
        //Debug.Log("Tilemap cell: " + tilemap.GetTile(new Vector3Int(1, 1, 1)));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseSize()
    {
        tilemap.size = new Vector3Int(tilemap.size.x + 2, tilemap.size.y + 2, tilemap.size.z);
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
                    tilemap.SetTile(pos, newTile);
                    //HexTile t = (HexTile)tilemap.GetTile(pos);
                    GameObject obj = tilemap.GetInstantiatedObject(pos);
                    obj.GetComponent<HexTileContainer>().terrainDef = newTileTerrain;
                    obj.GetComponent<HexTileContainer>().Setup();
                } else
                {
                    GameObject obj = tilemap.GetInstantiatedObject(pos);
                    obj.GetComponent<HexTileContainer>().terrainDef = oldTileTerrain;
                    obj.GetComponent<HexTileContainer>().height = 3;
                    obj.GetComponent<HexTileContainer>().Setup();
                }
            }
        }
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
