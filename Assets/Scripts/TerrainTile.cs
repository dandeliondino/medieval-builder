using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class TerrainTile : MonoBehaviour
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

        Debug.Log("My tile started");

        //Debug.Log(transform.GetComponent<MeshFilter>().sharedMesh.bounds.size);
        //tileClicked.AddListener(parentHex.GetComponent<Hex>().MyTileClicked);
        

    }

    void Update()
    {
        
    }



    public void MouseDown()
    {
        //if (EventSystem.current.IsPointerOverGameObject())
        //    return;

        tileClicked.Invoke();
        rend.material.color = gameManager.tileClickedColor;
    }


    public void MouseHover()
    {

        //Debug.Log("Mouse entered tile");

        //if (EventSystem.current.IsPointerOverGameObject())
        //    return;

        rend.material.color = gameManager.tileHoverColor;

    }

    public void MouseExit()
    {
        //Debug.Log("Mouse exited tile");
        rend.material.color = startColor;
    }
}
