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
}
