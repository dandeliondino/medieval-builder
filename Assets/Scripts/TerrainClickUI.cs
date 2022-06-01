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

    public Hex currentHex;

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
        currentHex = terrainTile.GetComponent<TerrainTile>().hex;
        hexPosition = new Vector3Int(currentHex.xCoord, currentHex.yCoord, 0);
        RefreshTileInfo();
    }

    public void ShowAtTile(GameObject tile)
    {
        panel.transform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, tile.transform.position);
        canvas.SetActive(true);
        currentHex = tile.GetComponent<TerrainTile>().hex;
        hexPosition = new Vector3Int(currentHex.xCoord, currentHex.yCoord, 0);

        RefreshTileInfo();
    }

    public void RaiseTerrain()
    {
        currentHex.RaiseTerrain();
        RefreshTileInfo();
    }

    public void LowerTerrain()
    {
        currentHex.LowerTerrain();
        RefreshTileInfo();
    }

    public void ChangeTerrain(TerrainDef terrainDef)
    {
        currentHex.ChangeTerrain(terrainDef);
        RefreshTileInfo();
    }

    public void RefreshTileInfo()
    {
        coordinatesLabel.text = currentHex.xCoord + ", " + currentHex.yCoord;
        terrainLabel.text = currentHex.terrainDef.displayName;
        heightLabel.text = currentHex.height.ToString();
    }

}
