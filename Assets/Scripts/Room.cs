using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{    
    [SerializeField] GatePoint[] gatePoint;
    [SerializeField] Room closestRoom = null;
    bool hasConnectedRoom = false;

    void Start()
    {
        gatePoint = GetComponentsInChildren<GatePoint>();
    }

    void LateUpdate()
    {
        transform.name = ("" + transform.position.x + ";" + transform.position.y);
    }

    public void SetClosestRoom(Room room)
    {
        closestRoom = room;
    }

    public bool ReturnHasConnectedRoom()
    {
        return hasConnectedRoom;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GenerateWorld.instance.spawnedRooms.Remove(this);
        Destroy(gameObject);
        GenerateWorld.instance.IncreaseRoomsDestroyedNumber();
        //GenerateWorld.instance.SpawnRoom();
    }
}
