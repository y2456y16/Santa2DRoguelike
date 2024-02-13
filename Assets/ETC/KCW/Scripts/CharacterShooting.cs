using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using UnityEngine;

public class CharacterShooting : MonoBehaviour
{
    private ProjectileManager _projectileManager;
    private CharacterController _controller;

    [SerializeField] private Transform projectileSpawnPosition;//����ü �߻� ��ġ
    private Vector2 _aimDirection = Vector2.right;//������ ����

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

    public void OnShoot(AttackSO attackSO)//����ü �߻�
    {
        RangedAttackData rangedAttackData = attackSO as RangedAttackData; //���� ������ ���Ÿ��� ����ȯ
        float projectilesAngleSpace = rangedAttackData.multipleProjectilesAngel;
        int numberOfProjextilesPerShot = rangedAttackData.numberofProjectilesPerShot;
        float minAngle = -(numberOfProjextilesPerShot / 2f) * projectilesAngleSpace + 0.5f * rangedAttackData.multipleProjectilesAngel; //����ü�� ���ÿ� ������ �߻��� ��� �߾��� �������� ����ü�� ���ư����� ������ ���� ���´�.

        for (int i = 0; i < numberOfProjextilesPerShot; i++) //�� ���� �߻��� ����ü ����
        {

            float angle = minAngle + projectilesAngleSpace * i; //������ ������ �����ϰ� ���Ѵ�.
            float randomSpread = Random.Range(-rangedAttackData.spread, rangedAttackData.spread);//����ü�� ��Ȯ���� ����
            angle += randomSpread;

            CreateProjectile(rangedAttackData, angle); //�Ѿ� ����
        }

    }

    private void CreateProjectile(RangedAttackData rangedAttackData, float angle)//�Ѿ� ����
    {
        _projectileManager.ShootBullet(
            projectileSpawnPosition.position,
            RotateVector2(_aimDirection, angle),
            rangedAttackData
            );
    }

    private static Vector2 RotateVector2(Vector2 v, float degree)//���͸� ������ �������� ȸ��
    {
        return Quaternion.Euler(0, 0, degree) * v;
    }

}
