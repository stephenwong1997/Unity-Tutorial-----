using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CSV_DataBases_Enum{
    Dialogue_Main,
    Dialogue_Side1,
    Dialogue_Side2
}

public class DialogueTrigger : MonoBehaviour {

    private DialogueManager dialogueManager;

    public CSV_DataBases_Enum dataBase;
    public int ID;
    public int part;
    private int sequence;

    public void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
   

    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("ID = " + ID + " Part = " + part + " seqence = " + sequence + "    " + CSV_DataBase.Dialogue[ID][part][sequence].sentences);
            sequence++;
            if (CSV_DataBase.Dialogue[ID][part].Count == sequence)
            {
                sequence = 0;
                part++;
            }
            if (CSV_DataBase.Dialogue[ID].Count == part)
            {
                ID++;
                part = 0;
                sequence = 0;
            }
            //print(CSV_DataBase.Dialogue[ID][part][sequence].sentences);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            TriggerDialogue();
        }

    }

    public void TriggerDialogue ()
	{
            dialogueManager.StartDialogue(ID,part);

    }

}
