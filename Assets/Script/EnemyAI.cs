using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent; // ��������
    public Transform player; // ���λ��
    public LayerMask groundLayer, playerLayer;

    // Ѳ�ߵ�
    public Vector3 patrolPoint;
    private bool patrolPointSet;
    public float patrolRange;

    // ����
    public float timeBetweenAttacks;
    private bool alreadyAttacked;

    // ��Ұ��Χ
    public float sightRange, attackRange;
    private bool playerInSightRange, playerInAttackRange;

    void Start()
    {
        // ��ȡ��������
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // ������λ��
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        if (!playerInSightRange && !playerInAttackRange) Patrol();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
    }

    void Patrol()
    {
        if (!patrolPointSet) SetPatrolPoint();

        if (patrolPointSet)
            agent.SetDestination(patrolPoint);

        Vector3 distanceToPatrolPoint = transform.position - patrolPoint;

        // ����Ѳ�ߵ�
        if (distanceToPatrolPoint.magnitude < 1f)
            patrolPointSet = false;
    }

    void SetPatrolPoint()
    {
        // �������Ѳ�ߵ�
        float randomZ = Random.Range(-patrolRange, patrolRange);
        float randomX = Random.Range(-patrolRange, patrolRange);

        patrolPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(patrolPoint, -transform.up, 2f, groundLayer))
            patrolPointSet = true;
    }

    void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    void AttackPlayer()
    {
        // ֹͣ�ƶ�
        agent.SetDestination(transform.position);

        if (!alreadyAttacked)
        {
            // �ڴ˴���ӹ����߼�����������������ֵ��
            Debug.Log("Enemy attacks!");

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        // ������Ұ�͹�����Χ
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
