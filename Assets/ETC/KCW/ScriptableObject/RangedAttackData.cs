using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RangedAttackData", menuName = "Controller/Attack/Ranged", order = 1)]
public class RangedAttackData : AttackSO
{
    [Header("Ranged Attack Data")]//���Ÿ� ���� ����
    public string bulletNameTag; //����ü�� �±�
    public float duration;//����ü�� ���ӽð�
    public float spread;//����ü�� ������ ���� = ��Ȯ��
    public int numberofProjectilesPerShot;//�� ���� ���ư� ����ü�� ��
    public float multipleProjectilesAngel;//����ü �� ������ ����
}
