using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어와 아이템 상호작용 테스트용
/// </summary>

public class GWTestScript : MonoBehaviour
{
    public int Attack = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Item item = collision.gameObject.GetComponent<Item>();
        item.Equire(gameObject);
    }
}
