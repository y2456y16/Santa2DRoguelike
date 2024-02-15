using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemSO/SantaShoesData")]
public class SantaShoesData : ItemSO
{
    public float Speed = 1f;
    SantaShoesData()
    {
        Name = "SantaShoes";
    }
    public override void ApplyEffect(GameObject target)
    {
        // TODO 플레이어 이동속도 증가
        CharacterStatsHandler playerStat = target.GetComponent<CharacterStatsHandler>();
        CharacterStats newStat = new CharacterStats();
        newStat.statsChangeType = StatsChangeType.Add;
        newStat.speed = Speed;
        playerStat.AddStatModifier(newStat);
        UIManager.Instance.SetStatsText();
    }
}