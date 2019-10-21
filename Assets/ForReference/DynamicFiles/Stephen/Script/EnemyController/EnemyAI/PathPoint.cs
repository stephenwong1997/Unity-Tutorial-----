using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PathPoint
{
    public GameObject pathPoint;
    public float waitingTime = 0;
    public float rotationSecond =1;
    public float rotation = -1;
    public GameObject eventToCall;
    public string functionName;

}
