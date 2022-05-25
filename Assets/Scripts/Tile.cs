using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Tile : MonoBehaviour
{

    public UnityEvent tileClicked;

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
        //tileClicked.AddListener(parentHex.GetComponent<Hex>().MyTileClicked);
        

    }

    void Update()
    {
        
    }



    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        tileClicked.Invoke();
        rend.material.color = gameManager.tileClickedColor;
    }


    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        rend.material.color = gameManager.tileHoverColor;

    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
