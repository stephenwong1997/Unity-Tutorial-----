using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSV_SpecialScriptToCall : MonoBehaviour
{

    public void CallFunction(CSV_Action action)
    {
        switch (int.Parse(action.parm) )
        {
            case 1:
                Function1();
                break;

        }

    }

    void Function1()
    {
        FindObjectOfType<AnimationCameraControl>().ResetCameraToMain();
    }

}
