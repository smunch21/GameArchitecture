using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowState : EnemyState
{
    float distanceToPlayer;
    public EnemyFollowState(EnemyController _enemy) : base(_enemy)
    {

    }
    public override void OnStateEnter()
    {
        Debug.Log("Enemy Following Player");
    }

    public override void OnStateExit()
    {
        Debug.Log("Enemy stoped following");
    }

    public override void OnStateUpdate()
    {
        if (enemy.player != null)
        {
            distanceToPlayer = Vector3.Distance(enemy.transform.position, enemy.player.position);
            if (distanceToPlayer > 20)
            {
                enemy.ChangeState(new EnemyIdleState(enemy));
            }

            //Attack
            if (distanceToPlayer < 2)
            {
                enemy.ChangeState(new EnemyAttackState(enemy));
            }
       
            enemy.agent.destination = enemy.player.position;
        }
        else
        {
            enemy.ChangeState(new EnemyIdleState(enemy));
        }
    }
}
