using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

/// <summary>
/// 이를 이용하여 인벤토리 창을 열때마다 curItem을 순회하고
/// item의 type에 따라 배치하면 될거 같다.
/// </summary>
public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;

    [SerializeField] private List<Item> items;
    [SerializeField] private List<Item> skills;
    //AddItem을 통해 얻은 모든 아이템은 여기에 저장된다.
    private Dictionary<ItemID, Item> curItems = new Dictionary<ItemID, Item>();
    public Item curSkill;
    // test용 임시 변수
    [SerializeField] public Transform player;

    private void Awake()
    {
        Instance = this;
    }

    public void AddItem(Item item)
    {
        if (!curItems.ContainsKey(item.data.ID))
        {
            curItems.Add(item.data.ID, item);
            if (item.data.Type == ItemType.Skill && curSkill != null && curSkill.data.ID != item.data.ID)
            {
                RemoveItem(curSkill.data.ID);
                foreach (Item skill in skills)
                {
                    if (skill.data.ID == item.data.ID)
                    {
                        curSkill = skill;
                        break;
                    }
                }
            }
            item.ApplyEffect(player.gameObject);
        }
        else
        {
            if(item.data.Type == ItemType.Useable)
            {
                curItems[item.data.ID].data.Count++;
            }
        }
    }
    public void RemoveItem(ItemID ID)
    {
        if (curItems.ContainsKey(ID))
            curItems.Remove(ID);
    }

    public Item GetItem(ItemID ID)
    {
        if (curItems.ContainsKey(ID))
            return curItems[ID];
        else
            return null;
    }

}


