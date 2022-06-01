using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    HexMapSaver xmlSerializer;
    HexMapLoader xmlDeserializer;

    public Slider xSlider;
    public Slider zSlider;

    public GameObject newMapUI;
    public GameObject editMapUI;

    public TerrainDef tileTypeToPlace;

    public GameObject tileTypeToggle;
    public GameObject tileTypeButtonPrefab;
    public GameObject tileTypeButtonContainer;

    public Color infoHoverColor;
    public Color infoClickedColor;
    public Color raiseTerrainHoverColor;
    public Color raiseTerrainClickedColor;
    public Color lowerTerrainHoverColor;
    public Color lowerTerrainClickedColor;
    public Color paintHoverColor;
    public Color paintClickedColor;

    public Color tileHoverColor;
    public Color tileClickedColor;

    public Dictionary<string, TerrainDef> tileTypeDict;

    public static GameManager instance;

    public string INFO = "INFO";
    public string PAINT = "PAINT";
    public string RAISE_TERRAIN = "RAISE_TERRAIN";
    public string LOWER_TERRAIN = "LOWER_TERRAIN";

    public string state;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one GameManager in scene");
            return;
        }

        instance = this;
    }

    private void Start()
    {
        xmlSerializer = gameObject.GetComponent<HexMapSaver>();
        xmlDeserializer = gameObject.GetComponent<HexMapLoader>();

        CreateTileTypeDict();
        SetUpTileTypeButtons();

        GotoNewMap();
    }

    public void LoadMap()
    {
        //GotoEditMap();
        //HexMap hexMap = xmlDeserializer.deserialize();
        //Debug.Log("HexMap loaded: " + hexMap.xsize + ", " + hexMap.zsize);
        //tileManager.GenerateMapFromHexMap(hexMap);
    }

    public void CreateBlankMap()
    {
        GotoEditMap();
        //tileManager.GenerateBlankMap((int)xSlider.value, (int)zSlider.value);
    }

    public void ChangeState(string newState)
    {
        Debug.Log(state + " => " + newState);
        state = newState;

        UpdateHoverColors();

    }

    private void UpdateHoverColors()
    {
        if (state == INFO)
        {
            tileHoverColor = infoHoverColor;
            tileClickedColor = infoClickedColor;
        }
        else if (state == PAINT)
        {
            //tileHoverColor = paintHoverColor;
            //tileClickedColor = paintClickedColor;
            tileHoverColor = tileTypeToPlace.material.color;
            tileClickedColor = paintClickedColor;
        }
        else if (state == RAISE_TERRAIN)
        {
            tileHoverColor = raiseTerrainHoverColor;
            tileClickedColor = raiseTerrainClickedColor;
        }
        else if (state == LOWER_TERRAIN)
        {
            tileHoverColor = lowerTerrainHoverColor;
            tileClickedColor = lowerTerrainClickedColor;
        }
    }

    public void NewMap()
    {
        //tileManager.ClearMap();
        GotoNewMap();
    }

    private void GotoEditMap()
    {
        newMapUI.SetActive(false);
        editMapUI.SetActive(true);
        ChangeState(INFO);
        
    }

    private void GotoNewMap()
    {
        newMapUI.SetActive(true);
        editMapUI.SetActive(false);
    }

    public void OnTileTypeToPlaceChanged()
    {
        Toggle selectedTileTypeToggle = tileTypeButtonContainer.GetComponent<ToggleGroup>().GetFirstActiveToggle();
        SetTileTypeToPlace(selectedTileTypeToggle.GetComponent<TileTypeToggle>().tileType);
    }

    public void SetTileTypeToPlace(TerrainDef newTileTypeToPlace)
    {
        tileTypeToPlace = newTileTypeToPlace;
        UpdateHoverColors();
        Debug.Log("tileTypeToPlace=" + tileTypeToPlace.name);
    }

    private void SetUpTileTypeButtons()
    {
        foreach (KeyValuePair<string, TerrainDef> elem in tileTypeDict)
        {
            TerrainDef tileType = elem.Value;

            GameObject tileTypeButton = (GameObject)Instantiate(tileTypeToggle, tileTypeButtonContainer.transform, false);
            tileTypeButton.GetComponent<Toggle>().group = tileTypeButtonContainer.GetComponent<ToggleGroup>();
            tileTypeButton.GetComponentInChildren<Text>().text = tileType.displayName;
            tileTypeButton.GetComponent<TileTypeToggle>().tileType = tileType;

            //TextMeshProUGUI buttonText = tileTypeButton.GetComponentInChildren<TextMeshProUGUI>();
            //buttonText.text = tileType.displayName;
            tileTypeButton.GetComponent<Toggle>().onValueChanged.AddListener(delegate { OnTileTypeToPlaceChanged(); });
        }
    }

    private void CreateTileTypeDict()
    {
        TerrainDef[] tileTypes = Resources.LoadAll<TerrainDef>("Tile Types");

        Debug.Log("TileTypes Loaded: " + tileTypes.Length);
        tileTypeDict = new Dictionary<string, TerrainDef>();
        foreach (var tileType in tileTypes)
        {
            tileTypeDict.Add(tileType.name, tileType);
        }
    }
}
