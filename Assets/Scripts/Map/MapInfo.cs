using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ROOM_TYPE
{
    None,
    StartRoom,
    ItemRoom,
    BossRoom,
    NormalRoom
}

public class MapInfo
{
    ROOM_TYPE eRoomType;

    public bool IsTopDoor = false;
    public bool IsBottomDoor = false;
    public bool IsRightDoor = false;
    public bool IsLeftDoor = false;

    public Vector2 mapPos;
    public Vector2 PlayerSpawnPos;
    public Vector2 CameraPos;

    public MapInfo(ROOM_TYPE _eRoomType, Vector2 _mapPos)
    {
        mapPos = _mapPos;
        eRoomType = _eRoomType;
    }
}
