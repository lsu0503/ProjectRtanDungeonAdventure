using UnityEngine;

public class WallChecker : MonoBehaviour
{
    private CharacterInfo characterInfo;

    private int wallGrade;
    public LayerMask targetLayers;
    [SerializeField] private float wallCheckRate;
    private float wallCheckTime;

    private Transform? wall;
    private Vector3 wallPosition;

    private void Awake()
    {
        characterInfo = GetComponent<CharacterInfo>();
    }

    private void FixedUpdate()
    {
        if (Time.time - wallCheckTime > wallCheckRate)
        {
            wallGrade = WallCheck();
        }
    }

    public int WallCheck()
    {
        float xPos = transform.position.x;
        float yPos = transform.position.y;
        float zPos = transform.position.z;

        RaycastHit hit;

        Ray[] rays = new Ray[]
        {
            new Ray(new Vector3(xPos, yPos + 0.3f, zPos), Vector3.forward),
            new Ray(new Vector3(xPos, yPos + 0.9f, zPos), Vector3.forward),
            new Ray(new Vector3(xPos, yPos + 1.5f, zPos), Vector3.forward)
        };

        int grade = 0;

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast((rays[i]), out hit, 0.3f, targetLayers))
            {
                grade = i;
            }
        }

        return grade;
    }
}