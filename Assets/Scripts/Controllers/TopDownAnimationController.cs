using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownAnimationController : TopDownAnimations
{
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int IsHit = Animator.StringToHash("IsHit");

    //임시
    private static readonly int Attack2 = Animator.StringToHash("Attack2");
    private static readonly int Skill = Animator.StringToHash("Skill");


    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    void Start()
    {
        controller.OnAttackEvent += Attacking;
        controller.OnMoveEvent += Move;

        //임시
        controller.OnAttackEvent2 += Attacking2;
        controller.OnSkillEvent += SkillUse;
    }

    private void Move(Vector2 obj)
    {
        animator.SetBool(IsWalking, obj.magnitude > .5f);
    }

    private void Attacking()
    {
        animator.SetTrigger(Attack);
    }

    //임시
    private void Attacking2()
    {
        animator.SetTrigger(Attack2);
    }
    private void SkillUse()
    {
        animator.SetTrigger(Skill);
    }

    private void Hit()
    {
        animator.SetBool(IsHit, true);
    }

    private void InvincibilityEnd()
    {
        animator.SetBool(IsHit, false);
    }
}