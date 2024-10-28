using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionPointer : MonoBehaviour
{
    [SerializeField] private float checkRate = 0.05f;
    private float lastCheckTime = 0.0f;
    [SerializeField] private float distanceMax;
    [SerializeField] private LayerMask targetLayers;

    private PlayerInfo playerInfo;

    private GameObject curObj;
    private IInteractable curInteract;

    [SerializeField] private PromptText promptText;
    private Camera targetCamera;

    private void Start()
    {
        targetCamera = Camera.main;
        playerInfo = PlayerManager.Instance.playerInfo;
    }

    private void FixedUpdate()
    {
        if(Time.time - lastCheckTime < checkRate)
        {
            lastCheckTime = Time.time;
            CheckScreen();
        }
    }

    private void CheckScreen()
    {
        Ray ray = targetCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, distanceMax, targetLayers))
        {
            if(hit.collider.gameObject != curObj)
            {
                curObj = hit.collider.gameObject;

                curInteract = curObj.GetComponent<IInteractable>();

                if (curInteract != null)
                    promptText.SetPromptText(curInteract.GetItemData());
            }
        }

        else
        {
            curObj = null;
            promptText.UnsetPrompt();
        }
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started && curInteract != null)
        {
            curInteract.OnInteract();
        }
    }
}
