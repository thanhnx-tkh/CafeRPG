using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRandomState : IState<BotController>
{

    private Vector3 destination;

    private float timer;
    private float moveTime;
    public void OnEnter(BotController t)
    {
        destination = t.GetRandomPositionBot();
        timer = 0;
        moveTime = Random.Range(5f,10f);
        destination = t.GetRandomPositionBot();
        t.SetDestination(destination);
    }

    public void OnExecute(BotController t)
    {
        if (Vector3.Distance(destination, t.transform.position) < 0.1f){
            destination = t.GetRandomPositionBot();
            t.SetDestination(destination);
        }

        if(timer > moveTime)
        {
            t.ChangeState(new MoveTargetState());
        }
        timer += Time.deltaTime;
    }
    public void OnExit(BotController t)
    {

    }

}