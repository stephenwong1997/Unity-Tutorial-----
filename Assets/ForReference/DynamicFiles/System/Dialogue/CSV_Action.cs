using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSV_Action 
{
    public string actionType;
    public string parm;

    public CSV_Action(string actionType, string parm)
    {
        this.actionType = actionType;
        this.parm = parm;
    }
}
