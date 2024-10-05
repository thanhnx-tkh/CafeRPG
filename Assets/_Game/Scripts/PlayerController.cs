using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseCharacter
{
    public float speed = 5f;
    public float rotationSpeed = 720f;
    [SerializeField] public Rigidbody rb;
    public float moveHorizontal;
    public float moveVertical;
    private bool IsStandUp = false;

    [SerializeField] private VariableJoystick joystick;
    private void Start()
    {
        ChangeAnim(Constants.ANIM_IDLE);
        IsMoving = true;
    }
    private void Update()
    {
        RotationToTable();
    }
    private void FixedUpdate()
    {
        moveHorizontal = joystick.Horizontal;
        moveVertical = joystick.Vertical;
        if (Mathf.Abs(moveHorizontal) > 0.01f || Mathf.Abs(moveVertical) > 0.1f)
        {
            ButtonStandUp();
        }
        if (IsMoving)
            Moving();
    }
    public void Moving()
    {


        if (Mathf.Abs(moveHorizontal) > 0.01f || Mathf.Abs(moveVertical) > 0.1f)
        {
            ChangeAnim(Constants.ANIM_WALK);

            rb.velocity = new Vector3(moveHorizontal * speed, rb.velocity.y, moveVertical * speed);

            if (rb.velocity != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(rb.velocity);
            }
        }
        else
        {
            rb.velocity = Vector3.zero;
            ChangeAnim(Constants.ANIM_IDLE);
        }
    }
    public override void SetPosition(Vector3 point)
    {
        transform.position = point;
    }
    public void SetVelocity(Vector3 velocity)
    {
        rb.velocity = velocity;
    }
    public IEnumerator CoAnimDrinkCafe()
    {
        ChangeAnim(Constants.ANIM_IDLE);
        SetVelocity(Vector3.zero);
        IsMoving = false;
        SetPosition(chairZone.transform.position);
        targetRotationObject = chairZone.chair.Table.transform;

        yield return new WaitForSeconds(1f);
        // sit down 
        ChangeAnim(Constants.ANIM_SIT_DOWN);
        IsStandUp = true;
    }

    public void ButtonStandUp()
    {
        if (!IsStandUp) return;
        // stand up 
        ChangeAnim(Constants.ANIM_STAND_UP);
        StartCoroutine(CoResetAnim());

    }
    IEnumerator CoResetAnim()
    {
        yield return new WaitForSeconds(2f);
        ChangeAnim(Constants.ANIM_WALK);
        chairZone.chair.objCoffe.SetActive(false);
        IsMoving = true;
        targetRotationObject = null;

        chairZone.IsIdle = true;
        chairZone.isChairFound = true;

        chairZone.IsPlayerSitDown = false;
        IsStandUp = false;
    }

}
