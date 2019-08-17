using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public PlayerStats playerStats;
    public Text healthText;
    public Text gameOverText;
    // Start is called before the first frame update
    void Start()
    {
        playerStats.health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerStats.health <= 0)
        {
            gameOverText.text = " GameOver ";
        }
        else
        {
            healthText.text = "Health : " + playerStats.health;
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
        playerStats.health = 100;
    }

}
