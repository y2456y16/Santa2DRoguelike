using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Heart")]
    private GameObject heart;
    public GameObject heartParent;
    public int heart_count; //GameManager나 Player로 가져올 예정 
    private List<GameObject> Hearts = new List<GameObject>();


    //item
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
    }

    private void MakeHeart()
    {
        for(int i = 0; i < heart_count; i++)
        {
            GameObject newHeart = Instantiate(heart);
            newHeart.transform.parent = heartParent.transform;
            Hearts.Add(newHeart);
        }
    }


    private void GetItem()//매개변수로 아이템 sprite랑 아이템명 가져오기
    {
        for(int i = 0; i < 3; i++)
        {
            if (items[i] == null)
            {
                items[i] = Resources.Load<GameObject>("item"); //먹은 아이템 넣어두기
                item_count[i] = 1;
                ItemText(i, item_count[i]);
            }
        }
    }

    private void ItemText(int index, int itemCount)
    {
        itemsText[index].text = itemCount.ToString();
    }
}
