using Unity.VisualScripting;
using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
    public StatusGauge health;

    public float moveSpeed;
    public bool isOnGround;

    protected virtual void Start()
    {
        health.Initailize();
    }

    protected virtual void FixedUpdate()
    {
        health.Recover();
    }
}
