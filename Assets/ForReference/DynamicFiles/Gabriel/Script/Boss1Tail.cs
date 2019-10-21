using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Boss1Tail : MonoBehaviour
{
    private Boss1AI boss1AI;

    // Start is called before the first frame update
    void Awake()
    {
        if (GetComponentInParent<Boss1AI>()) boss1AI = GetComponentInParent<Boss1AI>(); else Debug.Log("Tail cant find the boss1AI");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        //TODO: Add the blood effect 
        //ContactPoint contact = collision.contacts[0];
        //Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        //Vector3 pos = contact.point;
        //Instantiate(explosionPrefab, pos, rot);


        //check the animation of the player if it is attacking and create hitbox for the player 
        if(other.gameObject.tag == "Player")
        {
            boss1AI.HandleTrigger(Boss1AI.BodyPart.Tail);
        }
    }
}
