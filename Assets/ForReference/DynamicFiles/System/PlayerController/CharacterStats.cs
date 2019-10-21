using UnityEngine;
public enum Weapon
{
    Armed = 0,
    Dagger = 5,
    Maul = 20
}

enum Armor
{
    None = 0,
    KnightArmor =10 
}



public class CharacterStats : MonoBehaviour
{
    [SerializeField]
    private string characterName;
    [SerializeField]
    private int health;
    [SerializeField]
    private int maxHealth;

    [SerializeField]
    private int mana;
    [SerializeField]
    private int maxMana;

    [SerializeField][Range(0,12)]
    private float movementSpeed = 3;
    [SerializeField]
    private int baseDamage;

    [SerializeField]
    private float Strength;
    [SerializeField]
    private float Agility;
    [SerializeField]
    private float Intelligence;

    [SerializeField]
    private Weapon havingWeapon = Weapon.Armed;
    [SerializeField]
    private Weapon currentWeapon = Weapon.Armed;
    [SerializeField]
    private Armor armor;


    [SerializeField]
    private int level;
    [SerializeField]
    private int experience;
    [SerializeField]
    private int skillPoint;

    [SerializeField]
    private int humnaSoul;
    [SerializeField]
    private int demenSoul;

    private StatsPanel statsPanel;


    public void TakeDamage(int damage)
    {
        health -= Mathf.Max(1,(damage - (int)armor));

    }



    public string GetName()
    {
        return this.characterName;
    }

    public Weapon GetHavingWeapon()
    {
        return havingWeapon;
    }

    public Weapon GetWeapon()
    {
        return currentWeapon;
    }

    public void SetWeapon(Weapon weapon)
    {
        this.currentWeapon = weapon;
    }

    public int getPlayerDamage() {
        return baseDamage + (int)currentWeapon;
    }

    public float getPlayerHealthPercentage()
    {

        return (float)health/ maxHealth;
    }

    public void SetName(string name)
    {
        this.characterName = name;
    }
 

    public float getSpeed()
    {
        return movementSpeed;
    }

    public float[] getStats()
    {
        float[] temStats = new float[3];
        temStats[0] = this.Strength;
        temStats[1] = this.Agility;
        temStats[2] = this.Intelligence;
        return (temStats);
    }
}
