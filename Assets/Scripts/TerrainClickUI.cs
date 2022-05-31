using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainClickUI : MonoBehaviour
{
    public GameObject canvas;
    public GameObject panel;

    public HexTileContainer hexTileContainer;

    private void Start()
    {
        Hide();
    }

    public void Hide()
    {
        canvas.SetActive(false);
    }

    public void ShowAtTile(GameObject tile)
    {
        panel.transform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, tile.transform.position);
        canvas.SetActive(true);
        hexTileContainer = tile.GetComponent<TerrainTile>().hexTileContainer;
    }

    public void RaiseTerrain()
    {
        hexTileContainer.RaiseTerrain();
    }

    public void LowerTerrain()
    {
        hexTileContainer.LowerTerrain();
    }

}
