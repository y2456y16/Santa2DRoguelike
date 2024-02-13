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
        if (!_isReady)//���ư� �غ� ���� �ʾҴٸ�
        {
            return;
        }

        _currentDuration += Time.deltaTime; //�ð��� ������Ų��.

        if (_currentDuration > _attackData.duration)//����ü�� ���ӽð��� �Ѿ��
        {
            DestroyProjectile(transform.position, false);//����ü�� ȸ���Ѵ�.
        }

        _rigidbody.velocity = _direction * _attackData.speed;//����ü�� �ӵ��� ����
    }

    private void OnTriggerEnter2D(Collider2D collision)//Collider �浹�� �ϸ�
    {
        if (levelCollisionLayer.value == (levelCollisionLayer.value | (1 << collision.gameObject.layer))) // Level�� �ε�ġ�� 
        {
            DestroyProjectile(collision.ClosestPoint(transform.position) - _direction * .2f, fxOnDestory); //����ü ����
        }
        else if (_attackData.target.value == (_attackData.target.value | (1 << collision.gameObject.layer))) //����ü�� Ÿ�ٰ� �ε��ƴٸ�
        {
            DestroyProjectile(collision.ClosestPoint(transform.position), fxOnDestory);//����ü ����
        }
    }


    public void InitializeAttack(Vector2 direction, RangedAttackData attackData, ProjectileManager projectileManager) //����ü �ʱ�ȭ
    {
        _projectileManager = projectileManager;
        _attackData = attackData;
        _direction = direction;

        _trailRenderer.Clear();
        _currentDuration = 0;

        transform.right = _direction;//����ü�� �������� ���ư��� �������� ����

        _isReady = true;//���ư� �غ� ��ħ
    }

    private void UpdateProjectilSprite()//����ü�� ũ�⸦ ���� - ������ ����.
    {
        transform.localScale = Vector3.one * _attackData.size;
    }


    private void DestroyProjectile(Vector3 position, bool createFx) //����ü�� ��Ȱ��ȭ
    {
        if (createFx)
        {

        }
        gameObject.SetActive(false);
    }
}
