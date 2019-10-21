using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingArrow : MonoBehaviour
{
    public string targetName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent)
        { }
        else { this.transform.position += new Vector3(0, 0, 15f * Time.deltaTime); }
       
    }
    private void OnCollisionEnter(Collision collision)
    {
        print(collision.collider.name);
        if (collision.collider.name == targetName)
        {
            this.transform.parent = collision.transform.Find("hips").Find("spine").transform;
            this.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
