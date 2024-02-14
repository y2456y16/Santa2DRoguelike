using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;


[CreateAssetMenu(menuName = "ItemSO/SantaGiftBundle")]
public class SantaGiftBundle : ItemSO
{
    public float Defense = 5;
    SantaGiftBundle()
    {
        Name = "SantaGiftBundle";
    }
    public override void ApplyEffect(GameObject target)
    {
        // TODO 플레이어 방어력 증가
        CharacterStatsHandler playerStat = target.GetComponent<CharacterStatsHandler>();
    }
}
