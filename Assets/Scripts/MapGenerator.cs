using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int rows;
    public int columns;

    private float roomWidth = 50.0f;
    private float roomHeight = 50.0f;

    public GameObject[] gridPrefabs;

    private Room[,] grid;

    private void Start()
    {
        GenerateGrid();
    }

    public GameObject RandomRoomPrefab()
    {
        return gridPrefabs[Random.Range(0, gridPrefabs.Length)];
    }

    public void GenerateGrid()
    {
        // Clear out the grid - "Which column" is our X, "Which row" is our Y
        grid = new Room[columns, rows];

        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                float xPosition = column * roomWidth;
                float zPosition = row * roomHeight;

                Vector3 newRoomPosition = new Vector3(xPosition, 0f, zPosition);

                GameObject temporaryRoom = Instantiate(RandomRoomPrefab(), newRoomPosition, Quaternion.identity);

                Room currentRoom = temporaryRoom.GetComponent<Room>();
                if (row != rows - 1)
                {
                    currentRoom.doorNorth.SetActive(false);
                }
                if (row != 0)
                {
                    currentRoom.doorSouth.SetActive(false);
                }
                // If we are on the first column, open the east door
                if (column == 0)
                {
                    currentRoom.doorEast.SetActive(false);
                }
                else if (column == columns - 1)
                {
                    currentRoom.doorWest.SetActive(false);
                }
                else
                {
                    currentRoom.doorEast.SetActive(false);
                    currentRoom.doorWest.SetActive(false);
                }
                grid[column, row] = currentRoom;
         
                temporaryRoom.transform.parent = this.transform;

                temporaryRoom.name = "Room_" + column + "," + row;
            }
        }
    }
}
