using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int rows;
    public int columns;

    public int mapSeed;

    private float roomWidth = 50.0f;
    private float roomHeight = 50.0f;

    public GameObject[] gridPrefabs;

    private Room[,] grid;

    public enum MapGenerationType { Random, MapOfTheDay, CustomSeed };
    public MapGenerationType mapType = MapGenerationType.Random;

    private void Start()
    {
        GenerateGrid();
        GameManager.Instance.SpawnEnemies(4);
    }

    public GameObject RandomRoomPrefab()
    {
        return gridPrefabs[UnityEngine.Random.Range(0, gridPrefabs.Length)];
    }

    public void GenerateGrid()
    {
        switch (mapType)
        {
            case MapGenerationType.Random:
                mapSeed = DateToInt(DateTime.Now);
                break;
            case MapGenerationType.MapOfTheDay:
                mapSeed = DateToInt(DateTime.Now.Date);
                break;
            case MapGenerationType.CustomSeed:
                // Don't change the seed.
                break;
        }
        UnityEngine.Random.InitState(mapSeed);
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
                if (column != 0)
                {
                    currentRoom.doorWest.SetActive(false);
                }
                else if (column != columns - 1)
                {
                    currentRoom.doorEast.SetActive(false);
                }
                grid[column, row] = currentRoom;
         
                temporaryRoom.transform.parent = this.transform;

                temporaryRoom.name = "Room_" + column + "," + row;
            }
        }

        // Spawn in enemies
       
        // Spawn in Player(s)
    }
    public int DateToInt(DateTime dateToUse)
    {
        // Add our date up and return it
        return dateToUse.Year + dateToUse.Month + dateToUse.Day + dateToUse.Hour + dateToUse.Minute + dateToUse.Second + dateToUse.Millisecond;
    }
}
