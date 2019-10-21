using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyScript : MonoBehaviour
{
    public GameObject UI1;

    private void Awake()
    {
        DontDestroyOnLoad(UI1);

    }

    public void destory()
    {
        Destroy(UI1);
    }
}
