using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public EnemyPrefabManager _EnemyPrefabManager;
    public Transform Player { get; private set; }
    [SerializeField] private string playerTag = "Player";

    public List<Vector3> enemyLocation = new List<Vector3>();//enemy start location list
                                                             // Start is called before the first frame update


    private CharacterStatsHandler characterStats;
    [HideInInspector] public int player_health;
    [HideInInspector] public float player_speed;
    [HideInInspector] public int player_atk;
    [HideInInspector] public int player_def;
    [HideInInspector] public StatsChangeType player_type;


    private void Awake()
    {
        Instance = this;
        Player = GameObject.FindGameObjectWithTag(playerTag).transform;//gets transform data of object(tag == player)
        characterStats = Player.GetComponent<CharacterStatsHandler>();
        EnemyLocationSet();
        
    }
    void Start()
    {
        SetPlayerStats();
        Time.timeScale = 1f;
        //InvokeRepeating("EnemyCreate", 0.5f, 2f);
        BossCreate();
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

    public void BossCreate()
    {
        GameObject BossInstance = Instantiate(_EnemyPrefabManager.BossList[0]);
        BossInstance.transform.position = new Vector3(3f, 0f, 0);
    }

    void SetPlayerStats()
    {
        player_health = characterStats.CurrentStats.maxHealth;
        player_atk = characterStats.CurrentStats.atk;
        player_def = characterStats.CurrentStats.def;
        player_speed = characterStats.CurrentStats.speed;
    }
}
