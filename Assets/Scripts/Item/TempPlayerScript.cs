using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlayerScript : MonoBehaviour
{
    public GiftBoxBoom giftBoom;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(giftBoom, transform);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            ItemManager.Instance.AddItem(collision.gameObject.GetComponent<Item>());
        }
    }
}
