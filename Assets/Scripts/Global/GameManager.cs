using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public EnemyPrefabManager _EnemyPrefabManager;
    public GameObject _Enemy;
    public Transform Player { get; private set; }
    public HealthSystem healthSystem;
    public UIManager uiManager;

    public GameObject gameOverUI;   // 게임오버UI

    [SerializeField] private string playerTag = "Player";

    public List<Vector3> enemyLocation = new List<Vector3>();//enemy start location list
                                                             // Start is called before the first frame update


    public CharacterStatsHandler characterStats;
    [HideInInspector] public int player_health;
    [HideInInspector] public float player_speed;
    [HideInInspector] public int player_atk;
    [HideInInspector] public int player_def;
    [HideInInspector] public StatsChangeType player_type;

    [Header("Test")]
    public Item testitem;

    private void Awake()
    {
        Instance = this;
        Player = GameObject.FindGameObjectWithTag(playerTag).transform;//gets transform data of object(tag == player)
        characterStats = Player.GetComponent<CharacterStatsHandler>();
        //EnemyLocationSet();

        healthSystem = Player.GetComponent<HealthSystem>();
        healthSystem.OnDeath += gameOver;
    }

    void gameOver()
    {
        StartCoroutine(GameOver());
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1.5f);
        Time.timeScale = 0;
        gameOverUI.SetActive(true);
    }

    void Start()
    {
        SetPlayerStats();
        Time.timeScale = 1f;
        //Invoke("EnemyCreate", 0.5f);
        //Invoke("EnemyCreate", 0.5f);
        //BossCreate();
    }

    public void EnemyCreate()
    {
        int randomNumb = Random.Range(0, _EnemyPrefabManager.EnemyNumber);
        GameObject enemyInstance = Instantiate(_EnemyPrefabManager.EnemyList[randomNumb]);//prefab 복제하여 적 객체 생성
        enemyInstance.transform.SetParent(_Enemy.transform);
        int enemyLocationlist = Random.Range(0, enemyLocation.Count);
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

    public void BossCreate(Vector3 bossPos)
    {
        GameObject BossInstance = Instantiate(_EnemyPrefabManager.BossList[0]);
        BossInstance.transform.position = bossPos;
    }

    public void SetPlayerStats()
    {
        player_health = characterStats.CurrentStats.maxHealth;
        player_atk = characterStats.CurrentStats.atk;
        player_def = characterStats.CurrentStats.def;
        player_speed = characterStats.CurrentStats.speed;
    }
    public void RetryGame()
    {
        Time.timeScale = 1f;
        healthSystem.ChangeHealth(characterStats.CurrentStats.maxHealth);
        Player.transform.position = Vector3.zero;
        SceneManager.LoadScene("MainScene");
        gameOverUI.SetActive(false);
    }
    public void ExitGame()
    {
        SceneManager.LoadScene("IntroScene");
    }
}
