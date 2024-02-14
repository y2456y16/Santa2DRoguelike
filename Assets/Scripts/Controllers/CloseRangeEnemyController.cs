using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class CloseRangeEnemyController : TopDownCharacterController
{
    [SerializeField] private float followRange = 1000f; //�÷��̾� ���� ����
    [SerializeField] private float shootRange = 2f; //�����Ÿ�

    GameManager gameManager;
    private bool IsRange = false;

    protected Transform ClosestTarget { get; private set; } //�÷��̾��� ��ġ ����

    protected virtual void Start()
    {
        gameManager = GameManager.Instance;
        ClosestTarget = gameManager.Player;
    }
    protected override void Awake()
    {
        base.Awake();
    }
    protected float DistanceToTarget() //�÷��̾���� �Ÿ�
    {
        return Vector3.Distance(transform.position, ClosestTarget.position);
    }
    protected Vector2 DirectionToTarget()//�÷��̾ ���� ����
    {
        return (ClosestTarget.position - transform.position).normalized;
    }

    protected Vector2 BulletDirectionToTarget()
    {
        return (ClosestTarget.position - aim.atkPivot.position).normalized;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        Move();

        float distance = DistanceToTarget(); //�÷��̾���� �Ÿ�
        Vector2 direction = DirectionToTarget();//�÷��̾ ���� ����


        IsAttacking = false; //���� �Ұ���
        if (distance <= followRange) //���Ͱ� ������ �� �ִ� �����ȿ� �÷��̾ ������
        {
            if (distance <= shootRange)
            {
                _animcontroller.Stop(Vector2.zero);
                OnLookInput(direction); //�÷��̾ �ٶ󺻴�.
                aim.OnAim(direction);
                curMovementInput = Vector2.zero; //���ڸ��� ���缭 ���.
                IsAttacking = true; //���� �����ϰ� ����
                Invoke("AttackDisplay", 1f);

            }
            else
            {
                direction = BulletDirectionToTarget();
                _animcontroller.Move(direction);
                OnLookInput(direction);//�÷��̾� ������ �ٶ󺻴�.
                aim.OnAim(direction);
                OnMoveInput(direction);//�÷��̾� �������� �̵�
            }
        }
        else
        {
            direction = BulletDirectionToTarget();
            OnLookInput(direction);//�÷��̾� ������ �ٶ󺻴�.
            aim.OnAim(direction);
            OnMoveInput(direction);//�÷��̾� �������� �̵�
        }
    }
    

    public void AttackDisplay()
    {
        _animcontroller.Attacking(Stats.CurrentStats.attackSO);

    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        IsRange = true;
    }

    public void Move()
    {

        Vector2 moveInput = curMovementInput.normalized;
        moveInput = moveInput * moveSpeed;

        _rigidbody.velocity = moveInput;
    }

    //KCW : ���� �Ʒ��� �� ĳ���� ����� ��ɵ�
    public void OnMoveInput(Vector2 direction)
    {
        curMovementInput = direction;
    }

    public void Look(Vector2 direction)
    {
        aim.OnAim(direction);
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

    public void OnAttack()
    {

    }
}
