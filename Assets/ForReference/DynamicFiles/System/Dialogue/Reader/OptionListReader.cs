using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
public class OptionListReader
{
    private static List<List<CSV_Option>> returnOption = new List<List<CSV_Option>>();
    private static List<CSV_Option> optionList = new List<CSV_Option>();

    private static int IDindex = 1;

    public static List<List<CSV_Option>> Reader(string path)
    {

        ClearOptionReturner();

        StreamReader sr = null;
        sr = File.OpenText(path);
        string line;
        line = sr.ReadLine();
        while ((line = sr.ReadLine()) != null)
        {
            string[] lineData = line.Split(',');
            if (int.Parse(lineData[0]) == IDindex)
            {
                ClearOptionList();
                AddNewLine(lineData);
                returnOption.Add(optionList);
                IDindex++;
            }
        }

        return returnOption;
    }

    static void AddNewLine(string[] lineData)
    {
        for (int i = 0; i < 3; i++)
        {
            CSV_Option temOption = new CSV_Option();
            if (lineData[1 + i * 3] != "")
            {
                //Debug.Log("ADD : " + lineData[1 + i * 3]);
                temOption.optionName = lineData[1 + i * 3];
                //temOption.action.actionType = lineData[2];
                //temOption.action.parm = lineData[3];
                temOption.action = new CSV_Action(lineData[2 + i * 3], lineData[3 + i * 3]);
                optionList.Add(temOption);
            }
        }


    }

    static void ClearOptionReturner()
    {
        returnOption = new List<List<CSV_Option>>();
    }
    static void ClearOptionList()
    {
        optionList = new List<CSV_Option>();
    }


}
