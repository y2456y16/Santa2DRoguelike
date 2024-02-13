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

    public List<Pool> pools; //투사체의 리스트
    public Dictionary<string, Queue<GameObject>> poolDictionary;//오브젝트 풀 딕셔너리

    private void Awake()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (var pool in pools) //리스트에서 하나씩 꺼내서
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++) //pool의 사이즈만큼
            {
                GameObject obj = Instantiate(pool.prefeb);
                obj.SetActive(false);
                objectPool.Enqueue(obj); //큐에 넣고
            }
            poolDictionary.Add(pool.tag, objectPool);//딕셔너리 [tag] 에 넣는다.
        }
    }

    public GameObject SpawnFromPool(string tag)
    {
        if (!poolDictionary.ContainsKey(tag)) //딕셔너리에 tag가 존재하지 않으면
            return null;

        GameObject obj = poolDictionary[tag].Dequeue(); // [tag]에서 하나를 꺼낸다.
        poolDictionary[tag].Enqueue(obj);//다시 넣는다.

        return obj;
    }
}
