using System;
using UnityEngine;

public class Grid<T>
{
    internal int width;
    internal int height;
    internal float cellSize;

    private T[,] grids;
    private TextMesh[,] texts;

    public Grid(int width, int height, float cellSize, Transform parent, Func<Grid<T>, int, int, T> createGridObject)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        grids = new T[width, height];
        texts = new TextMesh[width, height];

        for (int x = 0; x < grids.GetLength(0); x++)
        {
            for (int y = 0; y < grids.GetLength(1); y++)
            {
                grids[x, y] = createGridObject(this, x, y);
            }
        }

        for (int x = 0; x < grids.GetLength(0); x++)
        {
            for (int y = 0; y < grids.GetLength(1); y++)
            {
                Vector3 pos = GetPos(x, y) + new Vector3(cellSize, cellSize) * 0.5f - new Vector3(3f, -2, 0);
                texts[x, y] = CreateText(x + "," + y, pos, parent);
                Debug.DrawLine(GetPos(x, y), GetPos(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetPos(x, y), GetPos(x + 1, y), Color.white, 100f);
            }
        }
        Debug.DrawLine(GetPos(0, height), GetPos(width, height), Color.white, 100f);
        Debug.DrawLine(GetPos(width, 0), GetPos(width, height), Color.white, 100f);
    }

    private TextMesh CreateText(string text, Vector3 pos, Transform parent)
    {
        GameObject gameObject = new GameObject("Text", typeof(TextMesh));

        Transform transform = gameObject.transform;
        transform.SetParent(parent);
        transform.localPosition = pos;
        transform.localScale = Vector3.one;

        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.fontStyle = FontStyle.Bold;
        textMesh.characterSize = 3;
        textMesh.text = text;

        return textMesh;
    }

    public Vector3 GetPos(int x, int y)
    {
        return new Vector3(x, y) * cellSize;
    }

    public Vector2Int GetCoordinates(Vector3 worldPosition)
    {
        int x = Mathf.FloorToInt(worldPosition.x / cellSize);
        int y = Mathf.FloorToInt(worldPosition.y / cellSize);

        return new Vector2Int(x, y);
    }

    public T GetGridObject(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return grids[x, y];
        }
        else
        {
            return default(T);
        }
    }

    public T GetGridObject(Vector3 pos)
    {
        Vector2Int cordinates = GetCoordinates(pos);
        return GetGridObject(cordinates.x, cordinates.y);
    }

    public void SetUnWakable(int x, int y)
    {
        texts[x, y].text = "-1";
    }
}
