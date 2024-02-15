using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{
    [SerializeField] private MapInfo mapInfo;
    bool isInPlayer = false;
    public void InfoSetting(MapInfo _mapInfo)
    {
        mapInfo = _mapInfo;
    }

    public void setMapPos(int cnt, float mapDistance)
    {
        setSizeValue();
        transform.position = new Vector2(0, (mapInfo.Height + mapDistance) * cnt);
        mapInfo.playerSpawnPos = transform.position;
        mapInfo.cameraPos = transform.Find("CameraPos").transform;
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
    }

    public void setPlayerPos(GameObject _player)
    {
        _player.transform.position = mapInfo.playerSpawnPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isInPlayer = true;
        }
    }
}
