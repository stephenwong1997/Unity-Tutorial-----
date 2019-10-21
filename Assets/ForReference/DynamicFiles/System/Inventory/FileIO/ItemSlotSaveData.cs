using System;

[Serializable]
public class ItemSlotSaveData 
{
    public string itemID;
    public int Amount;

    public ItemSlotSaveData(string id, int amount)
    {
        itemID = id;
        Amount = amount;
    }

}

[Serializable]
public class ItemContainerSaveData
{
    public ItemSlotSaveData[] SavedSlots;
    public ItemContainerSaveData(int numItems)
    {
        SavedSlots = new ItemSlotSaveData[numItems];
    }
}