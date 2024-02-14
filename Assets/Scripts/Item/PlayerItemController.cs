using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerItemController : MonoBehaviour
{
    private TopDownCharacterController _controller;
    private void Awake()
    {
        _controller = GetComponent<TopDownCharacterController>();
    }
    private void Start()
    {
        _controller.OnSkillEvent -= DoSkill;
        _controller.OnSkillEvent += DoSkill;
    }
    public void UseItem(ItemID ID)
    {
        Item item = ItemManager.Instance.GetItem(ID);
        if (item != null)
        {
            if(item.data.Type == ItemType.Useable && item.data.Count > 0)
            {
                item.Use(gameObject);
            }
        }
    }
    public void DoSkill()
    {
        if (ItemManager.Instance.curSkill != null)
        {
            Instantiate(ItemManager.Instance.curSkill, transform.position, Quaternion.identity);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            Item item = collision.gameObject.GetComponent<Item>();
            ItemManager.Instance.AddItem(item);
            if(item.data.Type == ItemType.Skill)
            {
                _controller.OnSkillEvent -= DoSkill;
                _controller.OnSkillEvent += DoSkill;
            }
        }
    }

}
