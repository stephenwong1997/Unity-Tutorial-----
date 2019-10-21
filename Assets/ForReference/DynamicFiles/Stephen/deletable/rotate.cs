using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    public GameObject a;
    public GameObject b;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0; 
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        print((int)timer);
        a.transform.Rotate(0,-360/7f*Time.deltaTime,0);
        b.transform.Rotate(0, 360 / 9f * Time.deltaTime, 0);
        
    }
}
