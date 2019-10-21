﻿
using UnityEngine;


using UnityEngine.UI;
public class ItemToolTips : MonoBehaviour
{
    [SerializeField] Text ItemNameText;
    [SerializeField] Text ItemTypeText;
    [SerializeField] Text ItemDescriptionText;

    public void ShowTooltip(Item item)
    {
        ItemNameText.text = item.ItemName;
        ItemTypeText.text = item.GetItemType() ;

       

        ItemDescriptionText.text = item.GetDescription();

        gameObject.SetActive(true);
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    


}
