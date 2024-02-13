using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RangedAttackController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private LayerMask levelCollisionLayer;
    [SerializeField] private bool isTargetPlayer;
    private RangedAttackData _attackData;
    private float _currentDuration;
    private Vector2 _direction;
    private bool _isReady;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private TrailRenderer _trailRenderer;
    private ProjectileManager _projectileManager;

    public bool fxOnDestory = true;

    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _trailRenderer = GetComponent<TrailRenderer>();
    }


    private void Update()
    {
        if (!_isReady)//날아갈 준비가 되지 않았다면
        {
            return;
        }

        _currentDuration += Time.deltaTime; //시간을 증가시킨다.

        if (_currentDuration > _attackData.duration)//투사체의 지속시간이 넘어가면
        {
            DestroyProjectile(transform.position, false);//투사체를 회수한다.
        }

        _rigidbody.velocity = _direction * _attackData.speed;//투사체의 속도를 설정
    }

    private void OnTriggerEnter2D(Collider2D collision)//Collider 충돌을 하면
    {
        if (levelCollisionLayer.value == (levelCollisionLayer.value | (1 << collision.gameObject.layer))) // Level과 부딪치면 
        {
            DestroyProjectile(collision.ClosestPoint(transform.position) - _direction * .2f, fxOnDestory); //투사체 삭제
        }
        else if (_attackData.target.value == (_attackData.target.value | (1 << collision.gameObject.layer))) //투사체의 타겟과 부딪쳤다면
        {
            DestroyProjectile(collision.ClosestPoint(transform.position), fxOnDestory);//투사체 삭제
        }
    }


    public void InitializeAttack(Vector2 direction, RangedAttackData attackData, ProjectileManager projectileManager) //투사체 초기화
    {
        _projectileManager = projectileManager;
        _attackData = attackData;
        _direction = direction;

        _trailRenderer.Clear();
        _currentDuration = 0;

        transform.right = _direction;//투사체의 오른쪽을 날아가는 방향으로 설정

        _isReady = true;//날아갈 준비를 마침
    }

    private void UpdateProjectilSprite()//투사체의 크기를 설정 - 사용되지 않음.
    {
        transform.localScale = Vector3.one * _attackData.size;
    }


    private void DestroyProjectile(Vector3 position, bool createFx) //투사체의 비활성화
    {
        if (createFx)
        {

        }
        gameObject.SetActive(false);
    }
}
