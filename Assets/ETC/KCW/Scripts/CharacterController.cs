using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class CharacterController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public Vector2 curMovementInput;

    [Header("Look")]
    public Vector2 curLookInput;
    public float lookSensitivity;

    [HideInInspector] //public이지만 가리려고 할때 이용
    public bool canLook = true;

    private Rigidbody2D _rigidbody;
    private Camera _camera;
    public CharacterShooting shoot;

    public CharacterAim aim;
    public Animator _animcontroller;

    private float _timeSinceLastAtteck = float.MaxValue;//공격 딜레이 시간 체크
    protected bool IsAttacking { get; set; } //공격 가능 여부

    protected CharacterStatHandler Stats { get; private set; }


    protected virtual void Awake()
    {
        _animcontroller = GetComponentInChildren<Animator>();
        aim = GetComponent<CharacterAim>();
        _rigidbody = GetComponent<Rigidbody2D>();
        Stats = GetComponent<CharacterStatHandler>();
        shoot = GetComponent<CharacterShooting>();
        _camera = Camera.main; //메인 카메라 연결
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //cursor가 보이지 않도록 잠금
    }

    protected virtual void FixedUpdate()
    {
        Move();
        HandleAttackDelay();
    }

    private void HandleAttackDelay()//공격이 가능하면 공격 실행
    {
        if (Stats.CurrentStats.attackSO == null) //자신의 공격 정보가 없으면
            return;

        if (_timeSinceLastAtteck <= Stats.CurrentStats.attackSO.delay) //공격 딜레이보다 시간이 작다면
        {
            _timeSinceLastAtteck += Time.deltaTime;//시간 증가
        }

        if (IsAttacking && _timeSinceLastAtteck > Stats.CurrentStats.attackSO.delay && shoot != null)//공격 가능 상태이고 딜레이의 시간을 넘겼다면
        {
            _timeSinceLastAtteck = 0;//시간 초기화
            shoot.OnShoot(Stats.CurrentStats.attackSO);//공격 실행
        }
        else if(IsAttacking && _timeSinceLastAtteck > Stats.CurrentStats.attackSO.delay && shoot == null)
        {
            _timeSinceLastAtteck = 0;//시간 초기화
            OnAttack();
        }
    }

    public void Move()
    {
        
        Vector2 moveInput = curMovementInput.normalized;
        moveInput = moveInput * moveSpeed;
      
        _rigidbody.velocity = moveInput;
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)//키가 눌러지고 있으면 Performed
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled) //누르고 있던 키를 땠으면 Canceled
        {
            curMovementInput = Vector2.zero;
        }
    }

    public void OnMoveInput(Vector2 direction)
    {
        curMovementInput = direction;
    }

    public void Look(Vector2 direction)
    {
        aim.OnAim(direction);
    }
    public void OnLookInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)//키가 눌러지고 있으면 Performed
        {
            curLookInput = context.ReadValue<Vector2>();
            Vector2 worldPos = _camera.ScreenToWorldPoint(curLookInput);//게임좌표로 변환한다.
            curLookInput = (worldPos - (Vector2)transform.position).normalized;//자신의 위치에서 에임의 위치로 가는 단위벡터를 반환

            if (curLookInput.magnitude >= .9f)//크기가 .9보다 클 때
            {
                Look(curLookInput);//실행
            }
        }
    }

    public void OnLookInput(Vector2 direction)//마우스를 화면상에서 움직였을 때
    {
        curLookInput = direction;//스크린상 좌표를 가져오고
        Vector2 worldPos = _camera.ScreenToWorldPoint(curLookInput);//게임좌표로 변환한다.
        curLookInput = (worldPos - (Vector2)transform.position).normalized;//자신의 위치에서 에임의 위치로 가는 단위벡터를 반환

        if (curLookInput.magnitude >= .9f)//크기가 .9보다 클 때
        {
            Look(curLookInput);//실행
        }
    }

    public void OnAttackInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)//키가 눌러지고 있으면 Performed
        {
            IsAttacking = true;
        }
        else if (context.phase == InputActionPhase.Canceled) //누르고 있던 키를 땠으면 Canceled
        {
            IsAttacking = false;
        }
    }

    public void OnAttack()
    {

    }
}
