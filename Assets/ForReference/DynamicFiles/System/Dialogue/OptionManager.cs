using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OptionManager : MonoBehaviour
{
    public GameObject Button;
    private CameraLock cameraLock;

    public void Start()
    {
        cameraLock = FindObjectOfType<CameraLock>();
    }

    public void CreateOptionButton(CSV_Action action)
    {
 
        Vector3 buttonPos = new Vector3(Screen.width / 2, Screen.height / 2 + 30, 0);
        foreach (CSV_Option option in CSV_DataBase.Option[int.Parse(action.parm) - 1])
        {
            GameObject createdButton = Instantiate(Button, FindObjectOfType<OptionManager>().transform);
            createdButton.transform.position = buttonPos;
            createdButton.transform.Find("Text").GetComponent<Text>().text = option.optionName;
            createdButton.GetComponent<Button>().onClick.AddListener(()=>ActionToMake(option.action));
            buttonPos += new Vector3(0, -50, 0);

        }

    }

    void ActionToMake(CSV_Action action)
    {
        CSV_ActionHandler.HandleAction(action);
        DestroyButtons();
    }

    void DestroyButtons()
    {

        foreach (Transform child in transform)
        {

            Destroy(child.gameObject);
        }
    }

}
