using System;
using UnityEngine;

public class Camerahandler : MonoBehaviour
{
    private CameraController controller;

    [SerializeField] private Transform cameraContainer;
    [SerializeField] private float minXLook;
    [SerializeField] private float maxXLook;
    private float camCurXRot;
    [SerializeField] private float lookSensitivity;
    private Vector2 mouseDelta;

    private void Awake()
    {
        controller = GetComponent<CameraController>();
    }

    private void Start()
    {
        controller.OnLookEvent += GetDelta;
    }

    private void LateUpdate()
    {
        if (GameManager.Instance.isMouseLocked)
            CameraLook();
    }

    private void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    public void GetDelta(Vector2 delta)
    {
        mouseDelta = delta;
    }
}