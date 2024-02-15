using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ItemSO/SantaGloveData")]
public class SantaGloveData : ItemSO
{
    public int Power = 10;
    SantaGloveData()
    {
        Name = "SantaGlove";
    }
    public override void ApplyEffect(GameObject target)
    {
        // TODO 플레이어 공격력 증가
        CharacterStatsHandler playerStat = target.GetComponent<CharacterStatsHandler>();
        CharacterStats newStat = new CharacterStats();
        newStat.statsChangeType = StatsChangeType.Add;
        newStat.atk = Power;
        playerStat.AddStatModifier(newStat);
    }

}
