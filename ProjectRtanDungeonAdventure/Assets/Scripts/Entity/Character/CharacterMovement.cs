using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    protected CharacterInfo characterInfo;
    protected CharacterController characterController;
    protected Rigidbody rigid;

    protected Vector2 moveDir;

    private void Awake()
    {
        characterInfo = GetComponent<CharacterInfo>();
        characterController = GetComponent<CharacterController>();
        rigid = GetComponent<Rigidbody>();
    }

    protected virtual void Start()
    {
        characterController.OnMoveEvent += GetMoveDir;
    }

    protected virtual void FixedUpdate()
    {
        if(characterInfo.isMovable)
            Move();

        if (transform.position.y < 0)
        {
            characterInfo.GetDamage(25);
            transform.position = new Vector3(UnityEngine.Random.Range(-35.0f, 35.0f), 8.5f, UnityEngine.Random.Range(-35.0f, 35.0f));
        }
    }

    protected virtual void Move()
    {
        Vector3 dir = new Vector3(moveDir.x, 0.0f, moveDir.y);
        transform.forward = dir;

        if(moveDir.magnitude > 0.5f)
            rigid.velocity = transform.forward * characterInfo.moveSpeed;
        else
            rigid.velocity = Vector3.zero;
    }

    protected virtual void GetMoveDir(Vector2 vector)
    {
        if (characterInfo.isMovable)
            moveDir = vector;

        else
            moveDir = Vector2.zero;
    }
}
