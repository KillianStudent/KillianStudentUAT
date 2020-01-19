using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<GameObject> enemies = new List<GameObject>();
    public int Player1Score = 0;
    public int Player2Score = 0;
    public int Lives = 3;
    public GameObject[] Players;

    // board creation variabls
    public int rows;
    public int cols;
    private float roomWidth = 50.0f;
    private float roomHeight = 50.0f;
    public GameObject[] gridPrefabs;
    public int mapSeed;

    public GameObject MenuMusicManager;

    private int RoomCount;
  
    public bool DailyMap = false;

    // prefabs for tanks
    public GameObject EnemyTank;
    public GameObject PlayerTank;
    public GameObject MultiplayerTank1;
    public GameObject MultiplayerTank2;

    // grid list
    private Room[,] grid;

    public GameObject[] PlayerSpawns;
    private int RandomNumber;
    private GameObject SpawnPoint;
    public GameObject[] EnemySpawns;

    // bool for multiplayer
    public bool Multiplayer = false;

    public int Player1Lives = 3;
    public int Player2Lives = 3;


    public void StartGame() // starts single player game
    {
        Multiplayer = false;
        SceneManager.LoadScene("MainScene");
        BeginGame();
        Destroy(GameObject.Find("Canvas"));
    }
    public void StartMultiplayerGame()  // starts multiplayer game
    {
        Multiplayer = true;
        SceneManager.LoadScene("MainScene");
        BeginGame();
        Destroy(GameObject.Find("Canvas"));
    }

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
        DontDestroyOnLoad(this.gameObject); // ensures that the GameManager does not delete upon starting a new scene
    }
    // function to destroy rooms when loading game over scene
    public void DestroyRooms()
    {
        GameObject[] Rooms = GameObject.FindGameObjectsWithTag("Room");
        for (int i = 0; i < Rooms.Length; i++)
        {
            Destroy(Rooms[i]);
        }
    }

    // function called when the game starts
    void BeginGame()
    {
        MenuMusicManager.GetComponent<AudioSource>().Stop();
        this.GetComponent<AudioSource>().Play();
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
        Player1Score = Players[0].GetComponent<TankData>().Score;
        Player2Score = Players[1].GetComponent<TankData>().Score;

        if (Players.Length == 0)
        {
            SceneManager.LoadScene("GameOver");
            DestroyRooms();
        }
    }

    // used for RNG seed
    public int DateToInt(DateTime dateToUse)
    {
        // add the date and return it
        return dateToUse.Year + dateToUse.Month + dateToUse.Day + dateToUse.Hour + dateToUse.Minute + dateToUse.Second + dateToUse.Millisecond;
    }
    // used for RNG seed for daily map
    public int mapOfTheDay(DateTime dateToUse)
    {
        return dateToUse.Year + dateToUse.Month + dateToUse.Day;
    }
    // function to generate the map
    public void GenerateGrid()
    {
        if (DailyMap)   // sets seed to daily map if daily map bool is true
        {
            mapSeed = mapOfTheDay(DateTime.Now.Date);
            Random.InitState(mapSeed);
        }
        else
        {
            mapSeed = DateToInt(DateTime.Now.Date);
            Random.InitState(mapSeed);
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
        if (!Multiplayer)
        {
            SpawnTank(PlayerTank, ref Player1Lives);
        }
        else
        {
            SpawnTank(MultiplayerTank1, ref Player1Lives);
            SpawnTank(MultiplayerTank2, ref Player2Lives);
        }
    }
    
    // funciton to get a random number
    public void FindRandomNumber()
    {
        RandomNumber = Random.Range(0, PlayerSpawns.Length);    // grabs a random number between 0 and spawn points
        SpawnPoint = PlayerSpawns[RandomNumber]; // sets the spawn point to be the random number
    }
    // used to get a random spawn point when spawning and respawning tanks
    public Transform FindSpawnPoint()
    {
        FindRandomNumber();
        return SpawnPoint.transform;
    }

    public void SpawnTank(GameObject TankToSpawn, ref int LiveCounter)
    {
        FindRandomNumber();
        Instantiate(TankToSpawn, SpawnPoint.transform.position, SpawnPoint.transform.rotation);  // spawns player tank with full camera
        LiveCounter--;
        Players = GameObject.FindGameObjectsWithTag("Player");
    }
}