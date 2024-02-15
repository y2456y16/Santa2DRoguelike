using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftBoxItem : Item
{
    protected override void Start()
    {
        base.Start();
    }
    public override void Use(GameObject target)
    {
        base.Use(target);
        if (data.Type == ItemType.Useable)
        {
            //방 안에 보스를 제외한 모든 몬스터 사망
            //방 안에 모든 몬스터 일시정지          
        }
    }
}
