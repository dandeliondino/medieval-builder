using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainClickUI : MonoBehaviour
{
    public GameObject canvas;

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
        transform.position = tile.transform.position;
        canvas.SetActive(true);
    }

}
