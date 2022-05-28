using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ZoomVirtualCamera : MonoBehaviour
{
    public float yFollowMin = 15f;
    public float zFollowMin = 20f;
    public float yFollowMax = 120f;
    public float zFollowMax = 100f;

    public float zoomSpeed = 750f;

    private CinemachineInputProvider inputProvider;
    private CinemachineVirtualCamera virtualCamera;
    private Transform cameraTransform;
    private CinemachineOrbitalTransposer transposer;

    private void Awake()
    {
        inputProvider = GetComponent<CinemachineInputProvider>();
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        cameraTransform = virtualCamera.VirtualCameraGameObject.transform;
        transposer = virtualCamera.GetCinemachineComponent<CinemachineOrbitalTransposer>();
    }

    void Update()
    {

    }


    public void Zoom()
    {
        float zoomDirection = inputProvider.GetAxisValue(2);
        Vector3 currentOffset = transposer.m_FollowOffset;

        //Debug.Log("Current offset: " + currentOffset);

        float yNew;
        float zNew;

        if (zoomDirection < 0)
        {
            yNew = currentOffset.y + (zoomSpeed * Time.deltaTime);
            zNew = currentOffset.z - (zoomSpeed * Time.deltaTime);
            yNew = Mathf.Clamp(yNew, yFollowMin, yFollowMax);
            zNew = -Mathf.Clamp(-zNew, zFollowMin, zFollowMax);
            

        } else if (zoomDirection > 0)
        {
            yNew = currentOffset.y - (zoomSpeed * Time.deltaTime);
            zNew = currentOffset.z + (zoomSpeed * Time.deltaTime);
            yNew = Mathf.Clamp(yNew, yFollowMin, yFollowMax);
            zNew = -Mathf.Clamp(-zNew, zFollowMin, zFollowMax);
        } else
        {
            return;
        }

        transposer.m_FollowOffset = new Vector3(currentOffset.x, yNew, zNew);
    }
}
