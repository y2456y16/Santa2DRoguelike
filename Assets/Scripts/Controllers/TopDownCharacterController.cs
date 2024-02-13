using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TopDownCharacterController : MonoBehaviour
{
    public event Action<Vector2> OnMoveEvent;
    public event Action<Vector2> OnLookEvent;
    public event Action OnAttackEvent;
    public event Action OnAttackEvent2;
    public event Action OnSkillEvent;

    private float _timeSinceLastAttack = float.MaxValue;
    protected bool IsAttacking { get; set; }

    //��Ÿ�� �����ָ԰��ݸ�� bool�� �Դϴ�.(�ӽ�)
    protected bool IsAttacking2 { get; set; }

    //��Ÿ�� ��ų���� bool�� �Դϴ�(�ӽ�)
    protected bool IsSkill { get; set; }

    protected CharacterStatsHandler Stats { get; private set; }

    protected virtual void Awake()
    {
        Stats = GetComponent<CharacterStatsHandler>();
    }

    protected virtual void Update()
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
            CallAttackEvent();
        }
        //�ӽ�
        if (IsAttacking2 && _timeSinceLastAttack > Stats.CurrentStats.attackSO.delay)
        {
            _timeSinceLastAttack = 0;
            CallAttackEvent2();
        }
        if (IsSkill && _timeSinceLastAttack > Stats.CurrentStats.attackSO.delay)
        {
            _timeSinceLastAttack = 0;
            CallSkillEvent();
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

    public void CallAttackEvent()
    {
        OnAttackEvent?.Invoke();
    }
    //�ӽ� �׼��̺�Ʈ2
    public void CallAttackEvent2()
    {
        OnAttackEvent2?.Invoke();
    }
    //�ӽ� ��ų �̺�Ʈ
    public void CallSkillEvent()
    {
        OnSkillEvent?.Invoke();
    }
}