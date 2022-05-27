using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapScript : MonoBehaviour
{
    public BoundsInt area;
    public GameObject tilePrefab;
    // Start is called before the first frame update
    void Start()
    {
        Tilemap tilemap = GetComponent<Tilemap>();
        TileBase[] tileArray = tilemap.GetTilesBlock(area);
        for (int index = 0; index < tileArray.Length; index++)
        {
            Debug.Log(tileArray[index]);
            if (tileArray[index] != null)
            {
                TileBase tile = tileArray[index];
                Debug.Log(tile);
            }
        }

        Debug.Log("Tilemap size: " + tilemap.size);
        Debug.Log("Tilemap origin: " + tilemap.origin);
        Debug.Log("Tilemap cell: " + tilemap.GetTile(new Vector3Int(0, 0, 0)));
        Debug.Log("Tilemap cell: " + tilemap.GetTile(new Vector3Int(1, 1, 1)));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
