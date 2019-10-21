using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObjectDemo : MonoBehaviour
{
    private SphereCollider m_Collider;
    public float cusRadius = 3f;
    // Start is called before the first frame update
    void Start()
    {
        m_Collider = gameObject.AddComponent<SphereCollider>();
        m_Collider.radius = cusRadius;
        m_Collider.transform.position = this.transform.position;
        m_Collider.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.tag);
        if(other.gameObject.tag == "Player")
        {
            print("pick me pick me");

        }
    }
}
