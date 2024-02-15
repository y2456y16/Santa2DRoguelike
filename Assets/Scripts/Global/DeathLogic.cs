using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathLogic : MonoBehaviour
{
    private HealthSystem _healthSystem;
    [SerializeField] private bool player = false;
    private Rigidbody2D _rigidbody;
    private GameObject _gameObject;

    private void Start()
    {
        _healthSystem = GetComponent<HealthSystem>();
        _healthSystem.OnDeath += Dead;
    }

    void Dead()
    {
        _rigidbody.velocity = Vector3.zero;

        foreach(SpriteRenderer renderer in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            Color color = renderer.color;
            color.a = 0.3f;
            renderer.color = color;
        }

        foreach (Behaviour component in transform.GetComponentsInChildren<Behaviour>())//죽은 객체의 모든 컴포넌트를 정지시킨다.
        {
            component.enabled = false;
        }
        Destroy(gameObject, 2f); // 2초 후 삭제
    }
}
