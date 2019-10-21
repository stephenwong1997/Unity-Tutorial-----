using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorInteraction : ShowTextOnRayCastInteraction
{
    public KeyCode interactionKey;
    public GameObject WatchCamera;
    public GameObject DoorToOpen;
    public GameObject mainCamera;

    private bool opened;
    private float timer;
    protected override void Start()
    {
        base.Start();
        opened = false;
        mainCamera = Camera.main.gameObject;



    }

    protected override void Update()
    {
        base.Update();

        if (!mainCamera.activeSelf)
        {
            timer += Time.deltaTime;
        }
        if (timer > DoorToOpen.GetComponent<Animator>().runtimeAnimatorController.animationClips[0].length*2.3  )
        {
            mainCamera.SetActive( true);
            WatchCamera.SetActive(false);
        }

    }



    protected override void HitEvent(RaycastHit hit)
    {

        if (hit.collider.tag == objectTag || hit.collider.name == objectName && !opened)
        {
            base.HitEvent(hit);
            DetectButtonClick();
 
        }
    }

    protected void DetectButtonClick()
    {
        if (Input.GetKeyDown(interactionKey))
        {
            opened = true;
            DoorToOpen.GetComponent<Animator>().Play("OpenGate") ;
            mainCamera.SetActive(false);
            WatchCamera.SetActive(true);
        }

    }
}
