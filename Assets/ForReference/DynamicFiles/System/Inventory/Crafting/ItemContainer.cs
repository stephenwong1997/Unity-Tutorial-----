using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemContainer : MonoBehaviour, IItemContainer
{
    public ItemSlot[] itemSlots;

	public virtual bool AddItem(Item item)
	{
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].CanAddStack(item))
            {
                itemSlots[i].Item = item;
                itemSlots[i].Amount++;
                return true;

            }
        }
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].Item == null)
            {
                itemSlots[i].Item = item;
                itemSlots[i].Amount++;
                return true;

            }
        }
        return false;
    }

	public virtual bool RemoveItem(Item item)
	{
		for (int i = 0; i < itemSlots.Length; i++)
		{
			if (itemSlots[i].Item == item)
			{
				itemSlots[i].Amount--;
  
                return true;
            }
		}
		return false;
	}

	public virtual Item RemoveItem(string itemID)
	{
		for (int i = 0; i < itemSlots.Length; i++)
		{
			Item item = itemSlots[i].Item;
			if (item != null && item.ID == itemID)
			{
				itemSlots[i].Amount--;

				return item;
			}
		}
		return null;
	}

    public virtual bool IsFull()
    {
        for(int i = 0; i < itemSlots.Length;i++)
        {
            if (itemSlots[i].Item == null)
            {
                return false;
            }
        }
        return false;
    }

	public virtual int ItemCount(string itemID)
	{
		int number = 0;

		for (int i = 0; i < itemSlots.Length; i++)
		{
			Item item = itemSlots[i].Item;
			if (item != null && item.ID == itemID)
			{
				number += itemSlots [i].Amount;
			}
		}
		return number;
	}

    public void Clear()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].Item != null && Application.isPlaying)
            {
                //itemSlots[i].Item.Destroy();
            }
            itemSlots[i].Item = null;
            itemSlots[i].Amount = 0;
        }
    }

}
