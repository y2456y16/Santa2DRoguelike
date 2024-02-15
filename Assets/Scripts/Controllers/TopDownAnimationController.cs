using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TopDownAnimationController : TopDownAnimations
{
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int IsHit = Animator.StringToHash("IsHit");

    //플레이어 사망시 출력애니메이션
    private static readonly int IsDead = Animator.StringToHash("IsDead");

    //임시공격2, 스킬
    private static readonly int Attack2 = Animator.StringToHash("Attack2");
    private static readonly int Skill = Animator.StringToHash("Skill");

    // KCW : 공격 여부 및 시간 체크
    private bool IsAttacking = false;
    private float timeCount = 0f;

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

        //임시
        controller.OnAttackEvent2 += Attacking2;
        //controller.OnSkillEvent += SkillUse;
    }
    private void FixedUpdate()
    {
        //KCW : 시간 체크
        timeCount += Time.deltaTime;
    }

    public void Move(Vector2 obj)
    {
        animator.SetBool(IsWalking, obj.magnitude > .5f);
    }

    //KCW : 멈춤 기능 추가
    public void Stop(Vector2 obj)
    {
        animator.SetBool(IsWalking, false);
    }

    public void Attacking(AttackSO obj)
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

    //사망처리
    public void Dead()
    {
        animator.SetBool(IsDead, true);
    }

    //임시
    private void Attacking2(AttackSO obj)
    {
        animator.SetTrigger(Attack2);
    }
    private void SkillUse(AttackSO obj)
    {
        animator.SetTrigger(Skill);
    }

}