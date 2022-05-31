using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class TerrainTile : MonoBehaviour
{
    //public GameObject terrainClickUIObject;
    //private TerrainClickUI terrainClickUI;

    public UnityEvent tileClicked;
    public HexTileContainer hexTileContainer;

    private Renderer rend;
    private Color startColor;

    private GameObject parentHex;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;

        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        parentHex = transform.parent.gameObject;

        //terrainClickUI = terrainClickUIObject.GetComponent<TerrainClickUI>();

        //tileClicked.AddListener(parentHex.GetComponent<Hex>().MyTileClicked);
        

    }

    public void MouseDown()
    {
        //tileClicked.Invoke();
        rend.material.color = gameManager.tileClickedColor;
        //terrainClickUI.ShowAtTile(this.gameObject);
    }


    public void MouseHover()
    {
        rend.material.color = gameManager.tileHoverColor;

    }

    public void MouseExit()
    {
        rend.material.color = startColor;
    }
}
