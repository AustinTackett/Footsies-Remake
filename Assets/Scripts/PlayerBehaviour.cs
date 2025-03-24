using System;
using UnityEngine;
using UnityEngine.InputSystem;

public enum ActionType
{
    Attack1,
    Hit,
    Move,
}

public class PlayerBehaviour : MonoBehaviour
{
    public float speed = 80;

    private ActionType action;
    private Animator animator;
    private Rigidbody2D rigidBody;
    private Vector2 moveDir;

    public void Start()
    {
        action = ActionType.Move;
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        moveDir = Vector2.zero;
    }

    void FixedUpdate()
    {
        Vector3 velocity = moveDir * speed * Time.fixedDeltaTime;
        rigidBody.linearVelocity = velocity;
        FaceMovementDir(velocity);

        if(velocity == Vector3.zero)
        {
            animator.SetBool("Run", false);
        }
        else
        {
            animator.SetBool("Run", true);
        }
    }


    public void FaceMovementDir(Vector3 velocity)
    {
        if(velocity.x > 0)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        else if (velocity.x < 0)
            transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    public void OnAttack1()
    {

    }

    public void OnAttack1Input()
    {
        animator.SetBool("Attack1", true);
    }

    public void OnMoveInput(InputValue value)
    {
        moveDir = value.Get<Vector2>();
    }

    public void OnHit()
    {
        Debug.Log("Hit!");
    }

}