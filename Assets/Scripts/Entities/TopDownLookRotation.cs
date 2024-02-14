using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownLookRotation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer punchRenderer;
    [SerializeField] public Transform atkPivot;

    [SerializeField] private SpriteRenderer characterRenderer;

    private TopDownCharacterController _controller;

    private void Awake()
    {
        _controller = GetComponent<TopDownCharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _controller.OnLookEvent += OnAim;
    }

    public void OnAim(Vector2 newAimDirection)
    {
        RotateLook(newAimDirection);
    }

    private void RotateLook(Vector2 direction)
    {
        //float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //기존 작업

        //KCW : player와 몬스터 구분
        float rotZ = 0f;
        if (gameObject.tag == "Player")//스프라이트의 방향이 몬스터와 달라서 플레이어라면
        {
            rotZ = Mathf.Atan2(direction.y, -direction.x) * Mathf.Rad2Deg; // x - 값
        }
        else
        {
            rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;// x + 값
        }

        punchRenderer.flipY = Mathf.Abs(rotZ) > 90f;
        characterRenderer.flipX = punchRenderer.flipY;

        atkPivot.rotation = Quaternion.Euler(0, 0, rotZ);
    }

    //KCW : 불필요해서 삭제
    /*
    void Update()
    {
        
    }
    */
}