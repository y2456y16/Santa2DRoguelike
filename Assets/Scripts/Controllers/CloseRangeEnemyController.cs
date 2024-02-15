using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class CloseRangeEnemyController : TopDownCharacterController
{
    [SerializeField] private float followRange = 1000f; //플레이어 인지 범위
    [SerializeField] private float shootRange = 2f; //사정거리

    GameManager gameManager;
    private bool IsRange = false;

    protected Transform ClosestTarget { get; private set; } //플레이어의 위치 참조

    protected virtual void Start()
    {
        gameManager = GameManager.Instance;
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
        return (ClosestTarget.position - aim.atkPivot.position).normalized;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        Move();

        float distance = DistanceToTarget(); //플레이어와의 거리
        Vector2 direction = DirectionToTarget();//플레이어를 향한 방향


        IsAttacking = false; //공격 불가능
        if (distance <= followRange) //몬스터가 인지할 수 있는 범위안에 플레이어가 들어오면
        {
            if (distance <= shootRange)
            {
                _animcontroller.Stop(Vector2.zero);
                OnLookInput(direction); //플레이어를 바라본다.
                aim.OnAim(direction);
                curMovementInput = Vector2.zero; //제자리에 멈춰서 쏜다.
                IsAttacking = true; //공격 가능하게 설정
                Invoke("AttackDisplay", 1f);

            }
            else
            {
                direction = BulletDirectionToTarget();
                _animcontroller.Move(direction);
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
        if(IsRange == false)
        {
            _animcontroller.EnemyAttack(Stats.CurrentStats.attackSO);
            StartCoroutine(DelayAttackTime());
            IsRange = true;
        }
        else
        {

        }
        

    }

    IEnumerator DelayAttackTime()
    {
        yield return new WaitForSeconds(2.0f);

    }

    public void Move()
    {

        Vector2 moveInput = curMovementInput.normalized;
        moveInput = moveInput * moveSpeed;

        _rigidbody.velocity = moveInput;
    }

    //KCW : 이후 아래는 적 캐릭터 연결된 기능들
    public void OnMoveInput(Vector2 direction)
    {
        curMovementInput = direction;
    }

    public void Look(Vector2 direction)
    {
        aim.OnAim(direction);
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


}
