using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


public class Pet : MonoBehaviour
{
    [Header("Stat")] 
    public float walkSpeed;
    public float runSpeed;
    
    [Header("AI")]
    private NavMeshAgent agent;
    public float detectDistance;
    private AIState aiState;
    
    [Header("Wandering")]
    public float minWanderDistance;
    public float maxWanderDistance;
    public float minWanderWaitTime;
    public float maxWanderWaitTime;
    
    [Header("Combat")] 
    public int damage;
    public float attackRate;
    private float lastAttackTime;
    public float attackDistance;
    private float playerDistance;
    
    public float fieldOfView;
    
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        SetState(AIState.Wandering);
    }

    private void Update()
    {
        playerDistance = Vector3.Distance(transform.position, CharacterManager.Instance.Player.transform.position);

        switch (aiState)
        {
            case AIState.Idle:
            case AIState.Wandering:
                PassiveUpdate();
                break;
            case AIState.Attacking:
                FollowUpdate();
                break;
        }
    }
    
    private void SetState(AIState state)
    {
        aiState = state;

        switch (aiState)
        {
            case AIState.Idle:
                agent.speed = walkSpeed;
                agent.isStopped = true;
                break;
            case AIState.Wandering:
                agent.speed = walkSpeed;
                agent.isStopped = false;
                break;
            case AIState.Attacking:
                agent.speed = runSpeed;
                agent.isStopped = false;
                break;
        }
    }

    private void PassiveUpdate()
    {
        if (aiState == AIState.Wandering && agent.remainingDistance <= 0.1f)
        {
            SetState(AIState.Idle);
            Invoke("WanderToNewLocation", UnityEngine.Random.Range(minWanderWaitTime, maxWanderWaitTime));
        }

        if (playerDistance < detectDistance)
        {
            SetState(AIState.Attacking);
        }
    }

    private void WanderToNewLocation()
    {
        if (aiState != AIState.Idle) return;
        
        SetState(AIState.Wandering);
        agent.SetDestination(GetWonderLocation());
    }

    private Vector3 GetWonderLocation()
    {
        NavMeshHit hit;

        NavMesh.SamplePosition(
            transform.position + (Random.onUnitSphere * Random.Range(minWanderDistance, maxWanderDistance)), out hit,
            maxWanderDistance, NavMesh.AllAreas);
        int i = 0;

        while (Vector3.Distance(transform.position, hit.position) < detectDistance)
        {
            NavMesh.SamplePosition(
                transform.position + (Random.onUnitSphere * Random.Range(minWanderDistance, maxWanderDistance)), out hit,
                maxWanderDistance, NavMesh.AllAreas);
            i++;
            if (i == 30) break;
        }
        
        return hit.position;
    }

    private void FollowUpdate()
    {
        if (playerDistance < attackDistance && IsPlayerInFieldOfView())
        {
            agent.isStopped = true;
            if (Time.time - lastAttackTime > attackRate)
            {
                lastAttackTime = Time.time;
                CharacterManager.Instance.Player.condition.uiCondition.stamina.curValue += damage;
            }
        }
        else
        {
            if (playerDistance < detectDistance)
            {
                agent.isStopped = false;
                NavMeshPath path = new NavMeshPath();
                if (agent.CalculatePath(CharacterManager.Instance.Player.transform.position, path))
                {
                    agent.SetDestination(CharacterManager.Instance.Player.transform.position);
                }
                else
                {
                    agent.SetDestination(transform.position);
                    agent.isStopped = true;
                    SetState(AIState.Wandering);
                }
            }    
        }
        
    }

    private bool IsPlayerInFieldOfView()
    {
        Vector3 directionToPlayer = CharacterManager.Instance.Player.transform.position - transform.position;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);
        return angle < fieldOfView * 0.5f;
        
        
    }
}
