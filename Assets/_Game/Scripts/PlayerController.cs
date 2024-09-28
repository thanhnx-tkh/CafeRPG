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
    private void Update() {
        RotationToTable();
    }
    private void FixedUpdate()
    {
        if (IsMoving)
            Moving();
        // sit_down && stand_up
        if (Input.GetKeyDown(KeyCode.G))
        {
            IsMoving = false;
            ChangeAnim(Constants.ANIM_SIT_DOWN);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            ChangeAnim(Constants.ANIM_STAND_UP);
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.25f, transform.position.z);
            StartCoroutine(CoStartIsMoving());
        }
    }
    IEnumerator CoStartIsMoving()
    {
        yield return new WaitForSeconds(3f);
        IsMoving = true;
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.25f, transform.position.z);

    }
    public void Moving()
    {
        // moveHorizontal = Input.GetAxis("Horizontal");
        // moveVertical = Input.GetAxis("Vertical");

        moveHorizontal = joystick.Horizontal;
        moveVertical = joystick.Vertical;

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
        rb.velocity = Vector3.zero;
        ChangeAnim(Constants.ANIM_SIT_DOWN);
        IsStandUp = true;
    }

    public void ButtonStandUp(){
        if(!IsStandUp) return;
        // stand up 
        ChangeAnim(Constants.ANIM_STAND_UP);
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
        StartCoroutine(CoResetAnim());
        
    }
    IEnumerator CoResetAnim(){
        yield return new WaitForSeconds(2f);
        ChangeAnim(Constants.ANIM_WALK);
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z);
        IsMoving = true;
        targetRotationObject = null;

        chairZone.IsIdle = true;
        chairZone.isChairFound = true;

        chairZone.IsPlayerSitDown = false;
        IsStandUp = false;
    }

}
