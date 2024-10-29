using UnityEngine;

public class ObjectStrike : MonoBehaviour
{
    [SerializeField] private int strikeDamage;
    [SerializeField] private float strikePower;
    [SerializeField] private LayerMask targetLayers;

    private void OnCollisionEnter(Collision collision)
    {
        if ((targetLayers & (1 << collision.gameObject.layer)) != 0)
        {
            Rigidbody targetRigid = collision.gameObject.GetComponent<Rigidbody>();
            CharacterInfo targetInfo = collision.gameObject.GetComponent<CharacterInfo>();

            if (targetInfo != null)
            {
                targetInfo.GetDamage(strikeDamage);
                targetInfo.isMovable = false;
                targetInfo.MoveBlockTime = 0.2f;
            }

            if (targetRigid != null)
            {
                Vector3 transOrigin = transform.position + (transform.forward * 16.5f);

                targetRigid.AddForce(((collision.transform.position - transOrigin).normalized + Vector3.up) * strikePower, ForceMode.VelocityChange);
            }
        }
    }
}