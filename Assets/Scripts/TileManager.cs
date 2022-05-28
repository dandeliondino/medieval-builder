using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileManager : MonoBehaviour
{

    public float xDistance = 2f;
    public float zDistance = 1.5f;

    public GameObject hexPrefab;

    public TerrainDef emptyTile;

    private GameObject hexContainer;
    
    private GameObject[][] hexes;

    public Slider xSlider;
    public Slider zSlider;

    public int xTiles;
    public int zTiles;
    public static TileManager instance;

    //public Dictionary<string, TileType> tileTypeDict;

    private GameManager gameManager;


    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one TileManager in scene");
            return;
        }

        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateMap()
    {
        ClearMap();
        GenerateHexes((int)xSlider.value, (int)zSlider.value);
        AddEmptyTiles();
    }

    public void GenerateBlankMap(int xSize, int zSize)
    {
        ClearMap();
        GenerateHexes(xSize, zSize);
        AddEmptyTiles();
    }

    public void GenerateMapFromHexMap(HexMap hexMap)
    {
        ClearMap();
        GenerateHexes(hexMap.xsize, hexMap.zsize);
        foreach (var hexData in hexMap.hexes)
        {
            GameObject hexObj = hexes[hexData.z][hexData.x];
            Hex hex = hexObj.GetComponent<Hex>();
            hex.height = hexData.height;
            TerrainDef tileType = gameManager.tileTypeDict[hexData.tileType];
            hex.AddTile(tileType);

            Debug.Log("Generating hex: " + hexObj.name + " - " + tileType.name);

        }
    }

    public void GenerateHexes(int xsize, int zsize)
    {
        xTiles = xsize;
        zTiles = zsize;

        hexContainer = new GameObject("Hex Container");
        Instantiate(hexContainer, Vector3.zero, Quaternion.identity);

        hexes = new GameObject[zsize][];

        for (int z = 0; z < zsize; z++)
        {
            hexes[z] = new GameObject[xsize];

            for (int x = 0; x < xsize; x++)
            {
                float xLoc = x * xDistance;
                if (z % 2 != 0)
                {
                    xLoc += 1f;
                }

                float zLoc = z * zDistance;
                GameObject hex = (GameObject)Instantiate(hexPrefab, new Vector3(xLoc, 0, zLoc), Quaternion.identity, hexContainer.transform);
                hex.name = x + "," + z;

                Hex hexScript = hex.GetComponent<Hex>();
                hexScript.xcoord = x;
                hexScript.zcoord = z;

                hexes[z][x] = hex;
            }
        }
    }

    public void AddEmptyTiles()
    {
        foreach (var hexArray in hexes)
        {
            foreach (var hexObj in hexArray)
            {
                Hex hex = hexObj.GetComponent<Hex>();
                hex.AddTile(emptyTile);
            }
        }
    }

    public void ClearMap()
    {
        if(hexContainer != null)
        {
            Destroy(hexContainer);
        }
    }

    private GameObject GetHex(int x, int z)
    {
        GameObject hex = hexes[z][x];
        return hex;
    }
}
