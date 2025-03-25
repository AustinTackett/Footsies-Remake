using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class FighterBehaviour : MonoBehaviour
{
    public int life = 3;
    public float speed = 300;
    public float attackRange = 4;
    public float attackRadius = 1;
    public LifeMeterBehaviour LifeMeter;

    private Animator animator;
    private Rigidbody2D rigidBody;
    private Vector2 moveDir;
    private Collider2D selfCollider;
    private int selfExcludedLayerMask;

    void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        selfCollider = GetComponent<Collider2D>();
        moveDir = Vector2.zero;
        selfExcludedLayerMask = ~(1 << gameObject.layer);
    }

    void FixedUpdate()
    {
        Vector3 velocity = moveDir * speed * Time.fixedDeltaTime;

        if(animator.GetBool("Attack1") || animator.GetBool("Hit"))
        {
            rigidBody.linearVelocity = Vector3.zero;
        }
        else
        {
            rigidBody.linearVelocity = velocity;
            FaceMovementDir(velocity);
        }

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
        if (transform.rotation.eulerAngles.y == 180)
            direction = Vector3.left;
        else    
            direction = Vector3.right;
         
        RaycastHit2D[] hits = Physics2D.CircleCastAll(
            playerCenter, 
            attackRadius, 
            direction, 
            attackRange, 
            selfExcludedLayerMask
        );

        foreach(RaycastHit2D hit in hits)
        {
            GameObject collisionGO = hit.collider.gameObject;
            if(collisionGO.TryGetComponent<FighterBehaviour>(out FighterBehaviour opponent))
            {
                Debug.Log("I see hit!");
                opponent.OnHit();
            }
        }
    }

    public void OnEndAttack1()
    {
        animator.SetBool("Attack1", false);
    }


    public void OnHit()
    {
        Debug.Log("Hit!");
        animator.SetBool("Hit", true);

        // Make sure if attack is interrupted the attacking state is released
        animator.SetBool("Attack1", false);

        if(LifeMeter != null)
        {
            Debug.Log("Remove Heart");
            LifeMeter.RemoveHeart();
        }
    }

    public void OnEndHit()
    {
        animator.SetBool("Hit", false);
    }

    public void OnAttack1Input()
    {
        if(!animator.GetBool("Attack1") && !animator.GetBool("Hit"))
        {
            animator.SetBool("Attack1", true);
            OnAttack1();
        }
    }

    public void OnMoveInput(InputValue value)
    {
        moveDir = value.Get<Vector2>();
    }
}