using UnityEngine;

public class Player : Character
{
    public override void SetTargetPosition(Vector3 targetPosition)
    {
        Pathfinding.instance.moveStraight = 0;
        Pathfinding.instance.moveDiagonal = 14;

        base.SetTargetPosition(targetPosition);
    }
}
