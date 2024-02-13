using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [Serializable]
    public struct Pool
    {
        public string tag;
        public GameObject prefeb;
        public int size;
    }

    public List<Pool> pools; //����ü�� ����Ʈ
    public Dictionary<string, Queue<GameObject>> poolDictionary;//������Ʈ Ǯ ��ųʸ�

    private void Awake()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (var pool in pools) //����Ʈ���� �ϳ��� ������
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++) //pool�� �����ŭ
            {
                GameObject obj = Instantiate(pool.prefeb);
                obj.SetActive(false);
                objectPool.Enqueue(obj); //ť�� �ְ�
            }
            poolDictionary.Add(pool.tag, objectPool);//��ųʸ� [tag] �� �ִ´�.
        }
    }

    public GameObject SpawnFromPool(string tag)
    {
        if (!poolDictionary.ContainsKey(tag)) //��ųʸ��� tag�� �������� ������
            return null;

        GameObject obj = poolDictionary[tag].Dequeue(); // [tag]���� �ϳ��� ������.
        poolDictionary[tag].Enqueue(obj);//�ٽ� �ִ´�.

        return obj;
    }
}
