using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHUDScript : MonoBehaviour
{
    public Image bar;
    public CharacterStats playerStats;

    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("CharacterStat").GetComponent<CharacterStats>();
    }
    private void Update()
    {
        if (playerStats)
        {
            updateHealth();
        }
    }

    public void updateHealth()
    {
        bar.fillAmount = playerStats.getPlayerHealthPercentage();
    }

}
