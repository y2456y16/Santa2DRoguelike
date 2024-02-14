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

    //아래부터 더미데이터 스킬을 어디에 사용할지 모르는상태이기때문에 정해지면 추가하면됩니다.

    //산타의 오른주먹공격(임시로 우클릭으로 설정해뒀습니다)
    public void OnAttack2(InputValue value)
    {
        IsAttacking2 = value.isPressed;
    }

    //산타의 스킬공격 (임시로 E키로 설정해뒀습니다)
    public void OnSkill(InputValue value)
    {
        IsSkill = value.isPressed;
    }
}
