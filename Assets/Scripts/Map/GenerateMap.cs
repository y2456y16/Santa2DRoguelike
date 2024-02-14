using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{
    [SerializeField] private MapInfo[] Maps;
    [SerializeField] private int MapsCount;
    [SerializeField] private GameObject[] MapPrefabs;

    private void Start()
    {
        //SetRoom();
    }

    public void SetRoom()
    {
        Maps = new MapInfo[MapsCount];

        Maps[0] = new MapInfo(ROOM_TYPE.StartRoom);
        Maps[MapsCount - 1] = new MapInfo(ROOM_TYPE.BossRoom);
        Maps[Random.Range(1, MapsCount - 1)] = new MapInfo(ROOM_TYPE.ItemRoom);
        for (int i = 0; i < MapsCount; i++)
        {
            if (Maps[i] == null)
                Maps[i] = new MapInfo(ROOM_TYPE.NormalRoom);
        }
    }

    public void GenerateRoom()
    {
        //int cnt = 0;
        
        //foreach(MapInfo map in Maps)
        //{
            

            
        //}
    }
}