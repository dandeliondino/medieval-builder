using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class MouseInteractions : MonoBehaviour
{
    public GameObject terrainClickUI;

    MouseInput mouseInput;
    private Camera mainCamera;
    private Collider lastCollider;

    PointerEventData pointerData;

    private bool mouseOverUI;
    private bool mouseOverTerrain;

    public void Awake()
    {
        mouseInput = new MouseInput();
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        mouseInput.Enable();
    }

    private void OnDisable()
    {
        mouseInput.Disable();
    }

    private void Start()
    {
        mouseInput.Mouse.PanCamera.performed += context => MouseMoved(context);
        pointerData = new PointerEventData(EventSystem.current)
        {
            pointerId = -1,
        };
    }

    public void MouseMoved(InputAction.CallbackContext context)
    {
        //if (EventSystem.current.IsPointerOverGameObject())
        //{
        //    Debug.Log("IsPointerOverGameObject() = True");
        //    return;
        //}

        Vector2 mousePosition = mouseInput.Mouse.MousePosition.ReadValue<Vector2>();
        //Debug.Log("Mouse moved: " + mousePosition);
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        //RaycastHit[] hits = Physics.RaycastAll(ray, 2000);
        //Debug.Log("3d hits: " + hits.Length);
        //for (int i = 0; i < hits.Length; i++)
        //{
        //    Debug.Log("#" + i + ": " + hits[i].collider.tag);
        //    if (hits[i].collider.CompareTag("UI")) {
        //        Debug.Log("Ignore");
        //        return;
        //    }
        //}

        mouseOverUI = IsMouseOverUI(mousePosition);

        if (mouseOverUI)
        {
            //Debug.Log("Over UI element");
            return;
        }


        if (Physics.Raycast(ray, out hit))
        {
            mouseOverTerrain = false;

            if (hit.collider != null)
            {
                
                if (hit.collider.CompareTag("TerrainTile"))
                {
                    mouseOverTerrain = true;
                }

                if (hit.collider != lastCollider)
                {
                    if (mouseOverTerrain)
                    {
                        MouseEnterTerrainTile(hit.collider);
                    }

                    //Debug.Log("3D hit: " + hit.collider.tag);
                    if (lastCollider != null)
                    {
                        //Debug.Log("No longer hitting: " + lastCollider);
                        if (lastCollider.CompareTag("TerrainTile"))
                        {
                            MouseExitTerrainTile(lastCollider);
                        }
                    }
                }
                lastCollider = hit.collider;
            } else
            {
                //Debug.Log("No longer hitting: " + lastCollider);
                if (lastCollider.CompareTag("TerrainTile"))
                {
                    MouseExitTerrainTile(lastCollider);
                }
                lastCollider = null;
            }
        } else
        {
            if (lastCollider != null)
            {
                //Debug.Log("No longer hitting: " + lastCollider);
                if (lastCollider.CompareTag("TerrainTile"))
                {
                    MouseExitTerrainTile(lastCollider);
                }
                lastCollider = null;
            }
        }

        
        


    }

    public void MouseEnterTerrainTile(Collider collider)
    {
        collider.GetComponent<TerrainTile>().MouseHover();
    }

    public void MouseExitTerrainTile(Collider collider)
    {
        collider.GetComponent<TerrainTile>().MouseExit();
    }

    private bool IsMouseOverUI(Vector2 mousePosition)
    {
        pointerData.position = mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (var result in results)
        {
            //Debug.Log("Result=" + result);
            if (result.gameObject.layer == LayerMask.NameToLayer("UI"))
            {
                return true;
            }
        }

        return false;
    }

}
