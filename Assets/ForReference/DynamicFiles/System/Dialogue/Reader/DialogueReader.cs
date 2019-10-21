using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
public class DialogueReader 
{
    static List<List<List<Dialogue>>> returnDialogue = new List<List<List<Dialogue>>>();

    static List<List<Dialogue>> temDialogue_B = new List<List<Dialogue>>();

    static List<Dialogue> temDialogue_A=  new List<Dialogue>();



    private static int IDindex = 1;
    private static int PartIndex = 1;


    public static List<List<List<Dialogue>>> Reader(string path)
    {
        ClearDialogueReturner();

        StreamReader sr = null;
        sr = File.OpenText(path);
        string line;
        line = sr.ReadLine();
        while ((line = sr.ReadLine()) != null)
        {
            string[] lineData = line.Split(',');
            if (int.Parse(lineData[0]) == IDindex)
            {
                if (int.Parse(lineData[1]) == PartIndex)
                {
                    AddNewLine(lineData);

                }
                else
                {
                    AddTotDialogue_B();
                    ClearTemDialogue_A();
                    AddNewLine(lineData);
                    PartIndex++;

                }
            }
            else {
                AddTotDialogue_B();
                AddToReturnDialogue();
                ClearTemDialogue_A();
                ClearTemDialogue_B();
                AddNewLine(lineData);
                PartIndex = 1;
                IDindex++;
            }

        }
        AddTotDialogue_B();
        AddToReturnDialogue();
        //Debug.Log("Return with size : " + returnDialogue.Count);
        return returnDialogue;
    }

    private static void AddNewLine(string[] lineData)
    {
        Dialogue temDia = new Dialogue();
        temDia.name = lineData[3];
        temDia.sentences = lineData[4];
        temDia.seconds = lineData[5] == "Null" ? -1 : int.Parse(lineData[5]);
        temDia.action = new CSV_Action(lineData[6], lineData[7]);
        //Debug.Log("add" + temDia.sentences + " to temDialogue_A");
        temDialogue_A.Add(temDia);
    }

    private static void AddTotDialogue_B()
    {
        //Debug.Log("=========Add to temDialogue_B==========");
        temDialogue_B.Add(temDialogue_A);
    }

    private static void AddToReturnDialogue()
    {
        //Debug.Log("==========================Add to ReturnDialogue==================");
        returnDialogue.Add(temDialogue_B);
    }

    private static void ClearTemDialogue_A()
    {
        //Debug.Log("------------Clear temDialogue_A------------");
        temDialogue_A = new List<Dialogue>();
    }

    private static void ClearTemDialogue_B()
    {
        //Debug.Log("--------------------------Clear temDialogue_B------------------");
        temDialogue_B = new List<List<Dialogue>>();
    }

    private static void ClearDialogueReturner()
    {
        //Debug.Log("Clear DialogueReturner");
        returnDialogue = new List<List<List<Dialogue>>>();
    }
}
