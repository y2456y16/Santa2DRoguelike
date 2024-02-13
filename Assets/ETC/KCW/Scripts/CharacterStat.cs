using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterType
{
    Player1,
    Player2,
    Monster1,
    Monster2
}

public enum StatsChangeType
{
    Add,
    Multiple,
    Override
}

[Serializable]
public class CharacterStat //ĳ������ ���� ����
{
    public CharacterType type; //ĳ������ ���� - ������ �ʴ´�.
    public StatsChangeType statsChangeType; //������ ���� - ������ �ʴ´�.
    [Range(1, 100)] public int maxHealth; //ĳ������ �ִ� ü��
    [Range(1f, 20f)] public float speed; //ĳ������ �̵� �ӵ�
    public int atk;//ĳ������ ���ݷ�
    public int def;//ĳ������ ����
    public string characterName; //ĳ������ �̸�
    public string info; //ĳ���� ���� - ������ �ʴ´�.
    public AttackSO attackSO; //���� ����
}
