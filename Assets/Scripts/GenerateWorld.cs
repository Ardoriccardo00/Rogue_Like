using System;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnDirection
{
    up,
    down,
    left,
    right
}

public class GenerateWorld : MonoBehaviour
{
    [SerializeField] Room[] rooms;

    [SerializeField] Room[] topRooms;
    [SerializeField] Room[] bottomRooms;
    [SerializeField] Room[] leftRooms;
    [SerializeField] Room[] rightRooms;

    [Header("Other variables")]
    [SerializeField] int gridSize = 8;
    [SerializeField] int roomsToSpawn = 0;
    Vector2 newSpawnPosition = new Vector3(0, 0, 0);

    List<Vector2> takenDirectionsList = new List<Vector2>();
    string[] directions;
    List<string> directionList = new List<string>();


    void Start()
    {
        #region enums
        //Turns the enum into a list of strings
        directions = Enum.GetNames(typeof(SpawnDirection));
        foreach(string direction in directions)
        {
            directionList.Add(direction);
        }
        #endregion
        //Loop to generate every room
        for(int i = 0; i < roomsToSpawn; i++)
        {
            int roomToSpawnNumber;
            roomToSpawnNumber = UnityEngine.Random.Range(0, rooms.Length);
            SpawnRoom(roomToSpawnNumber);
        }
    }


    private void SpawnRoom(int roomSpawnNumber)
    {
        Room newRoom = Instantiate(rooms[roomSpawnNumber], transform.position, Quaternion.identity);

        Vector2 newRoomPosition;
        newRoomPosition = GenerateCoordinates();

        newRoom.transform.position = newRoomPosition; //set the room position
        newSpawnPosition = newRoomPosition; //assign the new spawn position value
        takenDirectionsList.Add(newSpawnPosition); //Adds the position of the room into a list of Vector2
    }

    private Vector2 GenerateCoordinates()
    {
        int newDirectionPicker = UnityEngine.Random.Range(0, directionList.Count);
        string newDirection = directionList[newDirectionPicker];

        float newX = newSpawnPosition.x;
        float newY = newSpawnPosition.y;

        switch(newDirection)
        {
            case "up":
                newY += gridSize;
                break;
            case "down":
                newY -= gridSize;
                break;
            case "left":
                newX -= gridSize;
                break;
            case "right":
                newX += gridSize;
                break;
        }

        Vector2 generatedSpawnPosition = new Vector2(newX, newY);
        return generatedSpawnPosition;
    }
}

/*[Header("Room Templates")]
    [SerializeField] Room[] topRooms;
    [SerializeField] Room[] bottomRooms;
    [SerializeField] Room[] leftRooms;
    [SerializeField] Room[] rightRooms;

    int openingDirection;
    int rand;
    bool spawned = false;

    void Start()
    {
        Invoke("SpawnRooms", 0.1f);
    }

    private void Update()
    {
        
    }

    void SpawnRooms()
    {
        if(!spawned)
        {
            switch(openingDirection)
            {
                case 1:
                    rand = UnityEngine.Random.Range(0, topRooms.Length);
                    Instantiate(topRooms[rand], transform.position, Quaternion.identity);
                    break;
                case 2:
                    rand = UnityEngine.Random.Range(0, bottomRooms.Length);
                    Instantiate(bottomRooms[rand], transform.position, Quaternion.identity);
                    break;
                case 3:
                    rand = UnityEngine.Random.Range(0, leftRooms.Length);
                    Instantiate(leftRooms[rand], transform.position, Quaternion.identity);
                    break;
                case 4:
                    rand = UnityEngine.Random.Range(0, rightRooms.Length);
                    Instantiate(rightRooms[rand], transform.position, Quaternion.identity);
                    break;
            }
        }
        spawned = true;
    }
}*/
