using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    public ChairZone chairZone;

    [SerializeField] protected Animator anim;
    public Transform targetRotationObject;

    public bool IsMoving { get; set; }
    private void Awake()
    {
        IsMoving = false;
    }
    protected string animName = Constants.ANIM_IDLE;

    public void ChangeAnim(string animName)
    {
        if (this.animName != animName)
        {
            anim.ResetTrigger(this.animName);
            this.animName = animName;
            anim.SetTrigger(this.animName);
        }
    }
    public virtual void SetPosition(Vector3 point)
    {

    }
    public void RotationToTable()
    {
        if (targetRotationObject != null)
        {
            Vector3 direction = targetRotationObject.position - transform.position;
            direction.y = 0;

            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(direction); ;
            }
        }
    }

}
