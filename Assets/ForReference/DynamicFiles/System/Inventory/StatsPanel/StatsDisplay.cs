using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StatsDisplay : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{


    public  Text nameText;
    public  Text valueText;
    [SerializeField] StatsToolTips tooltip;

    private void OnValidate()
    {
        Text[] texts = GetComponentsInChildren<Text>();
        nameText = texts[0];
        valueText = texts[1];
        if(tooltip == null)
        {
            tooltip = FindObjectOfType<StatsToolTips>();
            
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
       tooltip.ShowTooltip();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.HideTooltip();
    }
}
