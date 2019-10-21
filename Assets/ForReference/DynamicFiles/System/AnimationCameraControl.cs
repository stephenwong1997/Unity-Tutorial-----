using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCameraControl : MonoBehaviour
{

    public void ResetCameraToMain()
    {
        this.GetComponent<Camera>().targetDisplay = 1;
        Camera.main.targetDisplay = 0;
    }


    public void ChangeTransform(Transform transform)
    {
        this.transform.position = transform.position;
        this.transform.rotation = transform.rotation;
        this.GetComponent<Camera>().targetDisplay = 0;
        Camera.main.targetDisplay = 1;
    }

}
