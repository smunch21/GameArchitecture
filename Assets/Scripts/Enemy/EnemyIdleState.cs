using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    int currentPoint = 0;
    

    public EnemyIdleState(EnemyController _enemy) : base(_enemy)
    {

    }
    public override void OnStateEnter()
    {
        enemy.agent.destination = enemy.patrolPoints[currentPoint].position;
        Debug.Log("Enemy is patroling");
    }

    public override void OnStateExit()
    {
        Debug.Log("Enemy is patroling");
    }

    public override void OnStateUpdate()
    {
        if (enemy.agent.remainingDistance < 0.1f)
        {
            currentPoint++;
            if (currentPoint >= enemy.patrolPoints.Length)
            {
                currentPoint = 0;
            }
            enemy.agent.destination = enemy.patrolPoints[currentPoint].position;
        }
        if (Physics.SphereCast( enemy.enemyEye.position, enemy.checkRadius, enemy.transform.forward, out RaycastHit hit, enemy.playerCheckDistance))
        {
            if (hit.transform.CompareTag("Player"))
            {
                Debug.Log("PlayerFound");

                enemy.player = hit.transform;
                enemy.agent.destination = enemy.player.position;
                enemy.ChangeState(new EnemyFollowState(enemy));
            }
        }
    }
}
