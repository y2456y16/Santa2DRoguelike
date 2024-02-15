using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemSO/BlueHeartData")]
public class BlueHeartData : ItemSO
{    
    BlueHeartData()
    {
        Name = "BlueHeart";
    }
    public override void ApplyEffect(GameObject target)
    {
        // 하트 UI도 변경 필요해 보임
        CharacterStatsHandler playerStat = target.GetComponent<CharacterStatsHandler>();
        CharacterStats newStat = new CharacterStats();
        newStat.statsChangeType = StatsChangeType.Add;
        newStat.maxHealth += 1;

        //플레이어의 체력도 업데이트 해야한다
        target.GetComponent<HealthSystem>().ChangeHealth(1);
        playerStat.AddStatModifier(newStat);
    }
}
