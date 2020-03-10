using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateWorld : MonoBehaviour
{
    [SerializeField] Room[] rooms;

    [Header("Position")]
    [SerializeField] int gridSize = 8;
    public Vector2 worldSize = new Vector2(100,100);

    [Header("Other")]
    public List<Room> spawnedRooms = new List<Room>();
    public bool canSpawnBridges = false;

    [SerializeField] int corridorsToGenerate = 5;

    #region Singleton
    public static GenerateWorld instance;
    [HideInInspector] public int roomsDestroyed = 0;

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

    private void Start()
    {
        for(int i = 0; i < corridorsToGenerate; i++)
        {
            Corridor corridor = GetComponent<Corridor>();
            corridor.GenerateCorridor();

            SpawnRoom(corridor.squareList[0].transform.position);
            SpawnRoom(corridor.squareList[corridor.squareList.Count - 1].transform.position);
        }          
    }

    /*private void Update()
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

        for(int i = 0; i < spawnedRooms.Count - 1; i++)
        {
            //spawnedRooms[i].SetClosestRoom(spawnedRooms[i + 1]);
            spawnedRooms[i].FindClosestRoom();
        }
        //spawnedRooms[spawnedRooms.Count - 1].SetClosestRoom(spawnedRooms[0]);
    }*/

    public void SpawnRoom(Vector2 spawnPosition)
    {
        int randRoom = Random.Range(0, rooms.Length);

        Room newRoom = Instantiate(rooms[randRoom], spawnPosition, Quaternion.identity);
        print(newRoom.transform.position);

        SnapRoomPosition(newRoom);
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