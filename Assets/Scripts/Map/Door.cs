using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    //GameObject NextMap;
    [SerializeField] private Transform NextCameraPos;
    public Door NextDoor;
    public Vector3 nextDoorSpawnPos = new Vector3(0, 1, 0);

    public void setDoor(Door _nextdoor, Transform _nextCameraPos)
    {
        NextDoor = _nextdoor;
        NextCameraPos = _nextCameraPos;
        nextDoorSpawnPos = NextDoor.transform.position + nextDoorSpawnPos;
    }

    public void OnCollisionCheck(Collider2D collision)
    {
        if(collision.tag.Equals("Player"))
        {
            if (NextDoor)
            {
                collision.gameObject.transform.position = nextDoorSpawnPos;
                Camera.main.transform.position = NextCameraPos.position;
            }
            else
                Debug.Log("Door : NextDoor is Null!");
        }
    }
}