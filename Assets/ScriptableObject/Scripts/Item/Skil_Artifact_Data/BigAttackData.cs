using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemSO/BigAttackData")]
public class BigAttackData : ItemSO
{
    BigAttackData()
    {
        Name = "BigAttack";
        Damage = 10;
    }
    public override void ApplyEffect(GameObject target)
    {
        // TODO 미정
    }
}
