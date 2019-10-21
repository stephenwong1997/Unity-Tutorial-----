using UnityEngine;

public enum EquipmentType
{
    Helmet,
    Chest,
    Gloves,
    Boots,
    Weapon1,
    Weapon2,
    Accessory1,
    Accessory2

}

public enum Rarity { 
    Common,
    Rare,
    Legendary
}

[CreateAssetMenu(menuName ="Items/Equippable Item")]
public class EquippableItem :Item
{
    public CharacterStats characterStats;
    public Rarity rarity;
    public int StrengthBonus;
    public int AgilityBonus;
    public int IntelligenceBonus;
    [Space]
    public float StrengthPercentBonus;
    public float AgilityPercentBonus;
    public float IntelligencePercentBonus;
    [Space]
    public Weapon weapon;
    public EquipmentType equipmentType;

    public override Item GetCopy()
    {

        return Instantiate(this);
    }

    public override void Destroy()
    {
        Destroy(this);
    }

    private void ChangeCharStatsWeapon(Weapon weapon)
    {
        characterStats.SetWeapon(weapon);
    }


    public void Equip(Character c)
    {

        Debug.Log("Equip : " + c.name);
        
    }

    public void Unequip(Character c)
    {
        Debug.Log("Unequip : " + c.name);
    }

    public override string GetItemType()
    {
        return equipmentType.ToString();
    }
    public override string GetDescription()
    {
        sb.Length = 0;
        AddStat(StrengthBonus, "Strength", false);
        AddStat(AgilityBonus, "Agility", false);
        AddStat(IntelligenceBonus, "Intelligence", false);

        AddStat(StrengthPercentBonus, "Strength", true);
        AddStat(AgilityPercentBonus, "Agility", true);
        AddStat(IntelligencePercentBonus, "Intelligence", true);
        return sb.ToString();
    }
    private void AddStat(float value, string statName, bool isPercent = false)
    {
        if (value != 0)
        {
            if (sb.Length > 0)
            {
                sb.AppendLine();
            }
            if (value > 0)
            {
                sb.Append("+");
            }
            if (isPercent)
            {
                sb.Append(value * 100);
                sb.Append("% ");
            }
            else
            {
                sb.Append(value);
                sb.Append(" ");
            }

            sb.Append(statName);
        }

    }
}
