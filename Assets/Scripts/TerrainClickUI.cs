using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainClickUI : MonoBehaviour
{
    public GameObject canvas;
    public GameObject panel;

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
        //Vector3 cameraPosition = Camera.main.WorldToScreenPoint(tile.transform.position);
        //transform.position = tile.transform.position;
        panel.transform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, tile.transform.position);
        canvas.SetActive(true);
        //RotateToCamera(Camera.main);
    }

    public void RotateToCamera(Camera camera)
    {
        //transform.rotation = Quaternion.LookRotation(transform.position - camera.transform.position);
        //transform.LookAt(camera.transform);
        transform.LookAt(transform.position + camera.transform.rotation * Vector3.forward, Vector3.up);
        
    }

}
