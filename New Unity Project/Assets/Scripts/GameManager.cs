using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<GameObject> enemies = new List<GameObject>();
    public static int PlayerScore = 0;
    public int Score;
    public int Lives = 3;
    public GameObject Player;

    // board creation variabls
    public int rows;
    public int cols;
    private float roomWidth = 50.0f;
    private float roomHeight = 50.0f;
    public GameObject[] gridPrefabs;
    public int mapSeed;

    private int RoomCount;
  
    public bool DailyMap = false;

    public GameObject EnemyTank;
    public GameObject PlayerTank;

    private Room[,] grid;
    public GameObject[] PlayerSpawns;
    private int RandomNumber;
    private GameObject SpawnPoint;
    public GameObject[] EnemySpawns;




    void Awake() // called when the game starts
    {
        if (instance == null)   // if there is no Game Manager, creates one
        {
            instance = this;
        }
        else
        {
            Debug.LogError("ERROR: There can only be one GameMnanager");
            Destroy(gameObject);
        }
    }

    void Start()
    {
        GenerateGrid();
        PlayerSpawns = GameObject.FindGameObjectsWithTag("PlayerSpawn");
        Spawnplayer();   
        EnemySpawns = GameObject.FindGameObjectsWithTag("EnemySpawn");
        foreach (GameObject EnemySpawn in EnemySpawns)
        {                                                             
            Instantiate(EnemyTank, EnemySpawn.transform.position, EnemySpawn.transform.rotation);
        }
    }

    void Update()
    {
        
    }


    public int DateToInt(DateTime dateToUse)
    {
        // add the date and return it
        return dateToUse.Year + dateToUse.Month + dateToUse.Day + dateToUse.Hour + dateToUse.Minute + dateToUse.Second + dateToUse.Millisecond;
    }

    public int mapOfTheDay(DateTime dateToUse)
    {
        return dateToUse.Year + dateToUse.Month + dateToUse.Day;
    }

    public void GenerateGrid()
    {
        if (DailyMap)
        {
            mapSeed = mapOfTheDay(DateTime.Now.Date);

        }
        else
        {
            mapSeed = DateToInt(DateTime.Now.Date);
        }
        grid = new Room[cols, rows];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                // figure out location
                float xPosition = roomWidth * j;
                float zPosition = roomHeight * i;
                Vector3 newPosition = new Vector3(xPosition, 0.0f, zPosition);

                // create a new grid
                GameObject tempRoomObj = Instantiate(RandomRoomPrefab(), newPosition, Quaternion.identity) as GameObject;

                // set parent
                tempRoomObj.transform.parent = this.transform;

                // give meaningful name
                tempRoomObj.name = "Room_" + j + "," + i;

                // get room object
                Room tempRoom = tempRoomObj.GetComponent<Room>();

                // open doors
                // if room is on the bottom row, open north door
                if (i == 0)
                {
                    tempRoom.doorNorth.SetActive(false);
                }
                else if (i == rows - 1)
                {
                    //otherwise if the room is in the top row, open south door
                    tempRoom.doorSouth.SetActive(false);
                }
                else
                {
                    // if room is in the middle
                    tempRoom.doorNorth.SetActive(false);
                    tempRoom.doorSouth.SetActive(false);
                }
                // if room is on the first column, open east door
                if (j == 0)
                {
                    tempRoom.doorEast.SetActive(false);
                }
                else if (j == cols - 1)
                {
                    // otherwise if room is on the last column, open west door
                    tempRoom.doorWest.SetActive(false);
                }
                else
                {
                    // otherwise the room is in the middle, open both doors
                    tempRoom.doorEast.SetActive(false);
                    tempRoom.doorWest.SetActive(false);
                }
                
                // save it to array
                grid[j, i] = tempRoom;

            }
        }
    }

    // Returns random room
    public GameObject RandomRoomPrefab()
    {
        return gridPrefabs[Random.Range(0, gridPrefabs.Length)];
    }
    public void Spawnplayer()
    {
        if (Player == null)
        {
            RandomNumber = Random.Range(0, PlayerSpawns.Length);
            SpawnPoint = PlayerSpawns[RandomNumber];
            Instantiate(PlayerTank, SpawnPoint.transform.position, SpawnPoint.transform.rotation);
            Debug.Log("tried to spawn");
            Player = GameObject.FindGameObjectWithTag("Player");
        }
    }
}

