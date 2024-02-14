using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemSO data;
    public void ApplyEffect(GameObject target)
    {
        //TODO 아이템 종류에 따라 어디 슬록에 들어갈 지 결정
        Debug.Log($"Equired Item : {gameObject.name}");
        data.ApplyEffect(target);
    }
    public void Use(GameObject target)
    {
        if(data.Type == ItemType.Useable)
        {
            data.Use(target);
        }
    }
}
