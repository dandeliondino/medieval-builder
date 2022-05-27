using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex : MonoBehaviour
{

    public float yOffset = 1f;
    public int minHeight = 0;
    public int maxHeight = 6;

    public GameObject baseTerrain;

    public int xcoord;
    public int zcoord;
    public int height = 0;

    private TileType tileType;
    private GameObject tile;
    private GameObject baseTile;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MyTileClicked()
    {
        if (gameManager.state == gameManager.INFO)
        {
            Debug.Log("Hex " + name + " : " + tileType.name + " : height=" + height);
        } else if (gameManager.state == gameManager.RAISE_TERRAIN)
        {
            ChangeHeightBy(1);
        }
        else if (gameManager.state == gameManager.LOWER_TERRAIN)
        {
            ChangeHeightBy(-1);
        }
        else if (gameManager.state == gameManager.PAINT)
        {
            Destroy(tile);
            AddTile(gameManager.tileTypeToPlace);
        }
    }

    public void ChangeHeightBy(int increment)
    {
        int newHeight = Mathf.Clamp(height + increment, minHeight, maxHeight);
        Debug.Log("Height: " + height + " => " + newHeight);

        if (height == newHeight)
            return;

        height = newHeight;

        UpdateHeight();

    }

    public void UpdateHeight()
    {
        float yNew = height * yOffset;
        float yDiff = yNew - tile.transform.localPosition.y;
        if (yDiff == 0)
            return;

        tile.transform.Translate(new Vector3(0, yDiff, 0), Space.Self);

        if (height > 0)
        {
            if (!baseTile)
                baseTile = (GameObject)Instantiate(baseTerrain, transform, false);
            int baseTileYScale = height;
            baseTile.transform.localScale = new Vector3(1, baseTileYScale, 1);
        }
        else
        {
            if (baseTile)
                Destroy(baseTile);
        }
    }

    public void AddTile(TileType tileTypeToAdd)
    {
        tileType = tileTypeToAdd;
        tile = (GameObject)Instantiate(tileType.prefab, transform, false);
        tile.GetComponent<MyTile>().tileClicked.AddListener(MyTileClicked);
        tile.GetComponent<MeshRenderer>().material = tileType.material;
        UpdateHeight();
    }

    public string getTileTypeName()
    {
        if (tileType == null)
        {
            return("");
        } else
        {
            return tileType.name;
        }
    }
}
