using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform cameraTransform;

    public float panAmount = 5f;
    public float panTime = 10f;

    public float rotationAmount = 5f;
    public float rotationTime = 10f;

    public Vector3 zoomAmount = new(0, -10, 10);
    public float zoomTime = 30f;

    // TODO: Add limits to pan and zoom


    void LateUpdate()
    {
        HandleMouseInput();
    }

    private void HandleMouseInput()
    {
        // Middle click + drag => Pan
        if (Input.GetMouseButton(2))
        {
            PanCamera();
        }

        // Right click + drag => Rotate
        if (Input.GetMouseButton(1))
        {
            RotateCamera();
        }

        // Mouse scroll => Zoom
        if (Input.mouseScrollDelta.y != 0)
        {
            ZoomCamera();
        }
    }

    private void PanCamera()
    {
        // TODO: Dependent on rotation?
        Vector3 newPosition = transform.position - new Vector3(Input.GetAxis("Mouse X") * panAmount, 0, Input.GetAxis("Mouse Y") * panAmount);
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * panTime);
    }

    private void RotateCamera()
    {
        Quaternion newRotation = transform.rotation * Quaternion.Euler(Vector3.up * (Input.GetAxis("Mouse X") * rotationAmount));
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * rotationTime);
    }

    private void ZoomCamera()
    {
        // TODO: Add rotation to zoom
        Vector3 newZoom = cameraTransform.localPosition + (Input.mouseScrollDelta.y * zoomAmount);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * zoomTime);
    }


}
