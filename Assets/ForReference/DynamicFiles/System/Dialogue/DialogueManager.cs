using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    public Text nameText;
    public Text dialogueText;
    public Text timerDialogueText;

    public Animator animator;

    private GameObject optionManager; // for referencing when to lock camera

    private Queue<Dialogue> dialogue;
    private CameraLock cameraLock;

    private CSV_Action fadeSentenceAction;
    private CSV_Action afterDialogue;
    void Awake()
    {
        dialogue = new Queue<Dialogue>();
        cameraLock = FindObjectOfType<CameraLock>();
        optionManager = FindObjectOfType<OptionManager>().gameObject;
        afterDialogue = new CSV_Action("", "");
    }

    void OnGUI()
    {
        if (Event.current != null && Event.current.type == EventType.MouseDown && optionManager.transform.childCount<=0 )
        {
            if(animator.GetBool("IsOpen"))
            DisplayNextSentence();
        }
    }

    public void StartDialogue(int ID, int part)
    {
        dialogue.Clear();
        foreach (Dialogue dia in CSV_DataBase.Dialogue[ID-1][part-1]) {
            dialogue.Enqueue(dia);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        CSV_ActionHandler.HandleAction(afterDialogue);
        if (dialogue.Count == 0)
        {
            ToggleDialogue(false);
            return;
        }
        else {
            ProcessDialogue();
        }
    }

    private void ProcessDialogue()
    {
 
        Dialogue temDia = dialogue.Dequeue();
        if (temDia.seconds > 0)
        {
            ToggleDialogue(false);
            timerDialogueText.color = new Color(timerDialogueText.color.r, timerDialogueText.color.g, timerDialogueText.color.b, 1);
            timerDialogueText.text = temDia.sentences;
            fadeSentenceAction = temDia.action;
            Invoke("StartFadeInWords", temDia.seconds - 0.25f);

        }
        else {
            ToggleDialogue(true);
            nameText.text = temDia.name;
            string sentence = temDia.sentences;
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
            if (temDia.action.actionType.Trim().ToLower() == "option")
            { CSV_ActionHandler.HandleAction(temDia.action);
                afterDialogue = new CSV_Action("", "") ;
            }
            else {
                afterDialogue = temDia.action;
            }


        }
  

    }

    private void StartFadeInWords()
    {
        StopAllCoroutines();
        StartCoroutine(FadeIn()); 
    }

    IEnumerator FadeIn()
    {
        for (float i = 1; i > -0.5f; i -= 0.1f)
        {
            timerDialogueText.color = new Color(timerDialogueText.color.r, timerDialogueText.color.g, timerDialogueText.color.b, i);
            yield return null ;
        }
        CSV_ActionHandler.HandleAction(fadeSentenceAction);
        DisplayNextSentence();

    }
    IEnumerator TypeSentence (string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
	}

	void ToggleDialogue(bool toggle)
	{
        
        if (animator.GetBool("IsOpen") == false && toggle == true) {
            animator.SetBool("IsOpen", toggle);
            cameraLock.LockCameraLock();
        }
        if (animator.GetBool("IsOpen") == true && toggle == false)
        {
            animator.SetBool("IsOpen", toggle);

            Invoke("UnlockCamera", 0.5f);
        }

    }

    void UnlockCamera()
    {
        cameraLock.UnlockCameraLock();
    }

}
