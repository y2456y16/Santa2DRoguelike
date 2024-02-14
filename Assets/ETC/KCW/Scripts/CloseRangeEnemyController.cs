using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class CloseRangeEnemyController : CharacterController
{
    [SerializeField] private float followRange = 1000f; //�÷��̾� ���� ����
    [SerializeField] private float shootRange = 2f; //�����Ÿ�

    GameManagerTemp gameManager;
    private bool IsRange = false;

    protected Transform ClosestTarget { get; private set; } //�÷��̾��� ��ġ ����

    protected virtual void Start()
    {
        gameManager = GameManagerTemp.Instance;
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
        return (ClosestTarget.position - aim.armPivot.position).normalized;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

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
}
