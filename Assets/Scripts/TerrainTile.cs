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
    public Hex hex;

    private Renderer rend;
    private Color startColor;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    public void MouseDown()
    {
        rend.material.color = gameManager.tileClickedColor;
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
