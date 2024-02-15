using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemInMap : MonoBehaviour
{
    int x;
    int y;

    private void Start()
    {
        DropItem();
    }


    private void DropItem()
    {
        x = 5;
        y = 2 + (int)gameObject.transform.position.y;
        for (int i = 0; i < 4; i++)
        {
            GameObject item = Instantiate(ItemManager.Instance.MakeItem());
            if(i%2 == 0)
            {
                x *= -1;
            }
            if(i == 2 || i == 3)
            {
                y += -4;
            }
            item.transform.position = new Vector3(x, y, 0);
        }
    }
}
