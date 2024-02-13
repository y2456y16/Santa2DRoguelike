using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemSO data;
    public void Equire(GameObject target)
    {
        data.ApplyEffect(target);
    }
}
