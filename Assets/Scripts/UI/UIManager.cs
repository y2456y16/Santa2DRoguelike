using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Heart")]
    private GameObject heart;
    public GameObject heartParent;
    [HideInInspector]public int heart_count;
    private List<GameObject> Hearts = new List<GameObject>();

    [Header("Stats")]
    public TMP_Text playerAtk_Text;
    public TMP_Text playerDef_Text;
    public TMP_Text playerSpeed_Text;


    [Header("Item")]
    public GameObject[] items = new GameObject[3]; //GameObject말고 아이템 설정해둔 script로 설정필요할 것 같음.
    public int[] item_count = new int[3];
    public TMP_Text[] itemsText = new TMP_Text[3];
    



    private void Awake()
    {
        heart = Resources.Load<GameObject>("Prifabs/Heart");
    }

    private void Start()
    {
        MakeHeart();
        SetPlayerStats();
    }

    private void MakeHeart()
    {
        heart_count = GameManager.instance.player_health;
        for(int i = 0; i < heart_count; i++)
        {
            GameObject newHeart = Instantiate(heart);
            newHeart.transform.parent = heartParent.transform;
            Hearts.Add(newHeart);
        }
    }

    private void SetPlayerStats()
    {
        playerAtk_Text.text = GameManager.instance.player_atk.ToString();
        playerDef_Text.text = GameManager.instance.player_def.ToString();
        playerSpeed_Text.text = GameManager.instance.player_speed.ToString();
    }


    private void GetItem()//매개변수로 아이템 sprite랑 아이템명 가져오기. 아니면 아이템 자체를 받기
    {
        for(int i = 0; i < 3; i++)
        {
            if (items[i] == null)
            {
                items[i] = Resources.Load<GameObject>("item"); //먹은 아이템 넣어두기, 아이템 정보 필요함
                item_count[i] = 1;
                //ItemText = items[i].transform.Find("count").GameObject; 아이템갯수 표기 text 설정해줘야함.
                ItemText(i, item_count[i]);
                break;
            }
        }
        //아이템 칸이 다 차서 먹지 못하게 해야함.
    }

    private void ItemText(int index, int itemCount)
    {
        itemsText[index].text = itemCount.ToString();
    }
}
