using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsPanel : MonoBehaviour
{
    [SerializeField] StatsDisplay[] statsDisplays;
    [SerializeField] string[] statNames;

    private CharacterStats stats;


    private void OnValidate()
    {
        statsDisplays = GetComponentsInChildren<StatsDisplay>();
        UpdateStatNames();
    }

    public void SetStats( CharacterStats charStats)
    {
        stats = charStats;
    }

    public void UpdateStatValues()
    {
        for (int i = 0; i < 3; i++)
        {
            statsDisplays[i].valueText.text = stats.getStats()[i].ToString();

        }
    }

    public void UpdateStatNames()
    {
        if (statNames.Length > 3)
        {
            Debug.LogError("StatsName is not size of 3, you only have 3 stats in CharacterStats");
            return;
        }

        for (int i = 0; i < 3; i++)
        {
            statsDisplays[i].nameText.text = statNames[i];

        }
    }

}
