using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YMoveEnemy : EnemyBase
{
    protected override void Move()
    {
        // Move the enemy downwards
        transform.position += Vector3.down * speed * Time.deltaTime;

        // Check if the enemy is out of bounds and destroy it
        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }
}
