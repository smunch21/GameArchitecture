using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    float distanceToPlayer;
    Health playerHealth;
    float damagePerSecond = 20f;

    public EnemyAttackState(EnemyController _enemy) : base(_enemy)
    {
        playerHealth = _enemy.player.GetComponent<Health>();
    }
    public override void OnStateEnter()
    {
        Debug.Log("Enemy is Attacking the player");
        
    }

    public override void OnStateExit()
    {
        Debug.Log("Enemy stopped attacking the player");
    }

    public override void OnStateUpdate()
    {
        Attack();
        if (enemy.player != null)
        {
            distanceToPlayer = Vector3.Distance(enemy.transform.position, enemy.player.position);
            if(distanceToPlayer > 2) 
            {
                enemy.ChangeState(new EnemyFollowState(enemy));
            }

            enemy.agent.destination = enemy.player.position;
        }
        else
        {
            enemy.ChangeState(new EnemyFollowState(enemy));
        }
    }

    void Attack()
    {
        if(playerHealth != null)
        {
            playerHealth.DeductHealth(damagePerSecond * Time.deltaTime);
        }
    }
}
