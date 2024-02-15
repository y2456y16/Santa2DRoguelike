using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GiftBoxBoom : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed = 20;
    [SerializeField] private float explosionRadius = 1.5f;

    private Animator _ani;
    private Transform _player;
    private Vector3 _curMousePosiiton;
    private Rigidbody2D _rigid;

    void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _ani = GetComponent<Animator>();
    }
    void Start()
    {
        _player = GameManager.Instance.Player;
        _curMousePosiiton = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dir = (_curMousePosiiton - _player.transform.position).normalized;
        _rigid.AddForce(dir * _bulletSpeed, ForceMode2D.Impulse);

        StartCoroutine(Explode());
    }
    private void Update()
    {
        if (_rigid.velocity != Vector2.zero)
        {
            float distance = Vector2.Distance((Vector2)_curMousePosiiton, (Vector2)transform.position);
            // 목표 지점에 가까워질수록 속도 감소
            if (distance < 1f)
            {
                _rigid.velocity *= distance;
            }
        }
    }
    private IEnumerator Explode()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();

        for(int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.3f);
            sprite.color = Color.white;
            yield return new WaitForSeconds(0.3f);
            sprite.color = Color.red;
        }
        
        _ani.Play("Explode");
        ApplyExplosionDamage(transform.position);
        Destroy(gameObject, 0.3f);
    }

    public void ApplyExplosionDamage(Vector2 explosionPoint)
    {
        // 폭발 범위 내의 모든 콜라이더 찾기
        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPoint, explosionRadius);

        foreach (Collider2D hit in colliders)
        {
            // 몬스터 레이어와 충돌한 경우에만 데미지 처리
            if (hit.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                Debug.Log("몬스터 충돌");
                //데미지 예상
                //ItemManager.Instance.GetItem(ItemID.GiftBoxBoom).data.Damage + _player.GetComponent<CharacterStatsHandler>().CurrentStats.atk;
            }
        }
    }

    //Debug 용도
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // 원의 색상 설정
        Gizmos.DrawWireSphere(transform.position, explosionRadius); // 원 그리기
    }
}
