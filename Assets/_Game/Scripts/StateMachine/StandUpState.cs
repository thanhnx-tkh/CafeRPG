using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandUpState : IState<BotController>
{
    private float timer;
    private float moveTime;
    public void OnEnter(BotController t)
    {
        timer = 0;
        moveTime = 2f;
        t.ChangeAnim(Constants.ANIM_STAND_UP);

    }

    public void OnExecute(BotController t)
    {
        if (timer > moveTime)
        {
            t.chairZone.IsIdle = true;
            t.chairZone.isChairFound = true;
            t.ChangeState(new IdleState());
            t.chairZone.chair.objCoffe.SetActive(false);
        }
        timer += Time.deltaTime;
    }

    public void OnExit(BotController t)
    {

    }

}