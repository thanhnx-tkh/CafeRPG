using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairZone : MonoBehaviour
{
    public bool isChairFound = true;
    public bool IsIdle = true;
    [SerializeField] public Chair chair;
    private float time;
    private float timeCount;
    private void Start() {
        time = 1f;
        timeCount = 0f;
    }
    private void OnTriggerStay(Collider other)
    {

        if (!IsIdle) return;
        if (timeCount > time)
        {
            BaseCharacter character = Cache<BaseCharacter>.GetCollider(other);
            if (character == null) return;
            if (character as PlayerController && isChairFound)
            {
                isChairFound =false;
                timeCount = 0f;
                IsIdle = false;
                // to do : character sit down
                PlayerController playerController = (PlayerController)character;
                playerController.chairZone = this;
                StartCoroutine(playerController.CoAnimDrinkCafe());
            }
            if (character as BotController)
            {
                timeCount = 0f;
                IsIdle = false;

                BotController botController = (BotController)character;
                
                botController.agent.enabled = false;
                botController.targetRotationObject = chair.Table.transform;
                botController.chairZone = this;

                botController.ChangeState(new SitDownState());
            }
        }
        timeCount += Time.deltaTime;
    }
    // Player
    
}
