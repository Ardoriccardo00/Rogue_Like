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

    void Update()
    {
        transform.name = ("" + transform.position.x + ";" + transform.position.y);

        /*if(GenerateWorld.instance.canSpawnBridges && !hasConnectedRoom)
        {
            FindClosestRoom();
        }*/
    }

    public void FindClosestRoom()
    {
        float distanceToClosestRoom = Mathf.Infinity;
        Room[] allRooms = GameObject.FindObjectsOfType<Room>();

        foreach(Room currentRoom in allRooms)
        {
            float distanceToRoom = (currentRoom.transform.position - this.transform.position).sqrMagnitude;
            if(distanceToRoom < distanceToClosestRoom)
            {
                distanceToClosestRoom = distanceToRoom;
                closestRoom = currentRoom;
            }
        }

        Debug.DrawLine(this.transform.position, closestRoom.transform.position);
        hasConnectedRoom = true;
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
