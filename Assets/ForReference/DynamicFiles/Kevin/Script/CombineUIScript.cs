using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CombineUIScript : MonoBehaviour
{
    public GameObject MapMenu;
    //public static bool UsingMap = false;
    //public static bool UsingMission = false;
    public GameObject MissionMenu;
    public static bool Pausing = false;
    public GameObject PauseMenu;
    public GameObject MainGameCanvas;
    public bool inStartMenu;
    public GameObject CombinedMenu;
    public static bool UsingCombinedMenu = false;
    public GameObject loadingScreen;
    public Slider slider;
    public Text reminder;


    private string[] ReminderArray = new string[] 
    {
        "要獲得惡魔的力量，必先付出代價",
        "當人失去了靈魂，人性和法力亦會隨之消去" ,
        "靈魂中有著人性、法力和記憶",
        //"惡靈是沉重的，而人類的靈魂則是極輕的，惡魔死後留下的惡靈會留在地上，而人類的靈魂則會四散，升往天上"
    };




    void Update()
    {
        if(inStartMenu == false)
        {//pause
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (Pausing == true)
                {
                    resume();
                }
                else
                {
                    pause();
                }
            }
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (Pausing == false)
                {
                    if (UsingCombinedMenu == false)
                    {
                        CombinedMenu.SetActive(true);
                        UsingCombinedMenu = true;
                        Time.timeScale = 0.0f;
                    }
                    else
                    {
                        CombinedMenu.SetActive(false);
                        UsingCombinedMenu = false;
                        Time.timeScale = 1.0f;
                    }

                }
            }
        }
    }

    //TpMethod
    public void tp(int TpIndex)
    {
        Time.timeScale = 1.0f;
        StartCoroutine(LoadAsynchronously(TpIndex));
    }

    IEnumerator LoadAsynchronously(int TpIndex)
    {
        Time.timeScale = 1.0f;
        AsyncOperation operation = SceneManager.LoadSceneAsync(TpIndex);
        loadingScreen.SetActive(true);
        reminder.text = ReminderArray[Random.Range(0, ReminderArray.Length)];

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress/0.9f);
            //process is the float who goes from 0 to 1;
            //loading is divided to two stages(loading=0-0.9,activation=0.9-1)
            Debug.Log(progress);
            slider.value = progress;
            //wait for 1 frame
            yield return null;
        }
        loadingScreen.SetActive(false);
    }

    //PasueMethod
    public void resume()
    {
        Time.timeScale = 1.0f;
        PauseMenu.SetActive(false);
        Pausing = false;
    }

    void pause()
    {
        Time.timeScale = 0.0f;
        PauseMenu.SetActive(true);
        Pausing = true;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MenuScene");
        resume();
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void Destroy()
    {
        Destroy(MainGameCanvas);
    }

    public void StartMenu(bool change)
    {
        inStartMenu = change;
    }
}
