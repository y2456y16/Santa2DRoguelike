using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TopDownCharacterController : MonoBehaviour
{
    //KCW : ĳ���� �̵� ����
    [Header("Movement")]
    public float moveSpeed;
    public Vector2 curMovementInput;

    //KCW : ĳ���� ���� ����
    [Header("Look")]
    public float lookSensitivity;
    public Vector2 curLookInput;




    //KCW : ĳ���� ���� ��� ����
    public TopDownLookRotation aim;
    //KCW : ĳ���� �ִϸ��̼� ��Ʈ�ѷ� ȣ��
    public TopDownAnimationController _animcontroller;
    //KCW : �߷� ȣ��
    public Rigidbody2D _rigidbody;
    public Camera _camera;

    public event Action<Vector2> OnMoveEvent;
    public event Action<Vector2> OnLookEvent;
    public event Action<AttackSO> OnAttackEvent;
    public event Action<AttackSO> OnAttackEvent2;
    public event Action<AttackSO> OnSkillEvent;

    private float _timeSinceLastAttack = float.MaxValue;
    protected bool IsAttacking { get; set; }

    //��Ÿ�� �����ָ԰��ݸ�� bool�� �Դϴ�.(�ӽ�)
    protected bool IsAttacking2 { get; set; }

    //��Ÿ�� ��ų���� bool�� �Դϴ�(�ӽ�)
    protected bool IsSkill { get; set; }

    protected CharacterStatsHandler Stats { get; private set; }
    

    protected virtual void Awake()
    {
        //KCW : �� ��� ���� �Է�
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
        //�ӽ�
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
    //�ӽ� �׼��̺�Ʈ2
    public void CallAttackEvent2(AttackSO attackSO)
    {
        OnAttackEvent2?.Invoke(attackSO);
    }
    //�ӽ� ��ų �̺�Ʈ
    public void CallSkillEvent(AttackSO attackSO)
    {
        OnSkillEvent?.Invoke(attackSO);
    }
}