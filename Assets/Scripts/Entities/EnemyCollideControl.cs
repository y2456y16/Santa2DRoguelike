using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EnemyCollideControl : MonoBehaviour
{
    [SerializeField] private LayerMask levelCollisionLayer;
    [SerializeField] private bool isTargetPlayer;
    [SerializeField] GameObject _gameObject;

    private CharacterStatsHandler _characterStats;
    private HealthSystem _collideHealthSystem;
    private Rigidbody2D _rigidbody;
    private bool _isCollidingWithTarget;
    void Start()
    {
        _characterStats = _gameObject.GetComponent<CharacterStatsHandler>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_isCollidingWithTarget) //충돌 종료 후 실행
        {
            ApplyHealthChange();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)//Collider 충돌을 하면
    {
        if (levelCollisionLayer.value == (levelCollisionLayer.value | (1 << collision.gameObject.layer))) // player와 부딪치면(즉 공격)
        {
            Debug.Log(2);
            _collideHealthSystem = collision.GetComponent<HealthSystem>();
            if(_collideHealthSystem != null) //부딪친 상대의 헬스시스템이 있다면
            {
                Debug.Log("ok");
                _isCollidingWithTarget = true; //접촉한 것으로 한다.
            }

            if (_collideHealthSystem != null) // 널이 아니면
            {
                Debug.Log("ok2");
                _collideHealthSystem.ChangeHealth(collision.gameObject.GetComponent<CharacterStatsHandler>().CurrentStats.atk); //체력을 자신의 공격력 만큼 닳게 한다.

            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision) //충돌 후 떨어질 때
    {
        GameObject receiver = collision.gameObject;
        _isCollidingWithTarget = false; // 접촉하지 않은 것으로 한다.
    }
    private void ApplyHealthChange()
    {
        AttackSO attackSO = _characterStats.CurrentStats.attackSO; //현재의 공격 정보
        bool hasBeenChanged = _collideHealthSystem.ChangeHealth(-attackSO.power); //상대의 헬스시스템에게 자신의 공격력만큼 데미지를 가한다.
    }
}
