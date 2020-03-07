using System;
using System.Collections;
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
    public static GenerateWorld instance;

    [SerializeField] Room[] topRooms;
    [SerializeField] Room[] bottomRooms;
    [SerializeField] Room[] leftRooms;
    [SerializeField] Room[] rightRooms;

    [Header("Other variables")]
    [SerializeField] int minGridSize = 8;
    [SerializeField] int maxGridSize = 10;
    [SerializeField] int roomsToSpawn = 0;

    public List<Room> spawnedRooms = new List<Room>();

    Vector2 newSpawnPosition = new Vector3(0, 0, 0);
    string[] directions;
    List<string> directionList = new List<string>();

    bool hasSpawnedDestroyedRooms = false;
    public bool canSpawnBridges = false;

    Queue<Room> roomsToSpawnQueue = new Queue<Room>();

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
            SpawnRoom();
        }
    }

    private void LateUpdate()
    {
        if(!hasSpawnedDestroyedRooms)
        {
            Room[] generatedRooms = FindObjectsOfType<Room>();
            if(generatedRooms.Length < roomsToSpawn)
            {
                int additionalRooms = roomsToSpawn - generatedRooms.Length;
                for(int j = 0; j < additionalRooms; j++)
                {
                    SpawnRoom();
                }
                //canSpawnBridges = true;
            }
                hasSpawnedDestroyedRooms = true;
        }
            //spawnedRooms.RemoveAt(spawnedRooms.Count - 1);

        /*if(hasSpawnedDestroyedRooms && canSpawnBridges)
        {
            for(int i = 0; i < spawnedRooms.Count; i++)
            {
                spawnedRooms[i].SetClosestRoom(spawnedRooms[i + 1]);
            }
            //spawnedRooms[spawnedRooms.Count].SetClosestRoom(spawnedRooms[0]);
            canSpawnBridges = false;
        }*/
    }

    public void SpawnRoom()
    {
        int randRoom;
        float newRandPos = UnityEngine.Random.Range(minGridSize, maxGridSize);

        int newDirectionPicker = UnityEngine.Random.Range(0, directionList.Count);
        string newDirection = directionList[newDirectionPicker];


        switch(newDirection)
        {
            case "up":
                float up = newSpawnPosition.y + newRandPos;
                newSpawnPosition = new Vector2(newSpawnPosition.x, up);
                randRoom = UnityEngine.Random.Range(0, topRooms.Length);
                SnapRoomPosition(Instantiate(rightRooms[randRoom], newSpawnPosition, Quaternion.identity));
                break;

            case "down":
                float down = newSpawnPosition.y - newRandPos;
                newSpawnPosition = new Vector2(transform.position.x, down);
                randRoom = UnityEngine.Random.Range(0, bottomRooms.Length);
                SnapRoomPosition(Instantiate(rightRooms[randRoom], newSpawnPosition, Quaternion.identity));
                break;

            case "left":
                float left = newSpawnPosition.x - newRandPos;
                newSpawnPosition = new Vector2(left, transform.position.y);
                randRoom = UnityEngine.Random.Range(0, leftRooms.Length);
                SnapRoomPosition(Instantiate(rightRooms[randRoom], newSpawnPosition, Quaternion.identity));
                break;

            case "right":
                float right = newSpawnPosition.x + newRandPos;
                newSpawnPosition = new Vector2(right, transform.position.y);
                randRoom = UnityEngine.Random.Range(0, rightRooms.Length);
                SnapRoomPosition(Instantiate(rightRooms[randRoom], newSpawnPosition, Quaternion.identity));
                break;
        }
    }

    private void SnapRoomPosition(Room newRoom)
    {
        Vector2 roomSnapPos;
        roomSnapPos.x = Mathf.RoundToInt(newRoom.transform.position.x / minGridSize) * minGridSize;
        roomSnapPos.y = Mathf.RoundToInt(newRoom.transform.position.y / minGridSize) * minGridSize;
        newRoom.transform.position = new Vector2(roomSnapPos.x, roomSnapPos.y);

        spawnedRooms.Add(newRoom);
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
