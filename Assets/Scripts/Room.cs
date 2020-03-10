using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{    
    public GatePoint[] gatePoints;
    public GatePoint[] nearestGatePoints;
    GatePoint nearestGate = null;

    bool hasConnectedRoom = false;
    Room nearestRoom = null;

    void Start()
    {
        gatePoints = GetComponentsInChildren<GatePoint>();
    }

    void LateUpdate()
    {
        transform.name = ("" + transform.position.x + ";" + transform.position.y);
    }

    public void FindClosestRoom()
    {
        foreach(Room room in GenerateWorld.instance.spawnedRooms)
        {
            float nearestDistance = float.MaxValue;
            nearestRoom = null;

            if(Vector2.Distance(transform.position, room.transform.position) < nearestDistance)
            {
                nearestDistance = Vector2.Distance(transform.position, room.transform.position);
                nearestRoom = room;
            }
            Debug.DrawLine(transform.position, nearestRoom.transform.position, Color.red);
        }
        nearestGatePoints = nearestRoom.GetComponentsInChildren<GatePoint>();

        FindClosestGate();
    }

    public void FindClosestGate()
    {
        foreach(GatePoint gate in gatePoints)
        {
            float nearestDistance = float.MaxValue;
            nearestGate = null;

            foreach(GatePoint closeGate in nearestGatePoints)
            {
                if(Vector2.Distance(gate.transform.position, closeGate.transform.position) < nearestDistance)
                {
                    nearestDistance = Vector2.Distance(gate.transform.position, closeGate.transform.position);
                    nearestGate = closeGate;
                }
                Debug.DrawLine(gate.transform.position, closeGate.transform.position, Color.green);
            }
        }
    }

    public bool ReturnHasConnectedRoom()
    {
        return hasConnectedRoom;
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        GenerateWorld.instance.spawnedRooms.Remove(this);
        Destroy(gameObject);
        GenerateWorld.instance.IncreaseRoomsDestroyedNumber();
        //GenerateWorld.instance.SpawnRoom();
    }*/
}
