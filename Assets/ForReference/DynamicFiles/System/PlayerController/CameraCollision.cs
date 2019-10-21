//Created and Developed by Kieren Hovasapian (C) 2018 - Filmstorm
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//How to use
//Create an empty game object, call it "CameraBase" and then add the component - "Camera Follow" to it.
//Create another empty game object and parent/place it on the hips/pelvis bone of your character/player
//Then parent the MainCamera to this object and apply the "CameraCollision" to the camera. 

[AddComponentMenu("Filmstorm/Camera Collision")]
public class CameraCollision : MonoBehaviour
{

    public float minDistance = 0.0f;
    public float initialDistance = 5.0f;
    public float contrainsMaxDistance = 6f;
    public float mouseRollScroll = 70f;
    public float smooth = 10.0f;
    Vector3 dollyDir;
    public Vector3 dollyDirAdjusted;
    public float distance;

    // Use this for initialization
    void Awake()
    {
        dollyDir = transform.localPosition.normalized;
        distance = transform.localPosition.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        initialDistance -= Input.GetAxis("Mouse ScrollWheel") * mouseRollScroll * Time.deltaTime;
        initialDistance = Mathf.Clamp(initialDistance, 0, contrainsMaxDistance);
        Vector3 desiredCameraPos = transform.parent.TransformPoint(dollyDir * initialDistance);

        RaycastHit closestValidHit = new RaycastHit();
        RaycastHit[] hits = Physics.RaycastAll(transform.parent.position, desiredCameraPos- transform.parent.position);
        //Debug.DrawRay(transform.parent.position,( desiredCameraPos- transform.parent.position) * 5f, Color.red);
        foreach (RaycastHit hit in hits)
        {
            if (!hit.transform.IsChildOf(GameObject.FindGameObjectWithTag("Player").transform)&&
               GameObject.FindGameObjectWithTag("Enemy")&&!hit.transform.IsChildOf(GameObject.FindGameObjectWithTag("Enemy").transform)&& !(hit.transform.tag=="Enemy")
               && GameObject.FindGameObjectWithTag("CameraCollisonIgnore") && !(hit.transform.tag == "CameraCollisonIgnore")
                && (closestValidHit.collider == null || closestValidHit.distance > hit.distance))
            {
                    closestValidHit = hit;
            }
        }


        if (closestValidHit.collider)
        {
            distance = Mathf.Clamp((closestValidHit.distance * 0.95f), minDistance, initialDistance);
        }
        else
        {
            distance = initialDistance;
        }


        transform.localPosition = Vector3.Lerp(transform.localPosition, dollyDir * distance, Time.deltaTime * smooth);
    }
}