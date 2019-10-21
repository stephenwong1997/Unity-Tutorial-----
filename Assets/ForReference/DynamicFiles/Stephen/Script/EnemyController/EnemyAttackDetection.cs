using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackDetection : MonoBehaviour
{
    CharacterStats charStats;
    private void Start()
    {
        charStats = FindObjectOfType<CharacterStats>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            charStats.GetComponent<CharacterStats>().TakeDamage(this.transform.root.GetComponent<EnemyStats>().getEnemyDamage());
            other.GetComponent<ThirdPersonController>().GetHit();
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                //Blood effect
                
            }
        }
    }
}
