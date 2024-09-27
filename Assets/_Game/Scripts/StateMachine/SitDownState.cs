using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitDownState : IState<BotController>
{
    float time;
    float count;
    public void OnEnter(BotController t)
    {
        time = Random.Range(5f,15f);
        count = 0;
        t.ChangeAnim(Constants.ANIM_SIT_DOWN);
    }
    public void OnExecute(BotController t)
    {
        count += Time.deltaTime;
        if (count > time)
        {
            t.ChangeState(new StandUpState());
        }

    }

    public void OnExit(BotController t)
    {

    }

}
