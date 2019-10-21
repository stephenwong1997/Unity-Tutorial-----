using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFadingSystem : MonoBehaviour
{
    Animator FadingAnimator;
    // Start is called before the first frame update
    void Start()
    {
        FadingAnimator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            FadingAnimator.Play("FadeOut");
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            FadingAnimator.Play("FadeIn");
        }

    }
}
