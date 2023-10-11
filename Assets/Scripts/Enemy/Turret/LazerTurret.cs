using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LazerTurret : MonoBehaviour
{
    private TurretState currentState;
    [SerializeField]public float maxLazerDistance = 10f;
    public LayerMask obstacleLayer;

   

    [SerializeField]private LineRenderer lazerLine;

    public Transform shootPoint;


    public void Start()
    {
        currentState = new IdleState(this);
        
    }

    public void SetState(TurretState state)
    {
        currentState.OnStateExit();
        currentState = state;
        currentState.OnStateEnter();
    }
    public void SwitchToIdle()
    {
        SetState(new IdleState(this));
    }

    public void SwitchToAttack()
    {
        SetState(new AttackState(this, lazerLine));
    }

    public void Update()
    {
        currentState.OnStateUpdate();

    }
    

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(shootPoint.position, shootPoint.position + shootPoint.forward * maxLazerDistance);
    }
}

