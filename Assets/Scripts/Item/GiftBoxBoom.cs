using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GiftBoxBoom : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed = 20;
    [SerializeField] private float _bulletDistance;
    [SerializeField] private ParticleSystem _particle;

    private Transform _player;
    private Vector3 _curMousePosiiton;
    private Rigidbody2D _rigid;
   

    void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        _player = ItemManager.Instance.player;
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
        
        //TODO 파티클 시스템에 trigger적용되게 변경
        Debug.Log("Explode");
        _particle.Play();
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0f);
        Destroy(gameObject, 0.3f);
    }
}
