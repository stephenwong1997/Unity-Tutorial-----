using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingAnimation_WakeUpAnimation : MonoBehaviour
{
    float timer;
    public AnimationClip Clip;
    public StartingDoorInteract door;
    private bool runOnce = false;
    // Start is called before the first frame update
    void Start()
    {
        timer = Clip.length;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && runOnce == false)
        {
            runOnce = true;
            //DialogueTrigger trigger = new DialogueTrigger();
            //trigger.dataBase = CSV_DataBases_Enum.Dialogue_Main;
            //trigger.ID = 1;
            //trigger.part = 1;
            //trigger.TriggerDialogue();
            FindObjectOfType<DialogueManager>().StartDialogue(1, 1);
            door.enabled = true;
        }
    }
}
