using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemSO/SantaSunGlassesData")]
public class SantaSunGlassesData : ItemSO
{
    SantaSunGlassesData()
    {
        Name = "SantaSunGlasses";
    }
    public override void ApplyEffect(GameObject target)
    {
        // TODO 모든 맵 밝히기
    }
}
