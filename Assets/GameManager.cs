using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Player player;
    void Start()
    {
        player = Player.Instance;
        if (player == null)
        {
            Debug.LogError("Player instance not found!");
            return;
        }
    }

    void Update()
    {
        if(player.PlayerHealth <= 0)
        {
            Debug.Log("Game Over!");
            SceneManager.LoadScene("GameOver");
        }
        if(EnemyBase.RemainingEnemy <= 0)
        {
            Debug.Log("All enemies destroyed!");
            SceneManager.LoadScene("Clear");
        }
    }
}
