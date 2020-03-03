using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public enum  SpawnDirection
{
    up,
    down,
    left,
    right
}

public class GenerateWorld : MonoBehaviour
{
    [SerializeField] int gridSize = 10;
    [SerializeField] int roomsToSpawn;
    [SerializeField] Room[] rooms;

    string[] directions;
    List<string> directionList = new List<string>();

    Vector2 newSpawnPosition = new Vector3(0,0,0);
    void Start()
    {
        directions = Enum.GetNames(typeof(SpawnDirection));
        foreach(string direction in directions)
        {
            directionList.Add(direction);
        }

        for(int i = 0; i < roomsToSpawn; i++)
        {
            int roomToSpawnNumber;
            roomToSpawnNumber = UnityEngine.Random.Range(0, rooms.Length);
            SpawnRoom(roomToSpawnNumber);
        }
    }

    private void SpawnRoom(int roomToSpawnNumber)
    {
        Room newRoom = Instantiate(rooms[roomToSpawnNumber], newSpawnPosition, Quaternion.identity);

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

        newSpawnPosition = new Vector2(newX, newY);
        SetRoomPosition(newRoom);
    }

    private void SetRoomPosition(Room newRoom)
    {
        Vector2 roomSnapPos;
        roomSnapPos.x = Mathf.RoundToInt(newRoom.transform.position.x / gridSize) * gridSize;
        roomSnapPos.y = Mathf.RoundToInt(newRoom.transform.position.y / gridSize) * gridSize;
        newRoom.transform.position = new Vector2(roomSnapPos.x, roomSnapPos.y);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(0);
    }
}
