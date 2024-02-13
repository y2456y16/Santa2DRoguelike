using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemSO : ScriptableObject
{
    public string Name;
    public Sprite Sprite;
    public abstract void ApplyEffect(GameObject target);
}

