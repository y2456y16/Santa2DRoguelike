using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemSO data;
    public int count; 
    public bool IsUsed { get; protected set; }
    protected virtual void Start()
    {
        count = 1;
    }
    public void ApplyEffect(GameObject target)
    {
        //TODO 아이템 종류에 따라 어디 슬록에 들어갈 지 결정
        data.ApplyEffect(target);
    }
    public virtual void Use(GameObject target)
    {

    }
}
