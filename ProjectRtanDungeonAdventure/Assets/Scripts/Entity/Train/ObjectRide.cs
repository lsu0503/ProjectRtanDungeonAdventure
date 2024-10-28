using System.Collections.Generic;
using UnityEngine;

public class ObjectRide : MonoBehaviour
{
    [SerializeField] private LayerMask canRideMark;

    private void OnTriggerEnter(Collider other)
    {
        if ((canRideMark & (1 << other.gameObject.layer)) != 0)
            other.gameObject.transform.SetParent(transform, true);
    }

    private void OnTriggerExit(Collider other)
    {
        if ((canRideMark & (1 << other.gameObject.layer)) != 0)
            other.gameObject.transform.SetParent(null);
    }
}
