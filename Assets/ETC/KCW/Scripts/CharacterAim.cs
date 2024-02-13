using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAim : MonoBehaviour
{
    [SerializeField] private SpriteRenderer armRenderer;
    [SerializeField] public Transform armPivot;

    [SerializeField] private SpriteRenderer characterRender;

    private CharacterController _controller;


    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnAim(Vector2 newAimDirection)
    {
        RotateArm(newAimDirection);
    }

    private void RotateArm(Vector2 direction)
    {
        float rotZ = 0f;
        if (gameObject.tag == "Player")//스프라이트의 방향이 몬스터와 달라서 플레이어라면
        {
            rotZ = Mathf.Atan2(direction.y, -direction.x) * Mathf.Rad2Deg; // x - 값
        }
        else
        {
            rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;// x + 값
        }

        armRenderer.flipY = Mathf.Abs(rotZ) > 90f;//캐릭터의 가운데를 기준으로 왼쪽이면 무기의 스프라이트 위 아래 반전
        characterRender.flipX = armRenderer.flipY;//무기가 반전되면 캐릭터의 좌 우를 반전
        armPivot.rotation = Quaternion.Euler(0, 0, rotZ);//투사체 발사 위치를 rotZ로 설정
    }
}
