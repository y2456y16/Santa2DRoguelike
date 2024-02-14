using System;
using UnityEngine;


public abstract class ItemSO : ScriptableObject
{
    [Header("ItemInfo")]
    public ItemID ID;
    public string Name;
    public Sprite Sprite;
    public ItemType Type;
    public int Count;
    public float Damage;
    
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
    BigAttack,

    // Item
    Gift,
    GodModePotion,

}

