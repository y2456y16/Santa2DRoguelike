using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultAttackData", menuName = "Controller/Attack/Default", order = 0)]
public class AttackSO : ScriptableObject
{
    [Header("Attack Info")]//공통 공격 정보
    public float size; //공격의 사이즈
    public float delay; //공격의 딜레이
    public float power;//공격력
    public float speed;//공격속도
    public LayerMask target;//공격할 대상의 LayerMask

    [Header("Knock Back Info")]//넉백 정보
    public bool isOnKnockback;//넉백 여부
    public float knockbackPower;//넉백의 힘
    public float knockbackTime;//넉백 지속시간
}

