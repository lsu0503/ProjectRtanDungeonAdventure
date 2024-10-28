using System;
using UnityEngine;

public class ObjectLauncher : MonoBehaviour
{
    [SerializeField] private Vector3 direction;
    [SerializeField] private float power;
    [SerializeField] private LayerMask targetLayers;

    public event Action OnPushEvent;

    private void OnCollisionEnter(Collision collision)
    {
        if ((targetLayers & (1 << collision.gameObject.layer)) != 0)
        {
            Rigidbody rigidTarget = collision.gameObject.GetComponent<Rigidbody>();

            if (rigidTarget != null)
                rigidTarget.AddForce(direction.normalized * power, ForceMode.Impulse);
        }
    }
}