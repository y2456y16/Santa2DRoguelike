using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;

    [SerializeField] private List<Item> items;
    private Dictionary<ItemID, Item> curItems = new Dictionary<ItemID, Item>();

    // test용 임시 변수
    [SerializeField] private GameObject player;

    private void Awake()
    {
        Instance = this;
    }

    public void AddItem(Item item)
    {
        if (!curItems.ContainsKey(item.data.ID))
        {
            curItems.Add(item.data.ID, item);
            item.ApplyEffect(player);
        }
        else
        {
            if(item.data.Type == ItemType.Useable)
            {
                curItems[item.data.ID].data.Count++;
            }
        }
    }

    public Item GetItem(ItemID ID)
    {
        return curItems[ID];
    }

}


