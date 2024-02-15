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
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UseItem(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UseItem(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UseItem(2);
        }
    }
    public void UseItem(int index)
    {
        Item item = UIManager.Instance.usableItems[index];
        if (item == null) return;
        
        if (item.data.Type == ItemType.Useable && item.count > 0)
        {
            if (!item.IsUsed)
            {
                UIManager.Instance.usableItems[index].count--;
                UIManager.Instance.UpdateItemCount(index);
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
