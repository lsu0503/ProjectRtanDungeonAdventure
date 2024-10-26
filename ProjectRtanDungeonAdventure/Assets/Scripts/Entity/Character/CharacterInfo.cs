using Unity.VisualScripting;
using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
    public StatusGauge health;

    public float moveSpeed;

    protected bool isOnGround;
    public LayerMask groundLayerMask;
    [SerializeField] private float groundCheckRate;
    private float groundCheckTime;

    protected virtual void Start()
    {
        health.Initailize();
    }

    protected virtual void FixedUpdate()
    {
        health.Recover();

        if (Time.time - groundCheckTime > groundCheckRate)
            GroundCheck();
    }

    public bool GroundCheck()
    {
        float xPos = transform.position.x;
        float yPos = transform.position.y;
        float zPos = transform.position.z;

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

        for(int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast((rays[i]), 0.3f, groundLayerMask))
            {
                isOnGround = true;
                return true;
            }
        }

        isOnGround = false;
        return false;
    }
}
