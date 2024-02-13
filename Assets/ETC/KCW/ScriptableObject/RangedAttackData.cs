using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RangedAttackData", menuName = "Controller/Attack/Ranged", order = 1)]
public class RangedAttackData : AttackSO
{
    [Header("Ranged Attack Data")]//원거리 공격 정보
    public string bulletNameTag; //투사체의 태그
    public float duration;//투사체의 지속시간
    public float spread;//투사체의 퍼지는 정도 = 정확도
    public int numberofProjectilesPerShot;//한 번에 날아갈 투사체의 수
    public float multipleProjectilesAngel;//투사체 간 떨어진 간격
}
