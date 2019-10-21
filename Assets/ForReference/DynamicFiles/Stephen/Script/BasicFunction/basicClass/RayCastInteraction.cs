using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastInteraction : MonoBehaviour
{
    protected GameObject player;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit closestValidHit = new RaycastHit();
        RaycastHit[] hits = Physics.RaycastAll(ray, 5, 1, QueryTriggerInteraction.Ignore);
        //Debug.DrawRay(transform.parent.position,( desiredCameraPos- transform.parent.position) * 5f, Color.red);
        foreach (RaycastHit hit in hits)
        {
            if (!hit.transform.IsChildOf(player.transform) && (closestValidHit.collider == null || closestValidHit.distance > hit.distance))
            {
                closestValidHit = hit;
            }
        }
        if (closestValidHit.collider)
        {
            HitEvent(closestValidHit);
        }
        else {
            ClearEvent();
        }
    }

    protected virtual void HitEvent(RaycastHit hit)
    {
        
    }
    protected virtual void ClearEvent()
    {


    }

}
