using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.UIElements;

public class OppGridManager : MonoBehaviour
{
    public int width = 3;
    public int height = 2;
    public float cellPadding = 0.5f; // Padding between cells

    public GameObject gridCellPrefab;
    public List<GameObject> gridObjects = new List<GameObject>();
    public GameObject[,] gridCells;

    void Start()
    {
        Transform gridTransform = GetComponent<Transform>();
        gridTransform.localScale = new Vector3(0.01f, 0.01f, 1);
        CreateGrid();
    }

    void CreateGrid()
    {
        gridCells = new GameObject[width, height];

        Vector2 centerOffset = new Vector2(
            (width - 1) * (1.0f + cellPadding) / 2.0f,
            (height - 1) * (1.4f + cellPadding) / 2.0f
        );

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if ((y == 0 && x == 0) || (y == 0 && x == 2))
                {
                    continue;
                }
                Vector2 position = new Vector2(x, y);

                Vector2 spawnPosition = new Vector2(
                    x * (1.0f + cellPadding) - 1.65f,
                    y * (1.4f + cellPadding) + 2.75f
                ) - centerOffset;

                GameObject cell = GameObject.Instantiate(gridCellPrefab, spawnPosition, Quaternion.identity);
                cell.transform.SetParent(transform);

                cell.GetComponent<GridCell>().gridIndex = position;
                gridCells[x, y] = cell;
            }
        }
    }

    public bool AddObjectToGrid(GameObject obj, Vector2 gridPosition, Vector3 scale)
    {
        if (gridPosition.x >= 0 && gridPosition.x < width && gridPosition.y >= 0 && gridPosition.y < height)
        {
            GridCell cell = gridCells[(int)gridPosition.x, (int)gridPosition.y].GetComponent<GridCell>();

            if (cell.isOccupied) return false;
            else
            {
                GameObject newObject = GameObject.Instantiate(obj, cell.GetComponent<Transform>().position, Quaternion.identity);
                newObject.transform.SetParent(transform);
                newObject.transform.localScale = scale;
                gridObjects.Add(newObject);
                cell.occupant = newObject;
                cell.isOccupied = true;

                CardMovement cardMovement = newObject.GetComponent<CardMovement>();
                if (cardMovement != null)
                {
                    cardMovement.isInPlay = true;
                    cardMovement.currentState = 3;
                }

                return true;
            }
        }
        else return false;
    }

}

