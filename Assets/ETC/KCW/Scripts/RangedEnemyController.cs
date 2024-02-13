using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class RangeEnemyController : CharacterController
{
    [SerializeField] private float followRange = 1000f; //�÷��̾� ���� ����
    [SerializeField] private float shootRange = 10f; //�����Ÿ�

    GameManagerTemp gameManager;

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
            if (distance <= shootRange)//���� ������ �÷��̾ ������
            {
                _animcontroller.SetBool("IsWalking", false);
                int layerMaskTarget = Stats.CurrentStats.attackSO.target; //��ǥ�� ���̾� ����ũ�� �����´�.
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 11f, (1 << LayerMask.NameToLayer("Level")) | layerMaskTarget); // �����ɽ�Ʈ�� ���� �÷��̾��� ���� ��Ʈ����ũ

                if (hit.collider != null && layerMaskTarget == (layerMaskTarget | (1 << hit.collider.gameObject.layer))) //���̿� ó�� �ε��� ���� �ְ� ��ǥ�� ��Ʈ����ũ�� �ε�ģ ����� ��Ʈ����ũ�� ������ (= ������ ���̾���)
                {
                    
                    direction = BulletDirectionToTarget();
                    OnLookInput(direction); //�÷��̾ �ٶ󺻴�.
                    aim.OnAim(direction);
                    shoot.OnAim(direction);
                    curMovementInput = Vector2.zero; //���ڸ��� ���缭 ���.
                    IsAttacking = true; //���� �����ϰ� ����
                }
                else
                {

                }
            }
            else
            {
                _animcontroller.SetBool("IsWalking", true);
                direction = BulletDirectionToTarget();
                OnLookInput(direction);//�÷��̾� ������ �ٶ󺻴�.
                aim.OnAim(direction);
                shoot.OnAim(direction);
                OnMoveInput(direction);//�÷��̾� �������� �̵�
            }
        }
        else
        {
            direction = BulletDirectionToTarget();
            OnLookInput(direction);//�÷��̾� ������ �ٶ󺻴�.
            aim.OnAim(direction);
            shoot.OnAim(direction);
            OnMoveInput(direction);//�÷��̾� �������� �̵�
        }
    }
}
