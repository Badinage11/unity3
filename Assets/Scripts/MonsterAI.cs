using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    public Transform[] patrolPoints; // 巡逻点数组
    public float chaseRange = 10f; // 追击范围
    public float chaseSpeed = 5f; // 追击速度

    private Transform target; // 目标玩家对象
    private int currentPatrolIndex = 0; // 当前巡逻点索引

    private void Start()
    {
        // 初始化目标玩家对象（可以通过标签或其他方式获取）
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // 计算怪物与玩家之间的距离
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget <= chaseRange)
        {
            // 在追击范围内，进行追击行为
            ChaseTarget();
        }
        else
        {
            // 不在追击范围内，进行巡逻行为
            Patrol();
        }
    }

    private void ChaseTarget()
    {
        // 计算朝向目标的方向
        Vector3 direction = (target.position - transform.position).normalized;

        // 移动向目标方向
        transform.position += direction * chaseSpeed * Time.deltaTime;
    }

    private void Patrol()
    {
        // 获取当前巡逻点
        Transform currentPatrolPoint = patrolPoints[currentPatrolIndex];

        // 计算朝向巡逻点的方向
        Vector3 direction = (currentPatrolPoint.position - transform.position).normalized;

        // 移动向巡逻点方向
        transform.position += direction * chaseSpeed * Time.deltaTime;

        // 如果到达巡逻点，选择下一个巡逻点
        float distanceToPatrolPoint = Vector3.Distance(transform.position, currentPatrolPoint.position);
        if (distanceToPatrolPoint < 0.1f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }
    }
}