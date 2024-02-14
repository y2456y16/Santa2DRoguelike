using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TopDownCharacterController : MonoBehaviour
{
    //KCW : 캐릭터 이동 구현
    [Header("Movement")]
    public float moveSpeed;
    public Vector2 curMovementInput;

    //KCW : 캐릭터 조준 구현
    [Header("Look")]
    public float lookSensitivity;
    public Vector2 curLookInput;




    //KCW : 캐릭터 조준 기능 구현
    public TopDownLookRotation aim;
    //KCW : 캐릭터 애니메이션 컨트롤러 호출
    public TopDownAnimationController _animcontroller;
    //KCW : 중력 호출
    public Rigidbody2D _rigidbody;
    public Camera _camera;

    public event Action<Vector2> OnMoveEvent;
    public event Action<Vector2> OnLookEvent;
    public event Action<AttackSO> OnAttackEvent;
    public event Action<AttackSO> OnAttackEvent2;
    public event Action<AttackSO> OnSkillEvent;

    private float _timeSinceLastAttack = float.MaxValue;
    protected bool IsAttacking { get; set; }

    //산타의 오른주먹공격모션 bool값 입니다.(임시)
    protected bool IsAttacking2 { get; set; }

    //산타의 스킬공격 bool값 입니다(임시)
    protected bool IsSkill { get; set; }

    protected CharacterStatsHandler Stats { get; private set; }
    

    protected virtual void Awake()
    {
        //KCW : 각 기능 정보 입력
        _animcontroller = GetComponent<TopDownAnimationController>();
        aim = GetComponent<TopDownLookRotation>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _camera = Camera.main;

        Stats = GetComponent<CharacterStatsHandler>();
    }

    protected virtual void FixedUpdate()
    {
        HandleAttackDelay();
    }

    private void HandleAttackDelay()
    {
        if (Stats.CurrentStats.attackSO == null)
            return;

        if(_timeSinceLastAttack <= Stats.CurrentStats.attackSO.delay)
        {
            _timeSinceLastAttack += Time.deltaTime;
        }
        if (IsAttacking && _timeSinceLastAttack > Stats.CurrentStats.attackSO.delay)
        {
            _timeSinceLastAttack = 0;
            CallAttackEvent(Stats.CurrentStats.attackSO);
        }
        //임시
        if (IsAttacking2 && _timeSinceLastAttack > Stats.CurrentStats.attackSO.delay)
        {
            _timeSinceLastAttack = 0;
            CallAttackEvent2(Stats.CurrentStats.attackSO);
        }
        if (IsSkill && _timeSinceLastAttack > Stats.CurrentStats.attackSO.delay)
        {
            _timeSinceLastAttack = 0;
            CallSkillEvent(Stats.CurrentStats.attackSO);
        }
    }

    public void CallMoveEvent(Vector2 direction)
    {
        OnMoveEvent?.Invoke(direction);
    }

    public void CallLookEvent(Vector2 direction)
    {
        OnLookEvent?.Invoke(direction);
    }

    public void CallAttackEvent(AttackSO attackSO)
    {
        OnAttackEvent?.Invoke(attackSO);
    }
    //임시 액션이벤트2
    public void CallAttackEvent2(AttackSO attackSO)
    {
        OnAttackEvent2?.Invoke(attackSO);
    }
    //임시 스킬 이벤트
    public void CallSkillEvent(AttackSO attackSO)
    {
        OnSkillEvent?.Invoke(attackSO);
    }
}