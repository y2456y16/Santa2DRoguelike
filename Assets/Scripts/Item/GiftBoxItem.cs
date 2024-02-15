using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftBoxItem : Item
{
    public override void Use(GameObject target)
    {
        if (data.Type == ItemType.Useable)
        {
            //방 안에 보스를 제외한 모든 몬스터 사망
            //방 안에 모든 몬스터 일시정지
            //10초동안 공격력 2배 상승
            StartCoroutine(IncreaseAttack(target, 20f));
        }
    }

    public IEnumerator IncreaseAttack(GameObject target, float durationTime)
    {
        CharacterStatsHandler playerStat = target.GetComponent<CharacterStatsHandler>();
        CharacterStats newStat = new CharacterStats();
        newStat.statsChangeType = StatsChangeType.Multiple;
        newStat.atk = 2;
        playerStat.AddStatModifier(newStat);
        yield return new WaitForSeconds(durationTime);
        playerStat.RemoveStatModifier(newStat);
    }
}
