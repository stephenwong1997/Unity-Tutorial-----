using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testJump : MonoBehaviour
{
    public bool isGounded;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isGounded = this.GetComponent<CharacterController>().isGrounded;
    }
}
