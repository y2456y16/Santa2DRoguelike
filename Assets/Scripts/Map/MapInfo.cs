using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum ROOM_TYPE
{
    None,
    StartRoom,
    ItemRoom,
    BossRoom,
    NormalRoom
}
[Serializable]
public class MapInfo
{
    public ROOM_TYPE eRoomType = ROOM_TYPE.None;
    
    public Vector2 mapPos;
    public Transform cameraPos;
    public List<Vector3> EnemyPosList;
    public Vector3 BossPos;

    public Tilemap tilemap;
    public int Width;
    public int Height;

    public Door currentDoor;

    public MapInfo(ROOM_TYPE _eRoomType)
    {
        eRoomType = _eRoomType;
        EnemyPosList = new List<Vector3>();
    }
}
