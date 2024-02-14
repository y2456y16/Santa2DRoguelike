using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : TopDownCharacterController
{
    protected override void Awake()
    {
        base.Awake();

    }

    public void OnMove(InputValue value)
    {
        Vector2 moveInput = value.Get<Vector2>().normalized;
        CallMoveEvent(moveInput);
    }

    public void OnLook(InputValue value)
    {
        Vector2 newAim = value.Get<Vector2>();
        Vector2 worldPos = _camera.ScreenToWorldPoint(newAim);
        newAim = (worldPos - (Vector2)transform.position).normalized;

        if(newAim.magnitude >= .9f)
        {
            CallLookEvent(newAim);
        }
    }

    public void OnAttack(InputValue value)
    {
        IsAttacking = value.isPressed;
    }

    //�Ʒ����� ���̵����� ��ų�� ��� ������� �𸣴»����̱⶧���� �������� �߰��ϸ�˴ϴ�.

    //��Ÿ�� �����ָ԰���(�ӽ÷� ��Ŭ������ �����ص׽��ϴ�)
    public void OnAttack2(InputValue value)
    {
        IsAttacking2 = value.isPressed;
    }

    //��Ÿ�� ��ų���� (�ӽ÷� EŰ�� �����ص׽��ϴ�)
    public void OnSkill(InputValue value)
    {
        IsSkill = value.isPressed;
    }
}
