using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public EnemyPrefabManager _EnemyPrefabManager;
    public Transform Player { get; private set; }
    [SerializeField] private string playerTag = "Player";

    public List<Vector3> enemyLocation = new List<Vector3>();//evemy start location list
                                                             // Start is called before the first frame update

    private void Awake()
    {
        Instance = this;
        Player = GameObject.FindGameObjectWithTag(playerTag).transform;//gets transform data of object(tag == player)
        EnemyLocationSet();
    }
    void Start()
    {
        Time.timeScale = 1f;
        InvokeRepeating("EnemyCreate", 0.5f, 2f);
    }


    public void EnemyCreate()
    {
        int randomNumb = Random.Range(0, _EnemyPrefabManager.EnemyNumber);
        GameObject enemyInstance = Instantiate(_EnemyPrefabManager.EnemyList[randomNumb]);//prefab 복제하여 적 객체 생성
        int enemyLocationlist = Random.Range(0, 6);
        enemyInstance.transform.position = enemyLocation[enemyLocationlist];

    }

    void EnemyLocationSet()//enemy start location set
    {
        enemyLocation.Add(new Vector3(-3f, 4f, 0));
        enemyLocation.Add(new Vector3(3f, 4f, 0));
        enemyLocation.Add(new Vector3(-3f, 0f, 0));
        enemyLocation.Add(new Vector3(3f, 0f, 0));
        enemyLocation.Add(new Vector3(-3f, -4f, 0));
        enemyLocation.Add(new Vector3(3f, -4f, 0));
    }
}
