using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PanCameraRig : MonoBehaviour
{
    MouseInput mouseInput;
    public float speed = 10f;
    private bool panEnabled = false;

    public void Awake()
    {
        mouseInput = new MouseInput();
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
        mouseInput.Mouse.EnablePanCamera.performed += context => PanPressed(context);
        mouseInput.Mouse.PanCamera.performed += context => Pan(context);
    }
    
    public void PanPressed(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<float>();
        //Debug.Log("Pan pressed: " + value);

        if (value <= 0)
        {
            panEnabled = false;
        } else
        {
            panEnabled = true;
        }
        
    }

    public void Pan(InputAction.CallbackContext context)
    {
        if (!panEnabled)
        {
            return;
        }

        Vector2 mouseDelta = context.ReadValue<Vector2>();
        Debug.Log("mouseDelta: " + mouseDelta);
        Vector3 panVector = new Vector3(-mouseDelta.x * Time.deltaTime * speed, 0, -mouseDelta.y * Time.deltaTime * speed);
        Debug.Log("panVector: " + panVector);

        //transform.Translate(panVector);

        float facing = Camera.main.transform.eulerAngles.y;
        Debug.Log("Facing: " + facing);

        Vector3 adjustedPanVector = Quaternion.Euler(0, facing, 0) * panVector;
        Debug.Log("adjustedPanVector: " + adjustedPanVector);

        transform.Translate(adjustedPanVector);



        //transform.Translate(new Vector3(panValue.x * Time.deltaTime * speed, 0, panValue.y * Time.deltaTime * speed));

    }
}
