using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDetectionPoint : MonoBehaviour
{
    CharacterStats characterStats;
    //public GameObject bloodEffect;
    private void Start()
    {
        characterStats = FindObjectOfType<CharacterStats>();
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Enemy")
    //    {
    //        other.GetComponent<EnemyStats>().TakeDamage(player.GetComponent<CharacterStats>().getPlayerDamage());
    //        if (other.GetComponent<EnemyStats>().KnockThreshold() < player.GetComponent<CharacterStats>().getPlayerDamage())
    //        {
    //            //other.GetComponent<Animator>().Play("getHit");
    //        }
    //        RaycastHit hit;

    //        if (Physics.Raycast(transform.position, transform.forward, out hit))
    //        {
    //            //print("HIt");
    //            Instantiate(bloodEffect, hit.point, hit.transform.rotation, hit.transform);
    //            ////Blood effect
    //        }
    //    }
    //}
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyAI>().TakeDamage(characterStats.getPlayerDamage());
        }
    }
}
