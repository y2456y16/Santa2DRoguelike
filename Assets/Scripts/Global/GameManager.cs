using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public EnemyPrefabManager _EnemyPrefabManager;
    public Transform Player { get; private set; }
    [SerializeField] private string playerTag = "Player";


    private CharacterStatsHandler playerStats;
    [HideInInspector] public StatsChangeType player_type;
    [HideInInspector] public int player_health;
    [HideInInspector] public int player_atk;
    [HideInInspector] public int player_def;
    [HideInInspector] public float player_speed;


    public List<Vector3> enemyLocation = new List<Vector3>();//evemy start location list
                                                             // Start is called before the first frame update

    private void Awake()
    {
        Instance = this;
        Player = GameObject.FindGameObjectWithTag(playerTag).transform;//gets transform data of object(tag == player)
        playerStats = Player.GetComponent<CharacterStatsHandler>();
        EnemyLocationSet();
        PlayerSetting();
    }
    void Start()
    {
        Time.timeScale = 1f;
        InvokeRepeating("EnemyCreate", 0.5f, 2f);
    }

    private void PlayerSetting()
    {
        player_health = playerStats.CurrentStats.maxHealth;
        player_type = playerStats.CurrentStats.statsChangeType;
        player_speed = playerStats.CurrentStats.speed;
        player_atk = playerStats.CurrentStats.atk;
        player_def = playerStats.CurrentStats.def;
    }


    public void EnemyCreate()
    {
        int randomNumb = Random.Range(0, _EnemyPrefabManager.EnemyNumber);
        GameObject enemyInstance = Instantiate(_EnemyPrefabManager.EnemyList[randomNumb]);//prefab �����Ͽ� �� ��ü ����
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
