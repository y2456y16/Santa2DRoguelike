using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class RangeEnemyController : CharacterController
{
    [SerializeField] private float followRange = 1000f; //플레이어 인지 범위
    [SerializeField] private float shootRange = 10f; //사정거리

    GameManagerTemp gameManager;

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
            if (distance <= shootRange)//공격 범위에 플레이어가 들어오면
            {
                _animcontroller.SetBool("IsWalking", false);
                int layerMaskTarget = Stats.CurrentStats.attackSO.target; //목표의 레이어 마스크를 가져온다.
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 11f, (1 << LayerMask.NameToLayer("Level")) | layerMaskTarget); // 레이케스트는 땅과 플레이어의 합인 비트마스크

                if (hit.collider != null && layerMaskTarget == (layerMaskTarget | (1 << hit.collider.gameObject.layer))) //레이와 처음 부딪힌 것이 있고 목표의 비트마스크와 부딪친 상대의 비트마스크가 같으면 (= 동일한 레이어라면)
                {
                    
                    direction = BulletDirectionToTarget();
                    OnLookInput(direction); //플레이어를 바라본다.
                    aim.OnAim(direction);
                    shoot.OnAim(direction);
                    curMovementInput = Vector2.zero; //제자리에 멈춰서 쏜다.
                    IsAttacking = true; //공격 가능하게 설정
                }
                else
                {

                }
            }
            else
            {
                _animcontroller.SetBool("IsWalking", true);
                direction = BulletDirectionToTarget();
                OnLookInput(direction);//플레이어 방향을 바라본다.
                aim.OnAim(direction);
                shoot.OnAim(direction);
                OnMoveInput(direction);//플레이어 방향으로 이동
            }
        }
        else
        {
            direction = BulletDirectionToTarget();
            OnLookInput(direction);//플레이어 방향을 바라본다.
            aim.OnAim(direction);
            shoot.OnAim(direction);
            OnMoveInput(direction);//플레이어 방향으로 이동
        }
    }
}
