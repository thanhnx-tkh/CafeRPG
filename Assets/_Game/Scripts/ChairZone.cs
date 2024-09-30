using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairZone : MonoBehaviour
{
    public bool isChairFound = true;
    public bool IsIdle = true;

    public bool IsPlayerSitDown = false;
    [SerializeField] public Chair chair;
    private float time;
    private float timeCount;
    private void Start() {
        time = 2f;
        timeCount = 0f;

        chair.objCoffe.SetActive(false);
    }
    private void OnTriggerStay(Collider other)
    {
        if (!IsIdle) return;
        if (timeCount > time)
        {
            BaseCharacter character = Cache<BaseCharacter>.GetCollider(other);
            if (character == null) return;
            if (character as PlayerController)
            {
                chair.objCoffe.SetActive(true);
                IsPlayerSitDown = true;
                isChairFound = false;
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
                chair.objCoffe.SetActive(true);

                BotController botController = (BotController)character;
                
                botController.agent.enabled = false;
                botController.targetRotationObject = chair.Table.transform;
                botController.chairZone = this;
                botController.transform.position = transform.position;

                botController.ChangeState(new SitDownState());
            }
        }
        timeCount += Time.deltaTime;
    }
    
}
