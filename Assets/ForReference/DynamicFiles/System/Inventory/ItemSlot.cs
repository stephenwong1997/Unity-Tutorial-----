﻿using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : BaseItemSlot, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{


    public event Action<BaseItemSlot> OnBeginDragEvent;
    public event Action<BaseItemSlot> OnEndDragEvent;
    public event Action<BaseItemSlot> OnDragEvent;
    public event Action<BaseItemSlot> OnDropEvent;

    //private Color normalColor = Color.white;
    //private Color disabledColor = new Color(0, 0, 0, 0);
    private Color dragColor = new Color(1, 1, 1, 0.5f);

    public override bool CanAddStack(Item item, int amount = 1)
    {
        return base.CanAddStack(item, amount) && Amount + amount <= item.MaxiumStacks;
    }

    public override bool CanReceiveItem(Item item)
    {
        return true;
    }

 
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Item != null)
            image.color = dragColor;
        if (OnBeginDragEvent != null)
            OnBeginDragEvent(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Item != null)
            image.color = normalColor;

        if (OnEndDragEvent != null)
            OnEndDragEvent(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (OnDragEvent != null)
            OnDragEvent(this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (OnDropEvent != null)
            OnDropEvent(this);
    }

}