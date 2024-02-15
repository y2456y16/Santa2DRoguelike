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
            Item item = Instantiate(ItemManager.Instance.MakeItem());
            item.transform.position = items[i].transform.position;
            item.transform.parent = items[i].transform;
        }
    }
}
