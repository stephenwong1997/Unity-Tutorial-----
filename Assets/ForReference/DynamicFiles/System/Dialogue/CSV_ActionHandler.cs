using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSV_ActionHandler :MonoBehaviour
{
    public static void HandleAction(CSV_Action action)
    {
        switch (action.actionType.Trim().ToLower())
        {
            case "option":
                FindObjectOfType<OptionManager>().CreateOptionButton(action);
                break;
            case "dialogue":
                string[] parmData = action.parm.Split('|');
                //print((int.Parse(parmData[0]) - 1 )+ "   " + (int.Parse(parmData[1]) - 1));
                FindObjectOfType<DialogueManager>().StartDialogue(int.Parse(parmData[0]), int.Parse(parmData[1]));
                break;
            case "script":
                FindObjectOfType<CSV_SpecialScriptToCall>().CallFunction(action);
                break;
             default:
                break;
       
        }

    }
}
