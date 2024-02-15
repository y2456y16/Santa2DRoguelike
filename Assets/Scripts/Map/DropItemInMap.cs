using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemInMap : MonoBehaviour
{
    int x;
    int y;

    public GameObject[] items = new GameObject[4];

    private void Start()
    {
        DropItem();
    }


    private void DropItem()
    {
        for (int i = 0; i < 4; i++)
        {
            Item item = ItemManager.Instance.MakeItem();
            items[i].AddComponent<Item>();
            items[i].GetComponent<Item>().data = item.data;
            items[i].GetComponent<SpriteRenderer>().sprite = item.data.Sprite;
        }
    }
}
