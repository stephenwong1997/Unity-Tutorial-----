using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items Effect/Heal")]
public class HealItemEffect : UsableItemEffect
{
    public int HealthAmount;
    public override void ExecuteEffect(UsableItem parentItem, Character character)
    {
        //character.Health += HealthAmount;
    }

    public override string GetDescription()
    {
        return "Heals for " + HealthAmount + " health.";
    } 

}
