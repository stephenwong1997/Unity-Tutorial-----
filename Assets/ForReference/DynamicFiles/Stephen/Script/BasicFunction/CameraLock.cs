using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLock : MonoBehaviour
{
    private int cameraLock;


    //private void Update()
    //{
    //    print(cameraLock);
    //}

    public int GetCameraLock()
    {
        return cameraLock;
    }

    public void LockCameraLock()
    {
        cameraLock++;
    }
    public void UnlockCameraLock()
    {
        cameraLock--;
    }

 
}
