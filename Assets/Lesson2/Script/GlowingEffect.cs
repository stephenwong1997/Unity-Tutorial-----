using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlowingEffect : MonoBehaviour
{

    Outline outline;
    public float alpha;
    public float changingSpeed ;
    [Range(0,255)]
    public float minRange;
    [Range(0, 255)]
    public float maxRange;
    // Start is called before the first frame update
    void Start()
    {
        outline = GetComponent<Outline>();
        alpha = minRange;
    }

    // Update is called once per frame
    void Update()
    {
        alpha += changingSpeed;
        if (alpha >= maxRange || alpha <= minRange)
        {
            changingSpeed *= -1;
        }
        outline.effectColor = new Color(outline.effectColor.r, outline.effectColor.g, outline.effectColor.b, alpha/255);
    }
}
