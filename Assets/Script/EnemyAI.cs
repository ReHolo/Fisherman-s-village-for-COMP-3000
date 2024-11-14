using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent; // 导航代理
    public Transform player; // 玩家位置
    public LayerMask groundLayer, playerLayer;

    // 巡逻点
    public Vector3 patrolPoint;
    private bool patrolPointSet;
    public float patrolRange;

    // 攻击
    public float timeBetweenAttacks;
    private bool alreadyAttacked;

    // 视野范围
    public float sightRange, attackRange;
    private bool playerInSightRange, playerInAttackRange;

    void Start()
    {
        // 获取导航代理
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // 检测玩家位置
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

        // 到达巡逻点
        if (distanceToPatrolPoint.magnitude < 1f)
            patrolPointSet = false;
    }

    void SetPatrolPoint()
    {
        // 随机设置巡逻点
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
        // 停止移动
        agent.SetDestination(transform.position);

        if (!alreadyAttacked)
        {
            // 在此处添加攻击逻辑（例如减少玩家生命值）
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
        // 绘制视野和攻击范围
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
