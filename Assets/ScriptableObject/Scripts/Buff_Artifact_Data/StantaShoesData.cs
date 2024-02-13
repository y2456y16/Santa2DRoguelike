using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemSO/SantaShoesData")]
public class SantaShoesData : ItemSO
{
    SantaShoesData()
    {
        Name = "SantaShoes";
    }
    public override void ApplyEffect(GameObject target)
    {
        // TODO 플레이어 이동속도 증가
    }
}