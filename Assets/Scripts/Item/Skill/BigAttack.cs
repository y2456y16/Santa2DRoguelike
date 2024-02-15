using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BigAttack : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed = 15;

    private Transform _player;
    private Vector3 _curMousePosiiton;
    private Rigidbody2D _rigid;


    void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        _player = GameManager.Instance.Player;
        _curMousePosiiton = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = ((Vector2)_curMousePosiiton - (Vector2)_player.transform.position).normalized;
        _rigid.AddForce(dir * _bulletSpeed, ForceMode2D.Impulse);

        float degree = Mathf.Atan2(_rigid.velocity.y, _rigid.velocity.x) * Mathf.Rad2Deg;
        transform.Rotate(new Vector3(0f,0f,degree));

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            //TODO 몬스터 데미지 주기
            Debug.Log("몬스터 데미지");
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
