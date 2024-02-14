using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    //GameObject NextMap;
    [SerializeField] private Transform NextCameraPos;
    [SerializeField] private Door NextDoor;
    public Vector3 PlayerSpawnPos = new Vector3(0, 1, 0);

    private void Start()
    {
        setDoor();
    }

    public void setDoor()
    {
        if(NextDoor) PlayerSpawnPos = NextDoor.transform.position + PlayerSpawnPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Player"))
        {
            if (NextDoor)
            {
                collision.gameObject.transform.position = PlayerSpawnPos;
                Camera.main.transform.position = NextCameraPos.position;
            }
            else
                Debug.Log("Door : NextDoor is Null!");
        }
    }
}
