using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform NextCameraPos;

    public GameObject OpenDoor;
    public GameObject CloseDoor;
    
    public Door NextDoor;
    public Vector3 nextDoorSpawnPos = new Vector3(0, 1, 0);

    public bool IsDoorOpen;
    
    public bool setDoorOpen
    {
        get
        {
            if (!IsDoorOpen)
                return false;
            return IsDoorOpen;
        }
        set
        {
            IsDoorOpen = value;
            if (OpenDoor) OpenDoor.SetActive(value);
            if (CloseDoor) CloseDoor.SetActive(!value);
        }
    }
    public void setDoor(Door _nextdoor, Transform _nextCameraPos)
    {
        OpenDoor = transform.Find("PF Props Wooden Gate Opened").gameObject;
        CloseDoor = transform.Find("PF Props Wooden Gate").gameObject;

        NextDoor = _nextdoor;
        NextCameraPos = _nextCameraPos;
        nextDoorSpawnPos = NextDoor.transform.position + nextDoorSpawnPos;
    }

    public void OnCollisionCheck(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
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