using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Player")]
    public GameObject player;
    [HideInInspector] public StatsChangeType player_type;
    [HideInInspector] public int player_health;
    [HideInInspector] public int player_atk;
    [HideInInspector] public int player_def;
    [HideInInspector] public float player_speed;


    private CharacterStatsHandler playerStats;

    public static GameManager instance;
    private void Awake()
    {
        instance = this;
        playerStats = player.GetComponent<CharacterStatsHandler>();
    }

    private void Start()
    {
        PlayerSetting();
    }

    private void PlayerSetting()
    {
        player_health = playerStats.CurrentStats.maxHealth;
        player_type = playerStats.CurrentStats.statsChangeType;
        player_speed = playerStats.CurrentStats.speed;
        player_atk = playerStats.CurrentStats.atk;
        player_def = playerStats.CurrentStats.def;
    }
}
