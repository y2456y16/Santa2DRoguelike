using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using UnityEngine;

public class CharacterShooting : MonoBehaviour
{
    private ProjectileManager _projectileManager;
    private CharacterController _controller;

    [SerializeField] private Transform projectileSpawnPosition;//투사체 발사 위치
    private Vector2 _aimDirection = Vector2.right;//에임의 방향

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _projectileManager = ProjectileManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnAim(Vector2 newAimDirection)
    {
        _aimDirection = newAimDirection;
    }

    public void OnShoot(AttackSO attackSO)//투사체 발사
    {
        RangedAttackData rangedAttackData = attackSO as RangedAttackData; //공격 정보를 원거리로 형변환
        float projectilesAngleSpace = rangedAttackData.multipleProjectilesAngel;
        int numberOfProjextilesPerShot = rangedAttackData.numberofProjectilesPerShot;
        float minAngle = -(numberOfProjextilesPerShot / 2f) * projectilesAngleSpace + 0.5f * rangedAttackData.multipleProjectilesAngel; //투사체를 동시에 여러개 발사할 경우 중앙을 기준으로 투사체가 날아가도록 각도를 꺾어 놓는다.

        for (int i = 0; i < numberOfProjextilesPerShot; i++) //한 번에 발사할 투사체 개수
        {

            float angle = minAngle + projectilesAngleSpace * i; //투사의 각도를 일정하게 더한다.
            float randomSpread = Random.Range(-rangedAttackData.spread, rangedAttackData.spread);//투사체의 정확도를 결정
            angle += randomSpread;

            CreateProjectile(rangedAttackData, angle); //총알 생성
        }

    }

    private void CreateProjectile(RangedAttackData rangedAttackData, float angle)//총알 생성
    {
        _projectileManager.ShootBullet(
            projectileSpawnPosition.position,
            RotateVector2(_aimDirection, angle),
            rangedAttackData
            );
    }

    private static Vector2 RotateVector2(Vector2 v, float degree)//벡터를 각도의 방향으로 회전
    {
        return Quaternion.Euler(0, 0, degree) * v;
    }

}
