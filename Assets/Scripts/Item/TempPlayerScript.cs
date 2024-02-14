using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlayerScript : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(ItemManager.Instance.curSkill != null)
            {
                Instantiate(ItemManager.Instance.curSkill, transform.position , Quaternion.identity);
            }
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
