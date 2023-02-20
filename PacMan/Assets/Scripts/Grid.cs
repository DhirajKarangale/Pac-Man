using System;
using UnityEngine;

public class Grid<T>
{
    internal int width;
    internal int height;
    internal float cellSize;
    private T[,] gridArray;

    public Grid(int width, int height, float cellSize, Func<Grid<T>, int, int, T> createGridObject)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        gridArray = new T[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                gridArray[x, y] = createGridObject(this, x, y);
            }
        }
    }

    public Vector3 WorldPos(int x, int y)
    {
        return new Vector3(x, y) * cellSize;
    }

    public Vector2Int GetCoordinates(Vector3 pos)
    {
        Vector2Int coordinates = Vector2Int.zero;
        coordinates.x = Mathf.FloorToInt(pos.x / cellSize);
        coordinates.y = Mathf.FloorToInt(pos.y / cellSize);

        return coordinates;
    }

    public void SetGridObject(int x, int y, T value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = value;
        }
    }

    public void SetGridObject(Vector3 worldPosition, T value)
    {
        Vector2Int coordinates = GetCoordinates(worldPosition);
        SetGridObject(coordinates.x, coordinates.y, value);
    }

    public T GetGridObject(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        }
        else
        {
            return default(T);
        }
    }

    public T GetGridObject(Vector3 worldPosition)
    {
        Vector2Int coordinates = GetCoordinates(worldPosition);
        return GetGridObject(coordinates.x, coordinates.y);
    }
}