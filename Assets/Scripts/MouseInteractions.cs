using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class MouseInteractions : MonoBehaviour
{
    public TerrainClickUI terrainClickUI;

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
        mouseInput.Mouse.LeftClick.performed += context => MouseClicked(context);

        pointerData = new PointerEventData(EventSystem.current)
        {
            pointerId = -1,
        };
    }

    public void MouseClicked(InputAction.CallbackContext context)
    {
        if (!mouseOverUI)
        {
            terrainClickUI.Hide();
        }

        if (mouseOverTerrain)
        {
            lastCollider.GetComponent<TerrainTile>().MouseDown();
            terrainClickUI.ShowAtTile(lastCollider.gameObject);
        }
    }

    public void MouseMoved(InputAction.CallbackContext context)
    {
        mouseOverTerrain = false;

        Vector2 mousePosition = mouseInput.Mouse.MousePosition.ReadValue<Vector2>();

        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        mouseOverUI = IsMouseOverUI(mousePosition);

        if (mouseOverUI)
        {
            return;
        }


        if (Physics.Raycast(ray, out hit))
        {
            

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

                    if (lastCollider != null)
                    {
                        if (lastCollider.CompareTag("TerrainTile"))
                        {
                            MouseExitTerrainTile(lastCollider);
                        }
                    }
                }
                lastCollider = hit.collider;
            } else
            {
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
