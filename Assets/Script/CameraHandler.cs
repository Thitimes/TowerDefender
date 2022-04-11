using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    private float orthoGraphicSize;
    private float targetOrthoGraphicSize;
    float moveSpeed = 12f;
    float zoomAmount = 2f;
    float zoomSpeed = 5f;
    private void Start()
    {
        orthoGraphicSize = cinemachineVirtualCamera.m_Lens.OrthographicSize;
        targetOrthoGraphicSize = orthoGraphicSize;
    }
    void Update()
    {
        Movement();
        Zoom();

        
      
    }

    void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = new Vector2(x, y).normalized;


        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }
    void Zoom()
    {
        targetOrthoGraphicSize += -Input.mouseScrollDelta.y * zoomAmount;

       targetOrthoGraphicSize = Mathf.Clamp(targetOrthoGraphicSize, 10, 30);

        orthoGraphicSize = Mathf.Lerp(orthoGraphicSize, targetOrthoGraphicSize, Time.deltaTime * zoomSpeed);

        cinemachineVirtualCamera.m_Lens.OrthographicSize = orthoGraphicSize;
    }
}
