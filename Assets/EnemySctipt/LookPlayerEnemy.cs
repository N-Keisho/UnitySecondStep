using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookPlayerEnemy : EnemyBase
{
    [SerializeField] private GameObject muzzle;
    private GameObject player;
    protected override void Start()
    {
        base.Start();
        player = GameObject.Find("Player");
        if (player == null)
        {
            Debug.LogError("Player not found!");
            return;
        }
    }

    protected override void Move()
    {
        if (player != null)
        {
            Vector3 diff = (transform.position - player.transform.position);
            this.transform.rotation = Quaternion.FromToRotation(Vector3.up, diff);
        }
    }

    protected override void Shot()
    {
        GameObject bullet = Instantiate(bulletPrefab, muzzle.transform.position, muzzle.transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Bullet Rigidbody2D not found!");
            return;
        }

        rb.AddForce(bullet.transform.up * -10f, ForceMode2D.Impulse);
        Destroy(bullet, 2f);
    }
}
