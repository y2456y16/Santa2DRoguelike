using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemSO/Manager_s_BlessingsData")]
public class Manager_s_BlessingsData : ItemSO
{
    Manager_s_BlessingsData()
    {
        Name = "Manager_s_Blessing";
    }
    public override void ApplyEffect(GameObject target)
    {
        // TODO 플레이어 목숨 증가
    }
}
