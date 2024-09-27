using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<BotController>
{
    public void OnEnter(BotController t)
    {
        t.targetRotationObject = null;
        t.ChangeState(new MoveRandomState());
    }

    public void OnExecute(BotController t)
    {

    }

    public void OnExit(BotController t)
    {

    }

}
