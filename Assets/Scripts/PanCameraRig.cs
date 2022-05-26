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
        mouseInput.Mouse.EnablePanCamera.performed += ctx => PanPressed(ctx);
        //mouseInput.Mouse.EnablePanCamera.canceled += _ => DisablePan();
        mouseInput.Mouse.PanCamera.performed += ctx => Pan(ctx);
    }

    public void PanPressed(InputAction.CallbackContext input)
    {
        //panEnabled = input.ReadValue<bool>().isPressed;
        //Debug.Log(input);
        float value = input.ReadValue<float>();
        Debug.Log("Pan pressed: " + value);

        if (value <= 0)
        {
            panEnabled = false;
        } else
        {
            panEnabled = true;
        }
        
    }

    public void Pan(InputAction.CallbackContext input)
    {
        if (panEnabled)
        {
            Debug.Log("Panning: " + input.ReadValue<Vector2>());
            Vector2 panValue = input.ReadValue<Vector2>();
            transform.Translate(new Vector3(panValue.x * Time.deltaTime * speed, 0, panValue.y * Time.deltaTime * speed));
        }
            //Debug.Log("Panning: " + input);
            


        //PlayerInput input = GameManager.instance.GetComponent<PlayerInput>();
        //input.GetComponent<>
    }
}
