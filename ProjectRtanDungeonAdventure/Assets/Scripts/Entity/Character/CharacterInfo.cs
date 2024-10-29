using Unity.VisualScripting;
using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
    public StatusGauge health;

    public float moveSpeed;
    public bool isOnGround;
    public bool isMovable;
    public float MoveBlockTime;

    protected virtual void Start()
    {
        health.Initailize();

        isMovable = true;
        MoveBlockTime = 0.0f;
    }

    protected virtual void FixedUpdate()
    {
        health.Recover();

        if (!isMovable && MoveBlockTime > 0)
            MoveBlockTime -= Time.deltaTime;
    }

    public virtual bool GetDamage(int amount)
    {
        bool result = health.Substract(amount);

        return result;
    }
}
