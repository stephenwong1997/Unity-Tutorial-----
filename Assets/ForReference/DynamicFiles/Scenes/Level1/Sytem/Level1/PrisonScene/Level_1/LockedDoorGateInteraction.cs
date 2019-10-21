using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoorGateInteraction : ShowTextOnRayCastInteraction
{
    public KeyCode interactionKey;
    public bool locked;

    protected override void Start()
    {
        base.Start();
        locked = true ;
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
            if (locked == false)
            {
                this.transform.parent.GetComponent<Animator>().Play("OpenGate");
                Destroy(this);
            }
            else {

                // locked and try to open event
            }
        }

    }

    protected override void ClearEvent()
    {
        base.ClearEvent();
    }
}
