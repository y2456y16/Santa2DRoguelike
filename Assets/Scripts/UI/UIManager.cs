using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Heart")]
    private GameObject heart;
    private GameObject blueHeart;
    public GameObject heartParent;
    [HideInInspector] public int heart_count;
    private List<GameObject> Hearts = new List<GameObject>();

    [Header("Stats")]
    public TMP_Text playerAtk_Text;
    public TMP_Text playerDef_Text;
    public TMP_Text playerSpeed_Text;

    [Header("Item")]
    public GameObject itemSlotParent;
    private GameObject itemSlot;
    public GameObject itemUsableParent;
    private GameObject itemUsable;
    public GameObject itemSkill;


    public Item[] usableItems = new Item[3]; //GameObject말고 아이템 설정해둔 script로 설정필요할 것 같음.
    public int[] item_count = new int[3];
    public GameObject[] itemSlots = new GameObject[3];
    public TMP_Text[] itemsText = new TMP_Text[3];
    List<GameObject> buffItems = new List<GameObject>();


    public static UIManager Instance;

    private void Awake()
    {
        heart = Resources.Load<GameObject>("Prifabs/Heart");
        blueHeart = Resources.Load<GameObject>("Prifabs/blueheart");
        itemSlot = Resources.Load<GameObject>("Prifabs/itemslot");
        Instance = this;
    }

    private void Start()
    {
        MakeHeart();
        SetStatsText();
    }

    private void MakeHeart()
    {
        heart_count = GameManager.Instance.player_health;
        for(int i = 0; i < heart_count; i++)
        {
            GameObject newHeart = Instantiate(heart);
            newHeart.transform.parent = heartParent.transform;
            Hearts.Add(newHeart);
        }
    }

    public void MakeBlueHeart()
    {
        GameObject newHeart = Instantiate(blueHeart);
        newHeart.transform.parent = heartParent.transform;
        Hearts.Add(newHeart);
    }

    public void SetStatsText()
    {
        GameManager.Instance.SetPlayerStats();
        playerAtk_Text.text = GameManager.Instance.player_atk.ToString();
        playerDef_Text.text = GameManager.Instance.player_def.ToString();
        playerSpeed_Text.text = GameManager.Instance.player_speed.ToString();
    }

    public void MakeItemSlot(ItemType itemType, Sprite itemsprite, ItemID ID = ItemID.Blueheart)
    {
        if(itemType == ItemType.Useable)
        {
            for (int i = 0; i < 3; i++)
            {
                if (usableItems[i] == null)
                {
                    usableItems[i] = Instantiate(ItemManager.Instance.FindUseItemByID(ID), new Vector3(-10000f,-10000f,0f), Quaternion.identity); //먹은 아이템 넣어두기, 아이템 정보 필요함
                    item_count[i] = usableItems[i].count;
                    itemSlots[i].transform.Find("Image").GetComponent<Image>().sprite = itemsprite;
                    ItemText(i, item_count[i]);
                    break;
                }
            }
            //아이템 칸이 다 차서 먹지 못하게 해야함.
        }
        else if(itemType == ItemType.Buff)
        {
            GameObject newItemSlot = Instantiate(itemSlot);
            newItemSlot.transform.Find("front").GetComponent<Image>().sprite = itemsprite;
            newItemSlot.transform.parent = itemSlotParent.transform;
        }
        else if(itemType == ItemType.Skill)
        {
            itemSkill.transform.Find("front").GetComponent<Image>().color = Color.white;
            itemSkill.transform.Find("front").GetComponent<Image>().sprite = itemsprite;
        }
    }

    private void ItemText(int index, int itemCount)
    {
        itemsText[index].text = itemCount.ToString();
    }

    public void UpdateItemCount(int index)
    {
        item_count[index] = usableItems[index].count;
        ItemText(index, item_count[index]);
       
    }
}
