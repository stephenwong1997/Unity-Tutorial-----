using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
public class CSV_DataBase : MonoBehaviour
{
    public static List<List<List<Dialogue>>> Dialogue = new List<List<List<Dialogue>>>();
    public static List<List<CSV_Option>> Option = new List<List<CSV_Option>>();


    string[] fileName = { "Dialogues", "OptionList" };

    private void Awake()
    {
        ReadCSV();
    }

    public void ReadCSV() //Assets/DynamicFiles/System/SystemFiles/dialoguesdialogues.csv
    {

        for (int i = 0; i < fileName.Length; i++)
        {
            string file_path = "Assets/DynamicFiles/System/SystemFiles/dialogues/" + fileName[i] + ".csv";

            switch (fileName[i])
            {
                case "Dialogues":
                    Dialogue = DialogueReader.Reader(file_path);
                    break;
                case "OptionList":
                    Option = OptionListReader.Reader(file_path);
                    break;
                default:
                    break;
            }
        }
        //StreamReader sr = null;
        //for (int i = 0; i < fileName.Length; i++)
        //{
        //try
        //{
        //string file_url = "Assets/DynamicFiles/System/SystemFiles/dialogues/" + fileName[i] + ".csv";
        //sr = File.OpenText(file_url);
        //}
        //catch
        //{
        //    Debug.Log("File not found");
        //    return;
        //}

        //string line;
        //while ((line = sr.ReadLine()) != null)
        //{
        //    switch (fileName[i])
        //    {
        //        case "Dialogues":
        //            ReadDiaolgues(line);
        //            break;
        //        default:
        //            break;
        //    }


        //}

        //}

        //sr.Close();
        //sr.Dispose();

    }

    void ReadDiaolgues(string path)
    {
        StreamReader sr = null;
        sr = File.OpenText(path);
    }
}
