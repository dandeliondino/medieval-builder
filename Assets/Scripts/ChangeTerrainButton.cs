using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChangeTerrainButton : MonoBehaviour
{
    public TerrainDef terrainDef;
    public TerrainClickUI terrainClickUI;

    public void OnClick()
    {
        terrainClickUI.ChangeTerrain(terrainDef);
    }

    public void Setup(TerrainClickUI newTerrainClickUI, TerrainDef newTerrainDef)
    {
        terrainClickUI = newTerrainClickUI;
        terrainDef = newTerrainDef;
        GetComponentInChildren<TextMeshProUGUI>().text = terrainDef.displayName;
        gameObject.GetComponent<Button>().onClick.AddListener(OnClick);
    }

}
