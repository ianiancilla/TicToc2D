using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] bool zoomedOut = false;
    [SerializeField] [Tooltip("Orthographic size of the camera when zoomed in")]
                     float zoomedInSize = 4.06f;
    [SerializeField] [Tooltip("Orthographic size of the camera when zoomed out")]
                     float zoomedOutSize = 10f;
    [SerializeField] float zoomSpeed = 1f;

    // cache
    float orthographicSize;
    CinemachineVirtualCamera virtualCamera;

    private void Start()
    {
        // cache
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }
    private void Update()
    {
        var deltaZoom = zoomSpeed * Time.deltaTime;
        if (zoomedOut)
        {
            virtualCamera.m_Lens.OrthographicSize = Mathf.Clamp(virtualCamera.m_Lens.OrthographicSize + deltaZoom,
                                                                virtualCamera.m_Lens.OrthographicSize,
                                                                zoomedOutSize);
        }
        else
        {
            virtualCamera.m_Lens.OrthographicSize = Mathf.Clamp(virtualCamera.m_Lens.OrthographicSize - deltaZoom,
                                                                zoomedInSize,
                                                                virtualCamera.m_Lens.OrthographicSize);
        }
    }

    public void ZoomIn()
    {
        zoomedOut = false;
    }

    public void ZoomOut()
    {
        zoomedOut = true;
    }
}
