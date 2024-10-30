using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Camerahandler : MonoBehaviour
{
    private CameraController controller;
    private Transform playerTransform;

    [SerializeField] private float minXLook;
    [SerializeField] private float maxXLook;

    [SerializeField] private Vector3 cameraPosition;
    private float camCurXRot;
    private float camCurYRot;
    [SerializeField] private float lookSensitivity;
    private Vector2 mouseDelta;

    private void Awake()
    {
        controller = GetComponent<CameraController>();
    }

    private void Start()
    {
        playerTransform = GameManager.Instance.playerInfo.gameObject.transform;
        controller.OnLookEvent += GetDelta;
        camCurXRot = 0.0f;
        camCurYRot = -90.0f;
    }

    private void LateUpdate()
    {
        transform.position = playerTransform.position + cameraPosition;
        if (GameManager.Instance.isMouseLocked)
            CameraLook();
    }

    private void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);

        camCurYRot += mouseDelta.x * lookSensitivity;

        transform.eulerAngles = new Vector3(-camCurXRot, camCurYRot, 0.0f);
    }

    public void GetDelta(Vector2 delta)
    {
        mouseDelta = delta;
    }
}