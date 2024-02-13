using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class CloseRangeEnemyController : CharacterController
{
    [SerializeField] private float followRange = 1000f; //플레이어 인지 범위
    [SerializeField] private float shootRange = 2f; //사정거리

    GameManagerTemp gameManager;
    private bool IsRange = false;

    protected Transform ClosestTarget { get; private set; } //플레이어의 위치 참조

    protected virtual void Start()
    {
        gameManager = GameManagerTemp.Instance;
        ClosestTarget = gameManager.Player;
    }
    protected override void Awake()
    {
        base.Awake();
    }
    protected float DistanceToTarget() //플레이어와의 거리
    {
        return Vector3.Distance(transform.position, ClosestTarget.position);
    }
    protected Vector2 DirectionToTarget()//플레이어를 향한 방향
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

        float distance = DistanceToTarget(); //플레이어와의 거리
        Vector2 direction = DirectionToTarget();//플레이어를 향한 방향


        IsAttacking = false; //공격 불가능
        if (distance <= followRange) //몬스터가 인지할 수 있는 범위안에 플레이어가 들어오면
        {
            if (distance <= shootRange)
            {
                _animcontroller.SetBool("IsWalking", false);
                direction = BulletDirectionToTarget();
                OnLookInput(direction); //플레이어를 바라본다.
                aim.OnAim(direction);
                curMovementInput = Vector2.zero; //제자리에 멈춰서 쏜다.
                IsAttacking = true; //공격 가능하게 설정
                Invoke("AttackDisplay", 0f);

            }
            else
            {
                _animcontroller.SetBool("IsWalking", true);
                direction = BulletDirectionToTarget();
                OnLookInput(direction);//플레이어 방향을 바라본다.
                aim.OnAim(direction);
                OnMoveInput(direction);//플레이어 방향으로 이동
            }
        }
        else
        {
            direction = BulletDirectionToTarget();
            OnLookInput(direction);//플레이어 방향을 바라본다.
            aim.OnAim(direction);
            OnMoveInput(direction);//플레이어 방향으로 이동
        }
    }

    public void AttackDisplay()
    {
        _animcontroller.SetTrigger("Attack");

    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        IsRange = true;
    }
}
