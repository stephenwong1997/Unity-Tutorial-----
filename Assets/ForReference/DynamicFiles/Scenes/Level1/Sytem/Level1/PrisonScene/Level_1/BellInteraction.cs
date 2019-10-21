using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellInteraction : ShowTextOnRayCastInteraction
{
    public KeyCode interactionKey;

    public GameObject triggerPathController;
    public string functionName;

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
            triggerPathController.GetComponent<PathController>().Invoke(functionName, 0);

        }

    }

    protected override void ClearEvent()
    {
        base.ClearEvent();
    }
}
