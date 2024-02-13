using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPrefabManager : MonoBehaviour
{
    public int EnemyNumber = 2;
    public GameObject Enemy1Prefab;
    public GameObject Enemy2Prefab;
    public List<GameObject> EnemyList = new List<GameObject>();

    private void Awake()
    {
        EnemyList.Add(Enemy1Prefab);
        EnemyList.Add(Enemy2Prefab);
    }
}
