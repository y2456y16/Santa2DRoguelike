using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{
    [SerializeField] private MapInfo[] Dungeons;
    [SerializeField] private int DungeonsCount;
    private void Start()
    {
        Dungeons = new MapInfo[DungeonsCount];

        Dungeons[0] = new MapInfo(ROOM_TYPE.StartRoom, Vector2.zero);

        for (int i = 0; i < DungeonsCount; i++)
        {
            
        }
    }
}
