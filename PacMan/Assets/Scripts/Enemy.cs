using UnityEngine;

public class Enemy : Character
{
    [SerializeField] Transform player;

    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SetTargetPosition(player.position);
        }

        base.Update();
    }

    public override void SetTargetPosition(Vector3 targetPosition)
    {
        Pathfinding.instance.moveStraight = 10;
        Pathfinding.instance.moveDiagonal = 0;

        base.SetTargetPosition(targetPosition);
    }
}
