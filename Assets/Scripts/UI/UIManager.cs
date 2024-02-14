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
        SetPlayerStats();
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

    private void SetPlayerStats()
    {
        playerAtk_Text.text = GameManager.Instance.player_atk.ToString();
        playerDef_Text.text = GameManager.Instance.player_def.ToString();
        playerSpeed_Text.text = GameManager.Instance.player_speed.ToString();
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
