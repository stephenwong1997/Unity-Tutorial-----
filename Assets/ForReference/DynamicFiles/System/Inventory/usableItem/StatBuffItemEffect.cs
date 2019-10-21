using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="Item Effects/Stat Buff")]
public class StatBuffItemEffect : UsableItemEffect
{
    public int AgilityBuff;
    public float Duration;

    public override void ExecuteEffect(UsableItem parentItem, Character character)
    {
        Debug.Log("buff");
    }

    public override string GetDescription()
    {
        return "Grants " + AgilityBuff + " Agiliy for " + Duration + "seconds";
    }

}
