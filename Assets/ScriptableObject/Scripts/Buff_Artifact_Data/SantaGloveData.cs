using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ItemSO/SantaGloveData")]
public class SantaGloveData : ItemSO
{
    SantaGloveData()
    {
        Name = "SantaGlove";
    }
    public override void ApplyEffect(GameObject target)
    {
        // TODO 플레이어 공격력 증가
        GWTestScript go = target.GetComponent<GWTestScript>();
        go.Attack += 10;
    }
}
