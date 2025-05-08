using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Step1
    [SerializeField] private float speed = 5f;    
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private int playerHealth = 3; // Player's health

    void Start()
    {
        if (bulletPrefab == null)
        {
            Debug.LogError("Bullet prefab not assigned!");
        }
    }

    void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            SoundManager.Instance.PlayShotSound();
            Shot();
        }
    }

    void Move()
    {
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
    }

    void Shot()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Bullet Rigidbody2D not found!");
            return;
        }
        rb.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
        Destroy(bullet, 2f);
    }


    // Step4 ---
    public static Player Instance { get; private set; }
    public int PlayerHealth {get { return playerHealth; } }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            playerHealth--;
            Destroy(other.gameObject); // Destroy the enemy bullet
            Debug.Log("Player hit! Health: " + playerHealth);
        }
    }
}
