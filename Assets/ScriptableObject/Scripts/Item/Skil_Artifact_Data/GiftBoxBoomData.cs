using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemSO/GiftBoxBoomData")]
public class GiftBoxBoomData : ItemSO
{
    GiftBoxBoomData()
    {
        Name = "GiftBoxBoom";
        Damage = 20;
    }
    public override void ApplyEffect(GameObject target)
    {
        // TODO 미정
    }
}
