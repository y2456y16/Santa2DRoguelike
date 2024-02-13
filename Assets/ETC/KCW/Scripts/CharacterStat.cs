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
public class CharacterStat //캐릭터의 스텟 정보
{
    public CharacterType type; //캐릭터의 종류 - 사용되지 않는다.
    public StatsChangeType statsChangeType; //스텟의 유형 - 사용되지 않는다.
    [Range(1, 100)] public int maxHealth; //캐릭터의 최대 체력
    [Range(1f, 20f)] public float speed; //캐릭터의 이동 속도
    public int atk;//캐릭터의 공격력
    public int def;//캐릭터의 방어력
    public string characterName; //캐릭터의 이름
    public string info; //캐릭터 정보 - 사용되지 않는다.
    public AttackSO attackSO; //공격 정보
}
