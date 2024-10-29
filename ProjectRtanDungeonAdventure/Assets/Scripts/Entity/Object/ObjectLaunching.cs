using System.Collections.Generic;
using UnityEngine;

public class ObjectLaunching : MonoBehaviour
{
    private static readonly int launched = Animator.StringToHash("launched");

    [SerializeField] private Vector3 launchDirection;
    [SerializeField] private float launchPower;
    [SerializeField] private float launchTime;
    [SerializeField] private float launchDelay;
    [SerializeField] private LayerMask targetLayers;

    private float launchTimer;

    [SerializeField] private List<GameObject> passengers = new List<GameObject>();
    private bool isPassengerExist = false;

    private void Start()
    {
        launchTimer = 0.0f;
    }

    private void FixedUpdate()
    {
        if (launchTimer > 0)
        {
            if (isPassengerExist)
            {
                launchTimer += Time.deltaTime;

                if (launchTimer >= launchTime)
                {
                    foreach(GameObject passenger in passengers)
                        LaunchPassenger(passenger);

                    launchTimer = -launchDelay;
                }
            }
        }

        else
            launchTimer += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((targetLayers & (1 << collision.gameObject.layer)) != 0)
        {
            if (!passengers.Contains(collision.gameObject))
            {
                passengers.Add(collision.gameObject);
                isPassengerExist = true;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if ((targetLayers & (1 << collision.gameObject.layer)) != 0)
        {
            if (passengers.Contains(collision.gameObject))
            {
                passengers.Remove(collision.gameObject);

                if (passengers.Count <= 0)
                {
                    isPassengerExist = false;
                    launchTimer = 0.0f;
                }
            }
        }
    }

    public void LaunchPassenger(GameObject passenger)
    {
        CharacterInfo targetInfo = passenger.GetComponent<CharacterInfo>();
        Rigidbody targetRigid = passenger.GetComponent<Rigidbody>();

        if (targetInfo != null)
        {
            targetInfo.isMovable = false;
            targetInfo.MoveBlockTime = 0.2f;
        }

        if(targetRigid != null)
        {
            targetRigid.AddForce(launchDirection.normalized * launchPower, ForceMode.VelocityChange);
        }
    }
}