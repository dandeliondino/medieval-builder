using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : MonoBehaviour
{
    public BoundsInt area;
    public GameObject tilePrefab;
    public Sprite sprite;

    private Tilemap tilemap;

    public Color tileColor;

    public HexTile placeholderTile;
    public HexTile dirtTile;

    void Start()
    {
        tilemap = GetComponent<Tilemap>();

        //HexTile newTile = HexTile.CreateInstance<HexTile>();
        //newTile.sprite = sprite;
        //newTile.gameObject = tilePrefab;

        

        //Vector3Int pos = new Vector3Int(0, 0, 0);
        //tilemap.SetTile(pos, newTile);

        tilemap.origin = area.position;
        tilemap.size = area.size;
        tilemap.ResizeBounds();

        TileBase[] tileArray = new TileBase[area.size.x * area.size.y * area.size.z];

        for (int i = 0; i < tileArray.Length; i++)
        {
            tileArray[i] = dirtTile;
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
        //HexTile secondTile = HexTile.CreateInstance<HexTile>();
        //secondTile.sprite = sprite;
        //secondTile.color = tileColor;

        //secondTile.color = tileColor;

        for (int x = 0; x < tilemap.size.x; x++)
        {
            for (int y = 0; y < tilemap.size.y; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                if (!tilemap.HasTile(pos))
                {
                    tilemap.SetTile(pos, placeholderTile);
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
