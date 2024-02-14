using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Player")]
    public GameObject player;
    [HideInInspector]public int player_health;


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
    }
}
