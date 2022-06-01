using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Tilemaps;

public class TerrainClickUI : MonoBehaviour
{
    public GameObject canvas;
    public GameObject panel;

    public Tilemap tilemap;
    public TilemapManager tilemapManager;

    public Transform changeTerrainButtonContainer;
    public GameObject changeTerrainButtonPrefab;

    public TextMeshProUGUI coordinatesLabel;
    public TextMeshProUGUI terrainLabel;
    public TextMeshProUGUI heightLabel;

    public HexTileContainer hexTileContainer;

    private Vector3Int hexPosition;

    private void Start()
    {
        Hide();

        tilemapManager = TilemapManager.instance;

        TerrainDef[] terrainDefs = Resources.LoadAll<TerrainDef>("TerrainDefs");
        foreach (var terrainDef in terrainDefs)
        {
            GameObject terrainButton = (GameObject)Instantiate(changeTerrainButtonPrefab, changeTerrainButtonContainer);
            terrainButton.GetComponent<ChangeTerrainButton>().Setup(this, terrainDef);
        }
    }

    public void Hide()
    {
        canvas.SetActive(false);
    }

    public void Show(GameObject terrainTile)
    {
        canvas.SetActive(true);
        hexTileContainer = terrainTile.GetComponent<TerrainTile>().hexTileContainer;
        hexPosition = new Vector3Int(hexTileContainer.xCoord, hexTileContainer.yCoord, 0);
        RefreshTileInfo();
    }

    public void ShowAtTile(GameObject tile)
    {
        panel.transform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, tile.transform.position);
        canvas.SetActive(true);
        hexTileContainer = tile.GetComponent<TerrainTile>().hexTileContainer;
        hexPosition = new Vector3Int(hexTileContainer.xCoord, hexTileContainer.yCoord, 0);

        RefreshTileInfo();
    }

    public void RaiseTerrain()
    {
        hexTileContainer.RaiseTerrain();
        RefreshTileInfo();
    }

    public void LowerTerrain()
    {
        hexTileContainer.LowerTerrain();
        RefreshTileInfo();
    }

    public void ChangeTerrain(TerrainDef terrainDef)
    {
        hexTileContainer.ChangeTerrain(terrainDef);
        RefreshTileInfo();
    }

    public void RefreshTileInfo()
    {
        coordinatesLabel.text = hexTileContainer.xCoord + ", " + hexTileContainer.yCoord;
        terrainLabel.text = hexTileContainer.terrainDef.displayName;
        heightLabel.text = hexTileContainer.height.ToString();
    }

    public void InsertColumn()
    {
        tilemap.InsertCells(hexPosition, new Vector3Int(1, 0, 0));
        tilemap.ResizeBounds();
        tilemap.GetComponent<TilemapManager>().FillEmptyTiles();
    }

    public void InsertRow()
    {
        tilemap.InsertCells(hexPosition, new Vector3Int(0, 1, 0));
        tilemap.ResizeBounds();
        tilemap.GetComponent<TilemapManager>().FillEmptyTiles();
    }

}
