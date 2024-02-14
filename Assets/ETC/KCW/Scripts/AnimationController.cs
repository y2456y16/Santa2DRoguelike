using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int IsHit = Animator.StringToHash("IsHit");

    protected Animator animator;
    private bool IsAttacking = false;
    private float timeCount = 0f;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        timeCount += Time.deltaTime;
    }


    public void Attacking(AttackSO obj)
    {
        if (IsAttacking == false)
        {
            IsAttacking = true;
            timeCount = 0.0f;
            animator.SetTrigger(Attack);
        }
        else if(IsAttacking == true && timeCount >600f)
        {
            IsAttacking = false;
        }
         
    }

    public void Move(Vector2 obj)
    {
        animator.SetBool(IsWalking, obj.magnitude > .5f);
    }

    public void Stop(Vector2 obj)
    {
        animator.SetBool(IsWalking, false);
    }

    private void Hit()
    {
        animator.SetBool(IsHit, true);
    }
}
