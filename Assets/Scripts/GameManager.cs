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

    public Color tileHoverColor;
    public Color tileClickedColor;

    public static GameManager instance;

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
        //xmlSerializer = gameObject.GetComponent<HexMapSaver>();
        //xmlDeserializer = gameObject.GetComponent<HexMapLoader>();

        //CreateTileTypeDict();
        //SetUpTileTypeButtons();

        //GotoNewMap();
        GotoEditMap();
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

    public void NewMap()
    {
        GotoNewMap();
    }

    private void GotoEditMap()
    {
        newMapUI.SetActive(false);
        editMapUI.SetActive(true);
        
    }

    private void GotoNewMap()
    {
        newMapUI.SetActive(true);
        editMapUI.SetActive(false);
    }



}
