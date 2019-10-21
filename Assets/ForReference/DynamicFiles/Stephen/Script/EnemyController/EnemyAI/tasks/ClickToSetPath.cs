using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToSetPath : MonoBehaviour
{
    public GameObject triggerEvent;
    public string functionName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            triggerEvent.GetComponent<PathController>().Invoke(functionName,0);
        }

    }
}
