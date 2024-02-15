using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemSO/SantaSunGlassesData")]
public class SantaSunGlassesData : ItemSO
{
    public int Attack = 1;
    public int Defense = 1;
    public float Speed = 1f;
    SantaSunGlassesData()
    {
        Name = "SantaSunGlasses";
    }
    public override void ApplyEffect(GameObject target)
    {
        // TODO 공격력 방어력 스피드 1씩 올리기
        CharacterStatsHandler playerStat = target.GetComponent<CharacterStatsHandler>();
        CharacterStats newStat = new CharacterStats();
        newStat.statsChangeType = StatsChangeType.Add;
        newStat.speed = Speed;
        newStat.atk = Attack;
        newStat.def = Defense;
        playerStat.AddStatModifier(newStat);
        UIManager.Instance.SetStatsText();
    }
}
