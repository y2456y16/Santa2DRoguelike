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
    [SerializeField] private List<GameObject> Hearts = new List<GameObject>();

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


    public GameObject[] usableItems = new GameObject[3]; //GameObject말고 아이템 설정해둔 script로 설정필요할 것 같음.
    public int[] item_count = new int[3];
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
        MakeHeart(false);
        SetStatsText();
    }

    public void MakeHeart(bool resurrection)
    {
        heart_count = GameManager.Instance.characterStats.CurrentStats.maxHealth;
        for (int i = 0; i < heart_count; i++)
        {
            GameObject newHeart = Instantiate(heart);
            newHeart.transform.parent = heartParent.transform;
            if (resurrection)
            {
                Hearts[i] = newHeart;
            }
            else
            {
                Hearts.Add(newHeart);
            }
        }
    }

    public void MakeBlueHeart()
    {
        GameObject newHeart = Instantiate(blueHeart);
        newHeart.transform.parent = heartParent.transform;
        if (Hearts[Hearts.Count - 1] != null)
        {
            Hearts.Add(newHeart);
        }
        else
        {
            for (int i = 0; i < Hearts.Count; i++)
            {
                if (Hearts[i] == null)
                {
                    Hearts[i] = newHeart;
                    break;
                }
            }
            Hearts.Add(null);
        }
    }

    public void BrokenHeart()
    {
        if (Hearts[Hearts.Count - 1] != null)
        {
            Destroy(Hearts[Hearts.Count - 1]);
        }
        else
        {
            for (int i = 0; i < Hearts.Count; i++)
            {
                if (Hearts[i] == null)
                {
                    try
                    {
                        Destroy(Hearts[i - 1]);
                    }
                    catch
                    {  }
                    break;
                }
            }
        }
    }

    public void SetStatsText()
    {
        GameManager.Instance.SetPlayerStats();
        playerAtk_Text.text = GameManager.Instance.player_atk.ToString();
        playerDef_Text.text = GameManager.Instance.player_def.ToString();
        playerSpeed_Text.text = GameManager.Instance.player_speed.ToString();
    }

    public void MakeItemSlot(ItemType itemType, Sprite itemsprite)
    {
        if(itemType == ItemType.Useable)
        {
            for (int i = 0; i < 3; i++)
            {
                if (usableItems[i] == null)
                {
                    usableItems[i] = Resources.Load<GameObject>("item"); //먹은 아이템 넣어두기, 아이템 정보 필요함
                    item_count[i] = 1; //itemManager Count가 1로 설정되어있으면 그거 가져오기
                    GameObject newItemUsable = Instantiate(itemUsable);
                    newItemUsable.transform.Find("Image").GetComponent<Image>().sprite = itemsprite;
                    newItemUsable.transform.parent = itemUsableParent.transform;
                    //ItemText = items[i].transform.Find("count").GameObject; 아이템갯수 표기 text 설정해줘야함.
                    //ItemText(i, item_count[i]);
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
}
