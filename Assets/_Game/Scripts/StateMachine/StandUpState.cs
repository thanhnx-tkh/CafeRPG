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
        t.transform.position = new Vector3(t.transform.position.x, t.transform.position.y + 0.2f, t.transform.position.z);

    }

    public void OnExecute(BotController t)
    {
        if (timer > moveTime)
        {
            t.transform.position = new Vector3(t.transform.position.x, t.transform.position.y - 0.2f, t.transform.position.z);
            t.chairZone.IsIdle = true;
            t.chairZone.isChairFound = true;
            t.ChangeState(new IdleState());
        }
        timer += Time.deltaTime;
    }

    public void OnExit(BotController t)
    {

    }

}