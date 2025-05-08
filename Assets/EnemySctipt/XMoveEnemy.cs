using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XMoveEnemy : EnemyBase
{
    [SerializeField] private float range = 5f;
    private Vector3 startPosition;
    protected override void Start()
    {
        base.Start();
        startPosition = transform.position;
    }

    protected override void Move()
    {
        // Move the enemy downwards
        transform.position = startPosition + new Vector3(Mathf.Sin(Time.time * speed) * range, 0, 0);
    }
}
