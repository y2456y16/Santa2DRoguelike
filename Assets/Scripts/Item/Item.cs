using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemSO data;
    public void Equire(GameObject target)
    {
        Debug.Log("Equired Item");
        data.ApplyEffect(target);
    }

    public void NewEquire(GameObject target)
    {
        Debug.Log("Equired Item Test"); // For Debug
        data.NewApplyEffect?.Invoke(target);
    }
}
