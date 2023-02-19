using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] SpriteRenderer cellPrefab;
    [SerializeField] Enemy enemy;

    private Camera cam;
    private Pathfinding pathfinding;
    private SpriteRenderer[,] cells;

    private void Start()
    {
        cam = Camera.main;
        cells = new SpriteRenderer[20, 10];
        pathfinding = new Pathfinding(20, 10);
        CreateGrid(pathfinding.GetGrid());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            enemy.SetTargetPosition(MousePos());
        }

        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mouseWorldPosition = MousePos();
            pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
            pathfinding.GetNode(x, y).SetIsWalkable(!pathfinding.GetNode(x, y).isWalkable);
            cells[x, y].color = Color.black;
        }
    }

    private Vector3 MousePos()
    {
        return cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void CreateGrid(Grid<PathNode> grid)
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
