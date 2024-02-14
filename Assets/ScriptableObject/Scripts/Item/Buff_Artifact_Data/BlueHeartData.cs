using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemSO/BlueHeartData")]
public class BlueHeartData : ItemSO
{
    BlueHeartData()
    {
        Name = "BlueHeart";
    }
    public override void ApplyEffect(GameObject target)
    {
        // TODO 추가 하트 생성 로직
    }
}
