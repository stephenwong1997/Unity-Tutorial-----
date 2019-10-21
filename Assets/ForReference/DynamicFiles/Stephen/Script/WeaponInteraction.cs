using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInteraction : ShowTextOnRayCastInteraction
{

    public KeyCode interactionKey;



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
        base.HitEvent(hit);
        if (hit.collider.tag == objectTag || hit.collider.name == objectName)
        {
            DetectButtonClick();
        }
    }

    protected void DetectButtonClick()
    {
        if (Input.GetKeyDown(interactionKey))
        {
            

        }

    }

    protected override void ClearEvent()
    {
        base.ClearEvent();
    }

}
