using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int rows;
    public int cols;
    private float roomWidth = 50.0f;
    private float roomHeight = 50.0f;
    public GameObject[] gridPrefabs;

    private Room[,] grid;



    public void GenerateGrid()
    {
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
}
