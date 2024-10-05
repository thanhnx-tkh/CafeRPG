using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTargetState : IState<BotController>
{

    private Vector3 destination;
    public void OnEnter(BotController t)
    {
        Chair chairTarget =  GameManager.Ins.GetRandomPointSitting();
        List<BotController> botControllers = GameManager.Ins.botControllers;
        for (int i = 0; i < botControllers.Count; i++)
        {
            if(botControllers[i].targetChair == chairTarget){
                t.ChangeState(new MoveTargetState());
            }
        }
        destination = chairTarget.chairZone.transform.position;
        t.targetChair = chairTarget;
        t.SetDestination(destination);
        
    }

    public void OnExecute(BotController t)
    {
        if (Vector3.Distance(destination, t.transform.position) < 0.2f)
        {
            t.transform.position = destination;
            t.ChangeAnim(Constants.ANIM_IDLE);
        }
    }
    public void OnExit(BotController t)
    {

    }

}