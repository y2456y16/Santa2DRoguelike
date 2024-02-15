using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TopDownAnimationController:TopDownAnimations
{
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int IsHit = Animator.StringToHash("IsHit");
    private static readonly int AttackToIdle = Animator.StringToHash("AttackToIdle");

    //보스 전용
    private static readonly int IsDelayAttack = Animator.StringToHash("IsDelayAttack");
    private static readonly int BossDeath = Animator.StringToHash("BossDeath");
    private static readonly int BossAttack = Animator.StringToHash("BossAttack");


    //플레이어 사망시 출력애니메이션
    private static readonly int IsDead = Animator.StringToHash("IsDead");

    //임시공격2, 스킬
    private static readonly int Attack2 = Animator.StringToHash("Attack2");
    private static readonly int Skill = Animator.StringToHash("Skill");

    // KCW : 공격 여부 및 시간 체크
    public bool IsAttacking = false;

    //KCW : 보스 공격 딜레이
    private bool IsDelay = false;

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

    public void EnemyAttack(AttackSO obj)
    {
        if (IsAttacking == false)
        {
            animator.SetTrigger("EnemyAttacking");
            IsAttacking = true;
        }
    }

    public void EnemyToIdle()
    {
        animator.SetTrigger("AttackToIdle");
        IsAttacking = false;
    }


    public void BossDelayMotion(AttackSO obj)
    {
        if(IsDelay == false)
        {
            animator.SetTrigger(IsDelayAttack);
            IsDelay = true;
        }

    }

    public void BossAttacking(AttackSO obj)
    {
        IsAttacking = true;
        IsDelay = false;
        animator.SetTrigger(BossAttack);
    }

    public void BossToIdle()
    {
        animator.SetTrigger(AttackToIdle);
        IsAttacking = false;
    }

    public void BossDead()
    {
        animator.SetTrigger(BossDeath);
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