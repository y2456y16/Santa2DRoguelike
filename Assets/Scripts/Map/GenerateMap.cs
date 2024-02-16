using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{
    [SerializeField] private MapInfo[] mapInfos;
    private Map[] Maps;
    [SerializeField] private int MapsCount;
    [SerializeField] private GameObject[] NormalMapPrefabs;
    [SerializeField] private GameObject StartMapPrefab;
    [SerializeField] private GameObject BossMapPrefab;
    [SerializeField] private GameObject ItemMapPrefab;

    [SerializeField] private float mapDistance;

    private void Start()
    {
        SetRoom();
        GenerateRoom();
        DoorSetting();
    }



    public void SetRoom()
    {
        mapInfos = new MapInfo[MapsCount];
        Maps = new Map[MapsCount];

        MapInfo temp = new MapInfo(ROOM_TYPE.StartRoom);

        mapInfos[0] = temp;
        mapInfos[MapsCount - 1] = new MapInfo(ROOM_TYPE.BossRoom);
        mapInfos[Random.Range(1, MapsCount - 2)] = new MapInfo(ROOM_TYPE.ItemRoom);
        for (int i = 0; i < MapsCount; i++)
        {
            if (mapInfos[i] == null)
                mapInfos[i] = new MapInfo(ROOM_TYPE.NormalRoom);
        }
    }

    public void GenerateRoom()
    {
        int cnt = 0;
        
        foreach (MapInfo mapinfo in mapInfos)
        {
            GameObject obj;
            Door currentdoor;
            switch (mapinfo.eRoomType)
            {
                case ROOM_TYPE.StartRoom:
                    obj = Instantiate(StartMapPrefab);
                    break;
                case ROOM_TYPE.ItemRoom:
                    obj = Instantiate(ItemMapPrefab);
                    break;
                case ROOM_TYPE.BossRoom:
                    obj = Instantiate(BossMapPrefab);
                    break;
                case ROOM_TYPE.NormalRoom:
                    obj = Instantiate(NormalMapPrefabs[Random.Range(0, NormalMapPrefabs.Length)]);
                    break;
                default:
                    Debug.Log("GenerateMap : GenerateRoom().switch Error!");
                    obj = null;
                    break;
            }
            if (obj != null)
            {
                Maps[cnt] = obj.GetComponent<Map>();
                Maps[cnt].InfoSetting(mapinfo);
                Maps[cnt].setMapPos(cnt, mapDistance);
            }
            cnt++;
        }
    }

    public void DoorSetting()
    {
        for(int i = 0; i < Maps.Length - 1; i++)
        {
            Door currentDoor = Maps[i].transform.Find("GatePos/UpGate").GetComponent<Door>();
            Door nextDoor = i != Maps.Length - 1 ? Maps[i + 1].transform.Find("GatePos/DownGate").GetComponent<Door>() : null;
            Maps[i].setCurrentDoor(currentDoor, nextDoor);
        }
    }
}