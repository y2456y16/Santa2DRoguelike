using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemSO/PotionData")]
public class PotionData : ItemSO
{
    public PotionType potionType;
    PotionData()
    {
        Name = "Potion";
    }
    public override void ApplyEffect(GameObject target)
    {
        // TODO 미정
    }
}

public enum PotionType
{
    GodMode,
    Attack,
    Defense
}
