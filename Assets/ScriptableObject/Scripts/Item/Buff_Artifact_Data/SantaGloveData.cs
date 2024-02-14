using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ItemSO/SantaGloveData")]
public class SantaGloveData : ItemSO
{
    public float Power = 10f;
    SantaGloveData()
    {
        Name = "SantaGlove";
    }
    public override void ApplyEffect(GameObject target)
    {
        // TODO 플레이어 공격력 증가
        CharacterStatsHandler playerStat = target.GetComponent<CharacterStatsHandler>();
        if (playerStat != null)
        {
            playerStat.CurrentStats.attackSO.power += Power;
        }
       
    }
}
