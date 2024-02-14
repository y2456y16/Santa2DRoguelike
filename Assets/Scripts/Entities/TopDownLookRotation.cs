using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownLookRotation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer punchRenderer;
    [SerializeField] private Transform atkPivit;

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

    private void OnAim(Vector2 newAimDirection)
    {
        RotateLook(newAimDirection);
    }

    private void RotateLook(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        punchRenderer.flipY = Mathf.Abs(rotZ) > 90f;
        characterRenderer.flipX = punchRenderer.flipY;

        atkPivit.rotation = Quaternion.Euler(0, 0, rotZ);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}