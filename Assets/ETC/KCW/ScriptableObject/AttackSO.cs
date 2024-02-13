using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultAttackData", menuName = "Controller/Attack/Default", order = 0)]
public class AttackSO : ScriptableObject
{
    [Header("Attack Info")]//���� ���� ����
    public float size; //������ ������
    public float delay; //������ ������
    public float power;//���ݷ�
    public float speed;//���ݼӵ�
    public LayerMask target;//������ ����� LayerMask

    [Header("Knock Back Info")]//�˹� ����
    public bool isOnKnockback;//�˹� ����
    public float knockbackPower;//�˹��� ��
    public float knockbackTime;//�˹� ���ӽð�
}

