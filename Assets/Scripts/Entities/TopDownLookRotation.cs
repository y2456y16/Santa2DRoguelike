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
        //float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //���� �۾�

        //KCW : player�� ���� ����
        float rotZ = 0f;
        if (gameObject.tag == "Player")//��������Ʈ�� ������ ���Ϳ� �޶� �÷��̾���
        {
            rotZ = Mathf.Atan2(direction.y, -direction.x) * Mathf.Rad2Deg; // x - ��
        }
        else
        {
            rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;// x + ��
        }

        punchRenderer.flipY = Mathf.Abs(rotZ) > 90f;
        characterRenderer.flipX = punchRenderer.flipY;

        atkPivot.rotation = Quaternion.Euler(0, 0, rotZ);
    }

    //KCW : ���ʿ��ؼ� ����
    /*
    void Update()
    {
        
    }
    */
}