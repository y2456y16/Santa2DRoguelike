using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackController:MonoBehaviour
{
    [SerializeField] private LayerMask levelCollisionLayer;

    //KCW : player가 타깃인지 확인
    [SerializeField] private bool isTargetPlayer;

    private RangedAttackData _attackData;
    private float _currentDuration;
    private Vector2 _direction;
    private bool _isReady;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;

    //KCW : trailRenderer 호출
    private TrailRenderer _trailRenderer;

    private ProjectileManager _projectileManager;

    public bool fxOnDestory = true;

    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _trailRenderer = GetComponentInChildren<TrailRenderer>();
    }

    private void Update()
    {
        if (!_isReady)
        {
            return;
        }

        _currentDuration += Time.deltaTime;

        if (_currentDuration > _attackData.duration)
        {
            DestroyProjectile(transform.position, false);
        }

        _rigidbody.velocity = _direction * _attackData.speed;
    }

    //벽과 충돌했을때
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (levelCollisionLayer.value == (levelCollisionLayer.value | (1 << collision.gameObject.layer)))
        {
            DestroyProjectile(collision.ClosestPoint(transform.position) - _direction * .2f, fxOnDestory);
        }
        else if (_attackData.target.value == (_attackData.target.value | (1 << collision.gameObject.layer)))
        {
            HealthSystem healthSystem = collision.GetComponent<HealthSystem>();
            if (healthSystem != null)
            {
                healthSystem.ChangeHealth(-_attackData.power);
                if (collision.tag == "Player")//타겟이 플레이어일 때 , 제가 layer를 아직 잘 활용하지 못해서 tag로 했습니다. layer로 수정해주셔도 괜찮습니다.
                {
                    UIManager.Instance.BrokenHeart();
                }
            }
            DestroyProjectile(collision.ClosestPoint(transform.position), fxOnDestory);
        }
    }


    public void InitializeAttack(Vector2 direction, RangedAttackData attackData, ProjectileManager projectileManager)
    {
        _projectileManager = projectileManager;
        _attackData = attackData;
        _direction = direction;

        UpdateProjectilSprite();
        _currentDuration = 0;
        _spriteRenderer.color = attackData.projectileColor;

        transform.right = _direction;

        _isReady = true;
    }

    private void UpdateProjectilSprite()
    {
        transform.localScale = Vector3.one;
    }

    private void DestroyProjectile(Vector3 position, bool createFx)
    {
        if (createFx)
        {
            
        }
        gameObject.SetActive(false);
    }
}