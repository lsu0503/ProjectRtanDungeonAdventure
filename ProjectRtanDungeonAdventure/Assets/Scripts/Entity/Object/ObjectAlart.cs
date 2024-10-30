using UnityEngine;

public class ObjectAlart : MonoBehaviour
{
    [SerializeField] private GameObject alartText;
    [SerializeField] private float alartRate;
    private float alartTime;
    [SerializeField] private float alartLength;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private float alartVertexTerm_X;
    [SerializeField] private int alartVertexAmount_X;
    [SerializeField] private float alartVertexTerm_Y;
    [SerializeField] private int alartVertexAmount_Y;

    private void FixedUpdate()
    {
        if (Time.time - alartTime > alartRate)
        {
            alartTime = Time.time;

            if (CheckPlayerOnWay())
                alartText.SetActive(true);

            else
                alartText.SetActive(false);
        }
    }

    private bool CheckPlayerOnWay()
    {


        for(int i = 0; i < alartVertexAmount_X; i++)
        {
            float xPos = (-(alartVertexAmount_X - 1) / 2 + i) * alartVertexTerm_X;

            for (int j = 0; j < alartVertexAmount_Y; j++)
            {
                float yPos = (-(alartVertexAmount_Y - 1) / 2 + j) * alartVertexTerm_Y;

                if (Physics.Raycast(transform.position + (transform.right * xPos) + (transform.up * yPos), transform.forward, alartLength, targetLayer))
                {
                    return true;
                }
            }
        }

        return false;
    }
}