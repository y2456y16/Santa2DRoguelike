using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownAnimationController : TopDownAnimations
{
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int IsHit = Animator.StringToHash("IsHit");

    //�÷��̾� ����� ��¾ִϸ��̼�
    private static readonly int IsDead = Animator.StringToHash("IsDead");

    //�ӽð���2, ��ų
    private static readonly int Attack2 = Animator.StringToHash("Attack2");
    private static readonly int Skill = Animator.StringToHash("Skill");

    private HealthSystem _healthSystem;

    protected override void Awake()
    {
        base.Awake();
        _healthSystem = GetComponent<HealthSystem>();
    }

    // Start is called before the first frame update
    void Start()
    {
        controller.OnAttackEvent += Attacking;
        controller.OnMoveEvent += Move;

        if(_healthSystem != null)
        {
            _healthSystem.OnDamage += Hit;
            _healthSystem.OnInvincibilityEnd += InvincibilityEnd;
            _healthSystem.OnDeath += Dead;
        }

        //�ӽ�
        controller.OnAttackEvent2 += Attacking2;
        controller.OnSkillEvent += SkillUse;
    }

    private void Move(Vector2 obj)
    {
        animator.SetBool(IsWalking, obj.magnitude > .5f);
    }

    private void Attacking(AttackSO obj)
    {
        animator.SetTrigger(Attack);
    }

    private void Hit()
    {
        animator.SetBool(IsHit, true);
    }

    private void InvincibilityEnd()
    {
        animator.SetBool(IsHit, false);
    }

    //���ó��
    public void Dead()
    {
        animator.SetBool(IsDead, true);
    }

    //�ӽ�
    private void Attacking2(AttackSO obj)
    {
        animator.SetTrigger(Attack2);
    }
    private void SkillUse(AttackSO obj)
    {
        animator.SetTrigger(Skill);
    }

}