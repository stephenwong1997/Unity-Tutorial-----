using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyStats : MonoBehaviour
{

    [SerializeField]
    protected int health;
    [SerializeField]
    protected int maxHealth;

    [SerializeField]
    protected int baseDamage;

    [SerializeField]
    protected int knockThreshold;


    [SerializeField]
    private Armor armor = Armor.None;


    public float getEnemyHealthPercentage()
    {
        return (float)health / maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= Mathf.Max(1, (damage - (int)armor));

    }

    public int getEnemyDamage()
    {
        return baseDamage;
    }

    public float KnockThreshold()
    {
        return knockThreshold;
    }
}
