using UnityEngine;
using System.Collections.Generic;

public class Character : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float speed;

    private int pathIndex;
    private List<Vector3> paths;

    private void Start()
    {
        pathIndex = 0;
        paths = new List<Vector3>();
        paths = null;
    }

    protected virtual void Update()
    {
        HandleMovement();
    }


    private void HandleMovement()
    {
        if (paths != null)
        {
            Vector3 targetPosition = paths[pathIndex];
            if (Vector3.Distance(transform.position, targetPosition) > 1f)
            {
                Vector3 moveDir = (targetPosition - transform.position).normalized;
                float dist = Vector3.Distance(transform.position, targetPosition);
                animator.Play("Run");
                transform.position = transform.position + moveDir * speed * Time.deltaTime;
            }
            else
            {
                pathIndex++;
                if (pathIndex >= paths.Count)
                {
                    paths = null;
                    animator.Play("Idle");
                }
            }
        }
        else
        {
            animator.Play("Idle");
        }
    }

    public virtual void SetTargetPosition(Vector3 targetPosition)
    {
        pathIndex = 0;
        paths = Pathfinding.instance.FindPath(transform.position, targetPosition);

        if (paths != null && paths.Count > 1)
        {
            paths.RemoveAt(0);
        }
    }
}
