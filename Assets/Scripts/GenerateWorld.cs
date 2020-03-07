using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateWorld : MonoBehaviour
{
    public static GenerateWorld instance;

    [SerializeField] Room[] rooms;

    [Header("Position")]
    [SerializeField] int gridSize = 8;
    [SerializeField] Vector2 worldSize = new Vector2(100,100);

    [Header("Other")]
    [SerializeField] int roomsToSpawn = 0;

    public List<Room> spawnedRooms = new List<Room>();

    bool hasSpawnedRooms = false;
    bool hasSpawnedDestroyedRooms = false;
    public bool canSpawnBridges = false;

    #region Singleton
    public int roomsDestroyed = 0;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void IncreaseRoomsDestroyedNumber()
    {
        roomsDestroyed++;
        print("Rooms destroyed: " + roomsDestroyed);
    }

    #endregion

    private void Update()
    {
        if(!hasSpawnedRooms)
        for(int i = 0; i < roomsToSpawn; i++)
        {
            SpawnRoom();
        }
        hasSpawnedRooms = true;

        if(hasSpawnedRooms && !hasSpawnedDestroyedRooms && spawnedRooms.Count < roomsToSpawn)
        {
            int additionalRooms = roomsToSpawn - spawnedRooms.Count;
            for(int j = 0; j < additionalRooms; j++)
            {
                SpawnRoom();
            }
        }

        else if(spawnedRooms.Count < roomsToSpawn)
        {
            hasSpawnedDestroyedRooms = true;
            canSpawnBridges = true;
        }

        if(canSpawnBridges)
        {
            for(int i = 0; i < spawnedRooms.Count - 1; i++)
            {
                spawnedRooms[i].SetClosestRoom(spawnedRooms[i + 1]);
            }
        }
    }

    public void SpawnRoom()
    {
        int randRoom = Random.Range(0, rooms.Length);
        float newRandPosX = Random.Range(0, worldSize.x);
        float newRandPosY = Random.Range(0, worldSize.y);

        Vector2 newRandPosFinal = new Vector2(newRandPosX, newRandPosY);

        SnapRoomPosition(Instantiate(rooms[randRoom], newRandPosFinal, Quaternion.identity));
    }

    private void SnapRoomPosition(Room newRoom)
    {
        Vector2 roomSnapPos;
        roomSnapPos.x = Mathf.RoundToInt(newRoom.transform.position.x / gridSize) * gridSize;
        roomSnapPos.y = Mathf.RoundToInt(newRoom.transform.position.y / gridSize) * gridSize;
        newRoom.transform.position = new Vector2(roomSnapPos.x, roomSnapPos.y);

        spawnedRooms.Add(newRoom);
    }
}