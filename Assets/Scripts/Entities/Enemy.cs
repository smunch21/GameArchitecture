using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform[] patrolPoints;
    [SerializeField] Transform enemyEye;
    [SerializeField] float playerCheckDistance;
    [SerializeField] float checkRadius = 0.4f;

    int currentPoint = 0;

    NavMeshAgent agent;

    public bool isIdle = true;
    public bool isPlayerFound;
    public bool isCloseToPlayer;

    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = patrolPoints[currentPoint].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isIdle)
        {
            Idle();
        }
        else if (isPlayerFound) 
        {
            if (isCloseToPlayer)
            {
                AttackPlayer();
            }
            else
            {
                FollowPlayer();
            }
        }
    }
    void Idle()
    {
        if(agent.remainingDistance < 0.1f)
        {
            currentPoint++;
            if(currentPoint >= patrolPoints.Length)
            {
                currentPoint = 0; 
            }
            agent.destination = patrolPoints[currentPoint].position;
        }
        if(Physics.SphereCast(enemyEye.position, checkRadius, transform.forward, out RaycastHit hit, playerCheckDistance))
        {
            if (hit.transform.CompareTag("Player"))
            {
                Debug.Log("PlayerFound");
                isIdle = false;
                isPlayerFound = true;
                player = hit.transform;
                agent.destination = player.position;
            }
        }
    }

    void FollowPlayer()
    {
        if(player != null)
        {
            if(Vector3.Distance(transform.position, player.position) > 20)
            {
                isPlayerFound = false;
                isIdle = true;
            }

            //Attack
            if (Vector3.Distance(transform.position, player.position) < 2)
            {
                isCloseToPlayer = true;
            }
            else
            {
                isCloseToPlayer = false; 
            }
            
            agent.destination = player.position;
        }
        else
        {
            isPlayerFound = false;
            isIdle = true;
            isCloseToPlayer = false;
        }
    }

    void AttackPlayer()
    {
        Debug.Log("attacking Player");
        if(Vector3.Distance(transform.position, player.position) > 2)
        {
            isCloseToPlayer = false;

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(enemyEye.position, checkRadius);
        Gizmos.DrawWireSphere(enemyEye.position + enemyEye.forward * playerCheckDistance, checkRadius);
        Gizmos.DrawLine(enemyEye.position, enemyEye.position + enemyEye.forward * playerCheckDistance);
    }
}
