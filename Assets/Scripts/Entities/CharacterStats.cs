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
    [Range(1, 100)] public int maxHealth;   //체력
    public int atk;                         //공격력
    public int def;                         //방어력
    [Range(1f, 20f)] public float speed;    //스피드
    public AttackSO attackSO;
}