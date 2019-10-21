using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FadeIn : MonoBehaviour
{
    public Image fadeInImage;
    public bool fadeIn;
    private float alpha;
    // Start is called before the first frame update
    void Start()
    {
        if (fadeIn)
        {
            alpha = 0;
        }
        else
        {
            alpha = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        fadeInImage.GetComponent<Image>();
        fadeInImage.color = new Vector4(fadeInImage.color.r, fadeInImage.color.g, fadeInImage.color.b, alpha);
        if (fadeIn)
        {
            alpha += Time.deltaTime/3f;

        }
        else {
            alpha -= Time.deltaTime/3f;
        }
    }
}
