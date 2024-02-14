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

    [HideInInspector] //public������ �������� �Ҷ� �̿�
    public bool canLook = true;

    private Rigidbody2D _rigidbody;
    private Camera _camera;
    public CharacterShooting shoot;

    public CharacterAim aim;
    public AnimationController _animcontroller;

    private float _timeSinceLastAtteck = float.MaxValue;//���� ������ �ð� üũ
    protected bool IsAttacking { get; set; } //���� ���� ����

    protected CharacterStatHandler Stats { get; private set; }


    protected virtual void Awake()
    {
        _animcontroller = GetComponentInChildren<AnimationController>();
        aim = GetComponent<CharacterAim>();
        _rigidbody = GetComponent<Rigidbody2D>();
        Stats = GetComponent<CharacterStatHandler>();
        shoot = GetComponent<CharacterShooting>();
        _camera = Camera.main; //���� ī�޶� ����
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //cursor�� ������ �ʵ��� ���
    }

    protected virtual void FixedUpdate()
    {
        Move();
        HandleAttackDelay();
    }

    private void HandleAttackDelay()//������ �����ϸ� ���� ����
    {
        if (Stats.CurrentStats.attackSO == null) //�ڽ��� ���� ������ ������
            return;

        if (_timeSinceLastAtteck <= Stats.CurrentStats.attackSO.delay) //���� �����̺��� �ð��� �۴ٸ�
        {
            _timeSinceLastAtteck += Time.deltaTime;//�ð� ����
        }

        if (IsAttacking && _timeSinceLastAtteck > Stats.CurrentStats.attackSO.delay && shoot != null)//���� ���� �����̰� �������� �ð��� �Ѱ�ٸ�
        {
            _timeSinceLastAtteck = 0;//�ð� �ʱ�ȭ
            shoot.OnShoot(Stats.CurrentStats.attackSO);//���� ����
        }
        else if(IsAttacking && _timeSinceLastAtteck > Stats.CurrentStats.attackSO.delay && shoot == null)
        {
            _timeSinceLastAtteck = 0;//�ð� �ʱ�ȭ
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
        if (context.phase == InputActionPhase.Performed)//Ű�� �������� ������ Performed
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled) //������ �ִ� Ű�� ������ Canceled
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
        if (context.phase == InputActionPhase.Performed)//Ű�� �������� ������ Performed
        {
            curLookInput = context.ReadValue<Vector2>();
            Vector2 worldPos = _camera.ScreenToWorldPoint(curLookInput);//������ǥ�� ��ȯ�Ѵ�.
            curLookInput = (worldPos - (Vector2)transform.position).normalized;//�ڽ��� ��ġ���� ������ ��ġ�� ���� �������͸� ��ȯ

            if (curLookInput.magnitude >= .9f)//ũ�Ⱑ .9���� Ŭ ��
            {
                Look(curLookInput);//����
            }
        }
    }

    public void OnLookInput(Vector2 direction)//���콺�� ȭ��󿡼� �������� ��
    {
        curLookInput = direction;//��ũ���� ��ǥ�� ��������
        Vector2 worldPos = _camera.ScreenToWorldPoint(curLookInput);//������ǥ�� ��ȯ�Ѵ�.
        curLookInput = (worldPos - (Vector2)transform.position).normalized;//�ڽ��� ��ġ���� ������ ��ġ�� ���� �������͸� ��ȯ

        if (curLookInput.magnitude >= .9f)//ũ�Ⱑ .9���� Ŭ ��
        {
            Look(curLookInput);//����
        }
    }

    public void OnAttackInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)//Ű�� �������� ������ Performed
        {
            IsAttacking = true;
        }
        else if (context.phase == InputActionPhase.Canceled) //������ �ִ� Ű�� ������ Canceled
        {
            IsAttacking = false;
        }
    }

    public void OnAttack()
    {

    }
}
