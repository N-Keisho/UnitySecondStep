using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public abstract class EnemyBase : MonoBehaviour
{
    protected static int enemyCount = 0;
    protected static int destroyedEnemyCount = 0;
    public static int RemainingEnemy { get { return enemyCount - destroyedEnemyCount; } }

    [SerializeField] protected float speed = 5f;
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected float fireRate = 1f; // Time in seconds between shots
    private float nextFireTime = 0f;

    protected virtual void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D not found!");
            return;
        }
        rb.gravityScale = 0; // Disable gravity for the enemy

        enemyCount++;
    }

    private void Update()
    {
        Move();
        if (Time.time >= nextFireTime)
        {
            Shot();
            nextFireTime = Time.time + fireRate;
        }
    }

    protected abstract void Move();

    protected virtual void Shot()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position + new Vector3(0, -1f, 0), Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Bullet Rigidbody2D not found!");
            return;
        }
        rb.AddForce(Vector2.down * 10f, ForceMode2D.Impulse);
        Destroy(bullet, 2f);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnHitPlayer(other);
        }
        else if (other.CompareTag("Bullet"))
        {
            
            OnHitBullet(other);
        }
    }

    protected virtual void OnHitPlayer(Collider2D player)
    {
        Debug.Log("Enemy hit the player!");
    }

    protected virtual void OnHitBullet(Collider2D bullet)
    {
        Destroy(bullet.gameObject);
        destroyedEnemyCount++;
        Debug.Log("Enemy hit by bullet! Remaining Enemy: " + (enemyCount - destroyedEnemyCount));
        SoundManager.Instance.PlayExplosionSound();
        Destroy(gameObject);
    }
}
