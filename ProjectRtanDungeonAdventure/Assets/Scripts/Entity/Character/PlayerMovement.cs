using UnityEngine;

public class PlayerMovement : CharacterMovement
{
    private PlayerInfo playerInfo;
    [SerializeField] private Transform cameraTransform;

    private bool isSprint;
    private float sprintCounter = float.MaxValue;

    private bool isOnDash;
    private bool isOnGround;
    private float DashCounter;
    private float DashTimeMax;

    protected override void Start()
    {
        base.Start();
        (characterController as PlayerController).OnJumpEvent += TryJump;
        (characterController as PlayerController).OnDashEvent += TryDash;
        (characterController as PlayerController).OnSprintEvent += SprintTrigger;

        playerInfo = characterInfo as PlayerInfo;
    }

    protected override void FixedUpdate()
    {
        if (playerInfo.isMovable)
        {
            if (isOnDash)
            {
                Dash();

                DashCounter += Time.deltaTime;
                if (DashCounter >= DashTimeMax)
                {
                    isOnDash = false;
                    DashCounter = 0.0f;
                }
            }

            else
                Move();
        }

        if (transform.position.y < 0)
        {
            characterInfo.GetDamage(25);
            transform.position = new Vector3(Random.Range(-35.0f, 35.0f), 8.5f, Random.Range(-35.0f, 35.0f));
        }
    }

    protected override void GetMoveDir(Vector2 vector)
    {
        if (!isOnDash)
        {
            base.GetMoveDir(vector);
        }

    }

    private void TryJump()
    {
        if (playerInfo.isMovable)
        {
            if (characterInfo.isOnGround)
            {
                if (playerInfo.stamina.Substract(playerInfo.jumpCostOnGound))
                    rigid.AddForce(Vector2.up * playerInfo.jumpPowerOnGround, ForceMode.Impulse);
            }

            else
            {
                if (playerInfo.stamina.Substract(playerInfo.jumpCostInAir))
                {
                    if (rigid.velocity.y >= 0)
                        rigid.AddForce(Vector2.up * playerInfo.jumpPowerInAir, ForceMode.Impulse);

                    else
                        rigid.velocity = new Vector3(rigid.velocity.x, playerInfo.jumpPowerInAir / rigid.mass, rigid.velocity.z);
                }
            }
        }
    }

    private void TryDash()
    {
        if (playerInfo.isMovable)
        {
            if (!isOnDash && moveDir.magnitude > 0.5f)
            {
                if (characterInfo.isOnGround)
                {
                    if (playerInfo.stamina.Substract(playerInfo.dashCostOnGround))
                    {
                        DashTimeMax = playerInfo.dashTimeOnGround;
                        isOnGround = true;
                        isOnDash = true;
                    }
                }

                else
                {
                    if (playerInfo.stamina.Substract(playerInfo.dashCostInAir))
                    {
                        DashTimeMax = playerInfo.dashTimeInAir;
                        isOnGround = false;
                        isOnDash = true;
                    }
                }
            }
        }
    }

    private void SprintTrigger(bool isOn)
    {
        if (playerInfo.isMovable)
        {
            isSprint = isOn;

            if (!isOn)
            {
                sprintCounter = float.MaxValue;
                playerInfo.isOnSprint = false;
            }
        }

        else
        {
            isSprint = false;

            sprintCounter = float.MaxValue;
            playerInfo.isOnSprint = false;
        }
    }

    protected override void Move()
    {
        Vector3 velocityDir;

        if (moveDir.magnitude > 0.5f)
        {
            Vector3 dir = cameraTransform.forward * moveDir.y;
            dir.y = 0.0f;
            dir = dir.normalized;
            dir += cameraTransform.right * moveDir.x;
            transform.forward = dir;

            float speedMultiplyer = 1.0f;
            if (isSprint && moveDir.magnitude > 0.5f && playerInfo.isOnGround)
            {
                sprintCounter += Time.deltaTime;
                speedMultiplyer *= playerInfo.sprintSpeed;

                if (sprintCounter > playerInfo.sprintCostRate)
                {
                    if (!playerInfo.stamina.Substract(1))
                    {
                        speedMultiplyer /= playerInfo.sprintSpeed;
                        playerInfo.isOnSprint = false;
                    }

                    else
                    {
                        sprintCounter = 0.0f;
                        playerInfo.isOnSprint = true;
                    }
                }
            }

            velocityDir = transform.forward * characterInfo.moveSpeed * speedMultiplyer;
        }

        else
        {
            velocityDir = Vector3.zero;
        }

        velocityDir.y = rigid.velocity.y;
        rigid.velocity = velocityDir;
    }

    private void Dash()
    {
        Vector3 dir = transform.forward * moveDir.y + transform.right * moveDir.x;
        float dirSpeed = (0.7f + (moveDir.y * 0.3f));

        if (isOnGround)
            dir *= playerInfo.dashSpeedOnGround * dirSpeed;

        else
            dir *= playerInfo.dashSpeedInAir * dirSpeed;

        dir.y = rigid.velocity.y;
        rigid.velocity = dir;
    }
}