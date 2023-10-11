using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : TurretState
{
    public IdleState(LazerTurret _turret) : base(_turret)
    {
    }
    
    private float idleTimer;
    private float idleDuration = 2.0f; // Example: Adjust the duration as needed


    public override void OnStateEnter()
    {
        // Enter Idle state logic
        idleTimer = 0f;
    }

    public override void OnStateUpdate()
    {
       
        idleTimer += Time.deltaTime;
        if (idleTimer >= idleDuration)
        {
            turret.SwitchToAttack();
        }
    }

    public override void OnStateExit()
    {
        // Exit Idle state logic
    }

    
}
