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
    public int heart_count; //GameManager�� Player�� ������ ���� 
    private List<GameObject> Hearts = new List<GameObject>();


    //item
    [Header("Item")]
    public GameObject[] items = new GameObject[3]; //GameObject���� ������ �����ص� script�� �����ʿ��� �� ����.
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


    private void GetItem()//�Ű������� ������ sprite�� �����۸� ��������. �ƴϸ� ������ ��ü�� �ޱ�
    {
        for(int i = 0; i < 3; i++)
        {
            if (items[i] == null)
            {
                items[i] = Resources.Load<GameObject>("item"); //���� ������ �־�α�, ������ ���� �ʿ���
                item_count[i] = 1;
                //ItemText = items[i].transform.Find("count").GameObject; �����۰��� ǥ�� text �����������.
                ItemText(i, item_count[i]);
                break;
            }
        }
        //������ ĭ�� �� ���� ���� ���ϰ� �ؾ���.
    }

    private void ItemText(int index, int itemCount)
    {
        itemsText[index].text = itemCount.ToString();
    }
}