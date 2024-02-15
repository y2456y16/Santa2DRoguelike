using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollideControl : MonoBehaviour
{
    [SerializeField] private LayerMask levelCollisionLayer;
    [SerializeField] private bool isTargetPlayer;
    [SerializeField] GameObject _gameObject;

    private CharacterStatsHandler _characterStats;
    private Rigidbody2D _rigidbody;
    void Start()
    {
        _characterStats = _gameObject.GetComponent<CharacterStatsHandler>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }


    private void OnTriggerEnter2D(Collider2D collision)//Collider 충돌을 하면
    {
        if (levelCollisionLayer.value == (levelCollisionLayer.value | (1 << collision.gameObject.layer))) // player와 부딪치면(즉 공격)
        {
            HealthSystem healthSystem = collision.GetComponent<HealthSystem>();
            if (healthSystem != null) // 널이 아니면
            {
                healthSystem.ChangeHealth(_characterStats.CurrentStats.atk); //체력을 자신의 공격력 만큼 닳게 한다.

            }
        }
    }
}
