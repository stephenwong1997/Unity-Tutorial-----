//Created and Developed by Kieren Hovasapian (C) 2018 - Filmstorm
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//How to use
//Create an empty game object, call it "CameraBase" and then add this component - "Camera Follow" to it.
//Create another empty game object and parent/place it on the hips/pelvis bone of your character/player
//Then parent the MainCamera to this object and apply the "CameraCollision" to the camera. 

[AddComponentMenu("Filmstorm/Camera Follow")]
public class CameraFollow : MonoBehaviour
{
    [Header("Drag the Object you want to follow here")]
    [Space(5)]
    [Tooltip("The best way to use this is to create an empty gameobject and parent it to your players hip/pelvis bone.")]
    public GameObject CameraFollowObj;

    [Header("Adjust these values to how you want the camera to rotate")]
    [Space(5)]
    public float CameraMoveSpeed = 120.0f;
    public float clampAngle = 80.0f;
    public float inputSensitivity = 150.0f;

    Vector3 FollowPOS;
    GameObject CameraObj;
    GameObject PlayerObj;
    float mouseX;
    float mouseY;
    float rotY = 0.0f;
    float rotX = 0.0f;
    GameObject player;

    private CameraLock cameraLock;


    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
        cameraLock = FindObjectOfType<CameraLock>();
    }

    // Update is called once per frame
    void Update()
    {

        if (cameraLock.GetCameraLock()<=0)
        {
            mouseX = Input.GetAxis("Mouse X");
            mouseY = Input.GetAxis("Mouse Y");

            rotY += mouseX * inputSensitivity * Time.deltaTime;
            rotX += mouseY * inputSensitivity * Time.deltaTime;

            rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

            Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
            transform.rotation = localRotation;
        }

    }

    void LateUpdate()
    {
        CameraUpdater();
    }

    void CameraUpdater()
    {
        // set the target object to follow
        Transform target = CameraFollowObj.transform;

        //move towards the game object that is the target
        float step = CameraMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
}