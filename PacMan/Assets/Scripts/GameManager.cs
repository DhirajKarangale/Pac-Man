using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Color normalColor;
    [SerializeField] SpriteRenderer cellPrefab;
    [SerializeField] Player player;

    private Camera cam;
    private Pathfinding pathfinding;
    private SpriteRenderer[,] cells;

    private void Start()
    {
        cam = Camera.main;
        cells = new SpriteRenderer[20, 10];
        pathfinding = new Pathfinding(20, 10);
        SpwanGrid(pathfinding.GetGrid());
        // AutoBlockGrid(pathfinding.GetGrid());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            player.SetTargetPosition(MousePos());
        }

        if (Input.GetMouseButtonDown(1))
        {
            Vector2Int coordinates = pathfinding.GetGrid().GetCoordinates(MousePos());
            BlockArea(coordinates.x, coordinates.y);
        }
    }

    private void AutoBlockGrid(Grid<PathNode> grid)
    {
        int x = 2;
        int y = 0;

        for (y = 0; y <= 7; y++)
        {
            BlockArea(x, y);
        }

        x = 4;
        for (y = 9; y >= 1; y--)
        {
            BlockArea(x, y);
        }

        y = 2;
        for (x = 6; x <= 12; x++)
        {
            BlockArea(x, y);
        }

        y = 7;
        for (x = 6; x <= 12; x++)
        {
            BlockArea(x, y);
        }

        x = 6;
        for (y = 6; y >= 3; y--)
        {
            if (y == 4) continue;
            BlockArea(x, y);
        }

        x = 12;
        for (y = 6; y >= 3; y--)
        {
            if (y == 4) continue;
            BlockArea(x, y);
        }

        y = 3;
        for (x = 14; x <= 17; x++)
        {
            BlockArea(x, y);
        }

        x = 18;
        for (y = 1; y <= 9; y++)
        {
            BlockArea(x, y);
        }
    }

    private void BlockArea(int x, int y)
    {
        pathfinding.GetNode(x, y).SetIsWalkable(!pathfinding.GetNode(x, y).isWalkable);
        if (!pathfinding.GetNode(x, y).isWalkable)
            cells[x, y].color = Color.black;
        else
            cells[x, y].color = normalColor;
    }

    private Vector3 MousePos()
    {
        return cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void SpwanGrid(Grid<PathNode> grid)
    {
        for (int x = 0; x < grid.width; x++)
        {
            for (int y = 0; y < grid.height; y++)
            {
                Vector3 gridPosition = new Vector3(x, y) * grid.cellSize + Vector3.one * grid.cellSize * .5f;
                SpriteRenderer t = Instantiate(cellPrefab, gridPosition, Quaternion.identity);
                t.transform.SetParent(this.transform);
                cells[x, y] = t;
            }
        }
    }
}
