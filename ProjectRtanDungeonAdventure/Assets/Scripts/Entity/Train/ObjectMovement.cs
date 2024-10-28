using System;
using UnityEngine;

public enum POSITION
{
    NorthEast,
    NorthWest,
    SouthEast,
    SouthWest
}

public class ObjectMovement : MonoBehaviour
{
    [SerializeField] private float velocity;
    private Rigidbody rigid;

    [SerializeField] private float initX;
    [SerializeField] private float initY;
    [SerializeField] private float initZ;

    private bool isOnMove;
    public event Action<POSITION> OnMoveEvent;
    public event Action OnMovingEvent;
    public event Action OnArriveEvent;
    private float MovingEventTime;
    private float MovingEventRate;

    private POSITION pos;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        isOnMove = false;
        StartMove();
    }

    private void FixedUpdate()
    {
        transform.position += transform.forward * velocity * Time.deltaTime;

        if (Time.time - MovingEventTime > MovingEventRate)
        {
            MovingEventTime = Time.time;
            OnMovingEvent?.Invoke();
        }

        if (Mathf.Abs(transform.position.x) > initX * 1.25f)
            OnDestination();
    }

    public void InitTrain()
    {
        switch (UnityEngine.Random.Range(0, 4))
        {
            case 0:
                pos = POSITION.NorthEast;
                transform.position = new Vector3(-initX, initY, -initZ);
                transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
                break;
            case 1:
                pos = POSITION.NorthWest;
                transform.position = new Vector3(initX, initY, -initZ);
                transform.rotation = Quaternion.Euler(0.0f, -90.0f, 0.0f);
                break;
            case 2:
                pos = POSITION.SouthEast;
                transform.position = new Vector3(-initX, initY, initZ);
                transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
                break;
            case 3:
                pos = POSITION.SouthWest;
                transform.position = new Vector3(initX, initY, initZ);
                transform.rotation = Quaternion.Euler(0.0f, -90.0f, 0.0f);
                break;
        }
    }

    public void StartMove()
    {
        InitTrain();
        isOnMove = true;
        OnMoveEvent?.Invoke(pos);

        MovingEventTime = Time.time;
        OnMovingEvent?.Invoke();
    }

    private void OnDestination()
    {
        isOnMove = false;
        OnArriveEvent?.Invoke();

        StartMove();
    }
}
