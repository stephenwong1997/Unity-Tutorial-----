using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyStats : EnemyStats
{

    public float skill1;
    public float skill2;
    public float skill3;

    public float GetSkill1Damage()
    {
        return baseDamage * skill1;
    }

    public float GetSkill2Damage()
    {
        return baseDamage * skill2;
    }
    public float GetSkill3Damage()
    {
        return baseDamage * skill3;
    }

}
