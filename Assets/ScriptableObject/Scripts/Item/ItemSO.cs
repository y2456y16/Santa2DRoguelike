using System;
using UnityEngine;


public abstract class ItemSO : ScriptableObject
{
    public ItemID ID;
    public string Name;
    public Sprite Sprite;
    public ItemType Type;
    public int Count;
    public abstract void ApplyEffect(GameObject target);
}
public enum ItemType
{
    Buff,
    Skill,
    Useable
}
public enum ItemID
{
    // Buff
    Blueheart,
    ManagerEngle,
    SantaGiftBundle,
    SantaShoes,
    SantaSunglass,
    SantaGlove,

    // Skill
    GiftBoxBoom,

    // Item
    Gift,
    GodModePotion,

}

