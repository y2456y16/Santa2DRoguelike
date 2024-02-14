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

public class MapInfo : MonoBehaviour
{
    public ROOM_TYPE eRoomType;
    public Tilemap tilemap;

    public bool IsTopDoor = false;
    public bool IsBottomDoor = false;
    public bool IsRightDoor = false;
    public bool IsLeftDoor = false;

    public Vector2 mapPos;
    public Vector2 playerSpawnPos;
    public Vector2 cameraPos;

    public int Width;
    public int Height;

    public MapInfo(ROOM_TYPE _eRoomType)
    {
        eRoomType = _eRoomType;
    }
    private void Start()
    {
        Width = tilemap.cellBounds.size.x;
        Height = tilemap.cellBounds.size.y;
    }
}
