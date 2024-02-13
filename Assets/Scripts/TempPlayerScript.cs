using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlayerScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            Debug.Log("Item Encount");
            collision.gameObject.GetComponent<Item>().Equire(gameObject);
            Debug.Log("Item Encount?");
        }
    }
}
