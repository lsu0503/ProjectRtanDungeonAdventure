using UnityEngine;

public class ObjectStrike : MonoBehaviour
{
    [SerializeField] private int strikeDamage;
    [SerializeField] private float strikePower;
    [SerializeField] private LayerMask targetLayers;

    private void OnTriggerEnter(Collider other)
    {
        if ((targetLayers & (1 << other.gameObject.layer)) != 0)
        {
            Rigidbody targetRigid = other.gameObject.GetComponent<Rigidbody>();
            CharacterInfo targetInfo = other.gameObject.GetComponent<CharacterInfo>();

            if (targetInfo != null)
            {
                int resultDamage = (int)(strikeDamage * (1 - (GameManager.Instance.trainCalm / 100.0f)));
                targetInfo.GetDamage(resultDamage);
                GameManager.Instance.GetTrainCalm(resultDamage);

                targetInfo.isMovable = false;
                targetInfo.MoveBlockTime = 0.2f;

            }

            if (targetRigid != null)
            {
                Vector3 transOrigin = transform.position;
                transOrigin.y = 4.2f;

                float resultPower = strikePower * (1 - (GameManager.Instance.trainCalm / 100.0f));

                targetRigid.AddForce(((other.transform.position - transOrigin).normalized + Vector3.up) * resultPower, ForceMode.VelocityChange);
            }
        }
    }
}