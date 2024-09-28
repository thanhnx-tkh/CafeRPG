using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotController : BaseCharacter
{
    [SerializeField]public NavMeshAgent agent;
    public Chair targetChair;
    private IState<BotController> currentState;
    private void Start()
    {
        ChangeState(new IdleState());
    }
     private void Update()
    {
        if(targetChair != null && targetChair.chairZone.IsPlayerSitDown){
            ChangeState(new MoveTargetState());
            targetRotationObject = null;
        }
        RotationToTable();
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
    }

    public void ChangeState(IState<BotController> state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = state;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    public void SetDestination(Vector3 point)
    {
        agent.enabled = true;
        agent.SetDestination(point);
        ChangeAnim(Constants.ANIM_WALK);
    }

    public Vector3 GetRandomPositionBot()
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * 10f;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, 10f, NavMesh.AllAreas);
        return hit.position;
    }
    
}
