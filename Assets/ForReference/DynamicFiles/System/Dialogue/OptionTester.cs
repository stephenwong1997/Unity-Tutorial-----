using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionTester : MonoBehaviour
{
    public int index;
    public int optionNumber;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            print(CSV_DataBase.Option[index][optionNumber].optionName);
        }
    }
}
