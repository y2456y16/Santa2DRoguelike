using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{
    [SerializeField] private MapInfo mapInfo;
    public int EnemyCount;
    bool isInPlayer = false;
    IEnumerator SpawnCourt = null;

    public void InfoSetting(MapInfo _mapInfo)
    {
        mapInfo = _mapInfo;
    }

    public void setMapPos(int cnt, float mapDistance)
    {
        setSizeValue();
        transform.position = new Vector2(0, (mapInfo.Height + mapDistance) * cnt);
        mapInfo.cameraPos = transform.Find("CameraPos").transform;
        Transform posList = transform.Find("MonsterPosList");
        if (posList)
        {
            for (int i = 0; i < posList.childCount; i++)
            {
                if (posList.GetChild(i).name == "BossPos")
                    mapInfo.BossPos = posList.GetChild(i).position;
                else
                    mapInfo.EnemyPosList.Add(posList.GetChild(i).position);
            }
        }
    }

    public void setSizeValue()
    {
        if (!mapInfo.tilemap)
        {
            mapInfo.tilemap = transform.Find("Grid/Stage").GetComponent<Tilemap>();
        }

        mapInfo.Width = mapInfo.tilemap.cellBounds.size.x;
        mapInfo.Height = mapInfo.tilemap.cellBounds.size.y;
    }

    public void setCurrentDoor(Door _door, Door _nextDoor)
    {
        mapInfo.currentDoor = _door;

        if (mapInfo.eRoomType != ROOM_TYPE.BossRoom)
        {
            Transform nextCam = _nextDoor.transform.parent.parent.GetComponent<Map>().mapInfo.cameraPos;
            mapInfo.currentDoor.setDoor(_nextDoor, nextCam);
        }
        if (EnemyCount > 0)
        {
            mapInfo.currentDoor.OpenDoor.SetActive(false);
            mapInfo.currentDoor.CloseDoor.SetActive(true);
        }
        else
        {
            mapInfo.currentDoor.OpenDoor.SetActive(true);
            mapInfo.currentDoor.CloseDoor.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && isInPlayer == false)
        {
            isInPlayer = true;
            GameManager.Instance.currentMap = this;
            GameManager.Instance.enemyLocation.Clear();
            foreach (Vector3 pos in mapInfo.EnemyPosList)
            {
                GameManager.Instance.enemyLocation.Add(pos);
            }
            if (SpawnCourt == null)
            {
                if (mapInfo.eRoomType == ROOM_TYPE.BossRoom)
                {
                    SpawnCourt = BossSpawn();
                }
                else
                {
                    SpawnCourt = EnemySpawn();
                }
                StartCoroutine(SpawnCourt);
            }
        }
    }
    public void EnemyDeathCount()
    {
        if(--EnemyCount <= 0)
        {
            mapInfo.currentDoor.setDoorOpen = true;
        }
    }
    IEnumerator EnemySpawn()
    {
        int cnt = 0;
        while(cnt < EnemyCount)
        {
            GameManager.Instance.EnemyCreate();
            yield return null;
            cnt++;
        }
        SpawnCourt = null;
    }
    IEnumerator BossSpawn()
    {
        GameManager.Instance.BossCreate(mapInfo.BossPos);
        yield return null;
        SpawnCourt = null;
    }
}
