using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTargetState : IState<BotController>
{

    private Vector3 destination;
    public void OnEnter(BotController t)
    {
        destination = GameManager.Ins.GetRandomPointSitting();
        t.SetDestination(destination);
    }

    public void OnExecute(BotController t)
    {
        if (Vector3.Distance(destination, t.transform.position) < 0.2f)
        {
            t.ChangeAnim(Constants.ANIM_IDLE);
        }
    }
    public void OnExit(BotController t)
    {

    }

}