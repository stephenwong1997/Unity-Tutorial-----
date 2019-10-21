using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponManager : MonoBehaviour
{
    public GameObject LeftArm; // special case

    public void EnableWeaponDetection(Weapon weapon)
    {
        if (weapon == Weapon.Armed)
        {
            transform.Find(weapon.ToString()).Find("Cube").gameObject.SetActive(true);
            LeftArm.gameObject.SetActive(true);
        }
        else if (weapon == Weapon.Maul)
        {
            transform.Find(weapon.ToString()).Find("Cube").gameObject.SetActive(true);
        }else if (weapon == Weapon.Dagger)
        {
            transform.Find(weapon.ToString()).Find("Cube").gameObject.SetActive(true);
        }
    }
    public void DisableWeaponDetection(Weapon weapon)
    {
        if (weapon == Weapon.Armed)
        {
            transform.Find(weapon.ToString()).Find("Cube").gameObject.SetActive(false);
            LeftArm.gameObject.SetActive(false);
        }
        else if (weapon == Weapon.Maul)
        {
            transform.Find(weapon.ToString()).Find("Cube").gameObject.SetActive(false);
        }
        else if (weapon == Weapon.Dagger)
        {
            transform.Find(weapon.ToString()).Find("Cube").gameObject.SetActive(false);
        }
    }
}
