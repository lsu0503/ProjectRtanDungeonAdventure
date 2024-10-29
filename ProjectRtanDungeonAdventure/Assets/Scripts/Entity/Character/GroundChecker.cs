using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    private CharacterInfo characterInfo;

    protected bool isOnGround;
    public LayerMask groundLayerMask;
    [SerializeField] private float groundCheckRate;
    private float groundCheckTime;

    private Transform? ground;
    private Vector3 groundPosition;

    private void Awake()
    {
        characterInfo = GetComponent<CharacterInfo>();
    }

    private void FixedUpdate()
    {
        if (Time.time - groundCheckTime > groundCheckRate)
        {
            if (GroundCheck())
            {
                characterInfo.isOnGround = true;
                isOnGround = true;

                if (!characterInfo.isMovable && characterInfo.MoveBlockTime <= 0)
                    characterInfo.isMovable = true;
            }

            else
            {
                characterInfo.isOnGround = false;
                isOnGround = false;
            }
        }

        if(ground != null)
        {
            if(ground.position != groundPosition)
            {
                Vector3 changePosition = ground.position - groundPosition;
                if (changePosition.y < 0.0f)
                    changePosition.y = 0.0f;

                transform.position += changePosition;
                groundPosition = ground.position;
            }
        }
    }

    public bool GroundCheck()
    {
        float xPos = transform.position.x;
        float yPos = transform.position.y;
        float zPos = transform.position.z;

        RaycastHit hit;

        Ray[] rays = new Ray[]
        {
            new Ray(new Vector3(xPos, yPos + 0.3f, zPos), Vector3.down),
            new Ray(new Vector3(xPos - 1.0f, yPos + 0.1f, zPos), Vector3.down),
            new Ray(new Vector3(xPos + 1.0f, yPos + 0.1f, zPos), Vector3.down),
            new Ray(new Vector3(xPos, yPos + 0.1f, zPos - 1.0f), Vector3.down),
            new Ray(new Vector3(xPos, yPos + 0.1f, zPos + 1.0f), Vector3.down),
            new Ray(new Vector3(xPos - 0.71f, yPos + 0.1f, zPos - 0.71f), Vector3.down),
            new Ray(new Vector3(xPos - 0.71f, yPos + 0.1f, zPos + 0.71f), Vector3.down),
            new Ray(new Vector3(xPos + 0.71f, yPos + 0.1f, zPos - 0.71f), Vector3.down),
            new Ray(new Vector3(xPos + 0.71f, yPos + 0.1f, zPos + 0.71f), Vector3.down),
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast((rays[i]), out hit, 0.3f, groundLayerMask))
            {
                if(ground != hit.transform)
                {
                    ground = hit.transform;
                    groundPosition = hit.transform.position;
                }

                return true;
            }
        }

        ground = null;
        groundPosition = Vector3.zero;

        return false;
    }
}
