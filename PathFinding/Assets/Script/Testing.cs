using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    private Camera cam;
    private Pathfinding pathfinding;

    private void Start()
    {
        cam = Camera.main;
        pathfinding = new Pathfinding(5, 5, 10, transform);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2Int cordinates = pathfinding.GetGrid().GetCoordinates(MousePos());
            int x = cordinates.x;
            int y = cordinates.y;

            List<PathNode> path = pathfinding.FindPath(0, 0, x, y);

            if (path != null)
            {
                for (int i = 0; i < path.Count - 1; i++)
                {
                    Vector3 startPos = new Vector3(path[i].x, path[i].y) * 10f + Vector3.one * 5f;
                    Vector3 endPos = new Vector3(path[i + 1].x, path[i + 1].y) * 10f + Vector3.one * 5f;
                    Debug.DrawLine(startPos, endPos, Color.green, 2f);
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Vector2Int cordinates = pathfinding.GetGrid().GetCoordinates(MousePos());
            int x = cordinates.x;
            int y = cordinates.y;

            pathfinding.GetNode(x, y).SetWalkable(!pathfinding.GetNode(x, y).isWalkable);
            pathfinding.SetUnWakable(x, y);
        }
    }

    private Vector3 MousePos()
    {
        return cam.ScreenToWorldPoint(Input.mousePosition);
    }
}
