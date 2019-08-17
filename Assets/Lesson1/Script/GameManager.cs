using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public PlayerStats playerStats;
    public Text healthText;
    public Text gameOverText;
    public bool gameOver;
    // Start is called before the first frame update
    void Start()
    {
        playerStats.health = 100;
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (playerStats.health >= 0)
        //{
        //    healthText.text = "Health : " + playerStats.health;
        //}
        //if (playerStats.health <= 0)
        //{
        //    gameOverText.text = " GameOver ";
        //}
        if (gameOver  == false)
        {
            healthText.text = "Health : " + playerStats.health;
        }
        if (playerStats.health <= 0)
        {
            gameOver = true;
            gameOverText.text = " GameOver ";
        }
    }

    public void Damage(int damage)
    {
        playerStats.health -= damage;
    }
    public void Heal(int amount)
    {
        playerStats.health += amount;
    }
    public void Restart()
    {
        gameOver = false;
        gameOverText.text = " ";
        playerStats.health = 100;
    }

}
