using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatsChangeType
{
    Add,
    Multiple,
    Override,
}

[Serializable]
public class CharacterStats
{
    public StatsChangeType statsChangeType;
    [Range(1, 100)] public int maxHealth;   //ü��
    public int atk;                         //���ݷ�
    public int def;                         //����
    [Range(1f, 20f)] public float speed;    //���ǵ�
    public AttackSO attackSO;
}