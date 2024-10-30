using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    private Transform playerTransform;
    private CharacterInfo info;
    private Animator animator;
    private NavMeshAgent agent;

    [SerializeField] private float attackDistance;
    [SerializeField] private float AttackAngle;
    [SerializeField] private float attackRate;
    [SerializeField] private int attackDamage;
    [SerializeField] private float attackImpulse;
    private float AttackedTime;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        info = GetComponent<CharacterInfo>();
        animator = GetComponent<Animator>();
        playerTransform = GameManager.Instance.playerInfo.gameObject.transform;
        AttackedTime = Time.time;
    }

    private void Update()
    {
        Vector3 playerFlatPosition = playerTransform.position;
        playerFlatPosition.y = transform.position.y;
        float playerDistance = Vector3.Distance(playerFlatPosition, transform.position);

        agent.speed = info.moveSpeed * (1 + (info.health.current / (float)info.health.max));
        animator.speed = (1 + (info.health.current / (float)info.health.max));
        float attackSpeed = attackRate / (1.0f + (info.health.current / (float)info.health.max));

        if (playerDistance <= attackDistance)
        {
            if (Time.time - AttackedTime > attackSpeed && CheckAttackAngle() && CheckHeight())
            {
                AttackedTime = Time.time;
                Attack();
            }

            else
            {
                Idle();
            }
        }

        else
            SetPath();

        if (transform.position.y < 0)
        {
            info.GetDamage(25);
            transform.position = new Vector3(UnityEngine.Random.Range(-35.0f, 35.0f), 8.5f, UnityEngine.Random.Range(-35.0f, 35.0f));
        }
    }

    private void Idle()
    {
        agent.SetDestination(transform.position);
        agent.isStopped = true;
        animator.SetBool("Moving", false);

        Vector3 direction = (playerTransform.position - transform.position).normalized;
        direction.y = 0.0f;
        transform.forward = direction;
    }

    private void SetPath()
    {
        agent.isStopped = false;
        animator.SetBool("Moving", true);

        NavMeshPath path = new NavMeshPath();
        if (agent.CalculatePath(playerTransform.position, path))
        {
            agent.SetDestination(playerTransform.position);
        }
    }

    private void Attack()
    {
        GameObject player = playerTransform.gameObject;
        Rigidbody targetRigid = player.GetComponent<Rigidbody>();

        agent.SetDestination(transform.position);
        agent.isStopped = true;

        animator.SetTrigger("Attack");
        GameManager.Instance.playerInfo.GetDamage(attackDamage);
        targetRigid.AddForce(Vector3.up * attackImpulse, ForceMode.VelocityChange);
    }

    private bool CheckAttackAngle()
    {
        Vector3 direction = playerTransform.position - transform.position;
        direction.y = transform.position.y;
        float angle = Vector3.Angle(transform.forward, direction);
        return angle < AttackAngle * 0.5f;
    }

    private bool CheckHeight()
    {
        if (playerTransform.position.y > transform.position.y + 1.0f)
            return false;

        else if (playerTransform.position.y < transform.position.y - 1.0f)
            return false;

        else
            return true;
    }
}