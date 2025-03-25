using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    public float speed = 300;
    public float attackRange = 1000;
    public float attackRadius = 10;

    private Animator animator;
    private Rigidbody2D rigidBody;
    private Vector2 moveDir;
    private Collider2D selfCollider;

    void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        selfCollider = GetComponent<Collider2D>();
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
        
        Vector3 playerCenter = selfCollider.bounds.center;
        Vector3 direction;

        Debug.Log(transform.rotation.eulerAngles.y);
        if (transform.rotation.eulerAngles.y == 180)
            direction = Vector3.left;
        else    
            direction = Vector3.right;
         
        RaycastHit2D[] hits = Physics2D.CircleCastAll(playerCenter, attackRadius, direction, attackRange);
        foreach(RaycastHit2D hit in hits)
            Debug.Log(hit.collider.gameObject.tag);
    }

    public void OnAttack1Input()
    {
        animator.SetBool("Attack1", true);
        OnAttack1();
    }

    public void OnMoveInput(InputValue value)
    {
        moveDir = value.Get<Vector2>();
    }

    public void OnHit()
    {
        Debug.Log("Hit!");
        animator.SetBool("Hit", true);
    }

}