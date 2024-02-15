using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    [SerializeField] private List<Item> useItems;
    //AddItem을 통해 얻은 모든 아이템은 여기에 저장된다.
    private Dictionary<ItemID, Item> curItems = new Dictionary<ItemID, Item>();
    public Item curSkill { get; private set; }
    private GameObject itemObject;
    private int usingItemIndex;
    private void Awake()
    {
        Instance = this;
    }

    public void AddItem(Item item)
    {
        if (!curItems.ContainsKey(item.data.ID))
        {
            curItems.Add(item.data.ID, FindByID(item.data.ID));
            UIManager.Instance.MakeItemSlot(item.data.Type, item.data.Sprite,item.data.ID);
            if (item.data.Type == ItemType.Skill)
            {
                if(curSkill != null && curSkill.data.ID != item.data.ID)
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

            //아이템 타입이 버프일 경우 슬롯이 생성되고[완], 버프가 적용이 되야하고, 같은 것은 중복되지 않도록.
            else if (item.data.Type == ItemType.Buff)
            {
            }

            else if (item.data.Type == ItemType.Useable)
            {
            }
            item.ApplyEffect(GameManager.Instance.Player.gameObject);
        }
        else
        {
            if(item.data.Type == ItemType.Useable)
            {
                for(int i = 0; i < UIManager.Instance.usableItems.Length; i++)
                {
                    if(UIManager.Instance.usableItems[i] != null)
                    {
                        if (UIManager.Instance.usableItems[i].data.ID == item.data.ID)
                        {
                            usingItemIndex = i;
                            UIManager.Instance.usableItems[i].count++;
                            UIManager.Instance.UpdateItemCount(usingItemIndex);
                            break;
                        }
                    }
                }
            }
        }
        Destroy(item.gameObject);
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

    public GameObject MakeItem()
    {
        GameObject newItem;
        int index = Random.Range(0, items.Count + skills.Count + useItems.Count);
        if(index < items.Count)
        {
            newItem = Instantiate(items[index].gameObject);
        }
        else if(index < items.Count + skills.Count)
        {
            newItem = Instantiate(skills[index - items.Count].gameObject);
        }
        else
        {
            newItem = Instantiate(skills[index - items.Count - skills.Count].gameObject);
        }

        return newItem;
    }

    //
    public Item FindUseItemByID(ItemID ID)
    {
        foreach (Item item in useItems)
        {
            if (item.data.ID == ID)
            {
                return item;
            }
        }
        Debug.Log("데이터 베이스에 존재하지 않는 아이템 입니다");
        return null;
    }
    public Item FindByID(ItemID ID)
    {
        foreach(Item item in items)
        {
            if(item.data.ID == ID)
            {
                return item;
            }
        }
        Debug.Log("데이터 베이스에 존재하지 않는 아이템 입니다");
        return null;
    }
    //For Debug
    public void ShowCurItem()
    {
        foreach(ItemID ID in curItems.Keys)
        {
            Debug.Log(ID);
        }
    }
}


