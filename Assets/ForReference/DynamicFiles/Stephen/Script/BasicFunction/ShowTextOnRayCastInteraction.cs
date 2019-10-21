using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShowTextOnRayCastInteraction : RayCastInteraction
{
    [Header("Either fill in name or tag")]
    public string objectName;
    public string objectTag;
    public string showString;


    
    public GameObject interactionCanvas;
    GameObject CanvasContainer;
    protected override void Start()
    {
        base.Start();
    }


    protected override void Update()
    {
        base.Update();
    }

    protected override void HitEvent(RaycastHit hit)
    {
        if (hit.collider.tag == objectTag || hit.collider.name == objectName)
        {
            CreateText();
        }

    }

    protected override void ClearEvent()
    {
        DestroyText();

    }

    protected void CreateText()
    {
        if (!CanvasContainer)
        {
            CanvasContainer = Instantiate(interactionCanvas);
            CanvasContainer.transform.Find("Text").GetComponent<Text>().text = showString;
        }
    }

    protected void DestroyText()
    {
        if (CanvasContainer)
        {
            Destroy(CanvasContainer);
        }
    }

    protected void OnDestroy()
    {
        DestroyText();
    }

}
