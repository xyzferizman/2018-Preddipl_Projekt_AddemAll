using System;
using System.Collections;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    
    private bool task = false;
    private bool level = false;
    private bool subject = false;
    private bool year = false;
    private bool load = false;
    private Button[] buttons;
    public Text text;

    public void Awake()
    {


        
        
    }

    public void Start()
    {
        buttons = GetComponentsInChildren<Button>();
        
        foreach (Button b in buttons)
        {
            if(b.name != "Quit")
            {

                b.interactable = false;
            }
        }
    }

    public void Update()
    {
        if (!year&&!load)
        {
            load = true;
            Debug.Log("year");
            StartCoroutine(ListYears());
            
        }
        else if(!level&&!load)
        {
            load = true;
            Debug.Log("level");
            StartCoroutine(LoadLevel());
        }
        else if(!subject&&!load)
        {
            load = true;
            Debug.Log("subject");
            StartCoroutine(ListSubject());
        }
        else if(!task&&!load)
        {
            load = true;
            Debug.Log("task");
            StartCoroutine(TaskUpdate());
        }
        //Debug.Log("year:" + year + " level:" + level + " subject: " + subject + " task: " + task);
        if (year&&task&&level&&subject)
        {
            text.text = "";
            foreach(Button b in buttons)
            {
                b.interactable = true;
            }
        }
        else
        {
            text.text = "Podaci nisu učitani, povežite se s internetom";
        }
    }

    public void Login()
    {
        SceneManager.LoadScene(1);
    }
    public void Register()
    {
        SceneManager.LoadScene(2);
    }
    public void Quit()
    {
        Application.Quit();
    }






    

    
    

    

    

    private IEnumerator ListSubject()
    {
        WWWForm form = new WWWForm();
        WWW www = new WWW(GlobalVariables.LoginURL + "listSubjects.php", form);


        yield return www;
        string[] result = www.text.Split('\n');
        bool good = true ;
        foreach (string line in result)
        {
            if(line == "")
            {
                continue;
            }
            try
            {
                string[] data = line.Split('-');
                Subject.AddSubject(Int32.Parse(data[0]), data[1], Int32.Parse(data[2]));
            }
            catch
            {
                good = false;
            }
        }
        subject = good;
        year = good;
        load = false;

    }

    

    private IEnumerator TaskUpdate()
    {
        WWWForm form = new WWWForm();
        form.AddField("year", 0);
        form.AddField("level", 0);
        form.AddField("strenght", 0);
        form.AddField("subject", "");
        form.AddField("author_id", 0);

        WWW www = new WWW(GlobalVariables.LoginURL + "loadQuestion.php", form);
        
        yield return www;
        //Debug.Log(www.text);
        bool good = true;
        if (www.text != "")
        {
            try
            {
                Questions.AddQuestions(www.text);
            }
            catch
            {
                good = false;
            }
        }
        year = good;
        task = good;
        load = false;
        
    }

    private IEnumerator LoadLevel()
    {
        WWWForm form = new WWWForm();
        form.AddField("id_year", 0);
        form.AddField("id_subject", 0);

        WWW www = new WWW(GlobalVariables.LoginURL + "listLevel.php", form);

        yield return www;
        //Debug.Log("leveli:\n"+www.text);
        string[] levels = www.text.Split('\n');
        bool good = true;
        foreach (string level in levels)
        {
            if (level == "")
            {
                continue;
            }
            try
            {
                string[] data = level.Split('|');
                Level.AddLevel(Int32.Parse(data[0]), data[1], Int32.Parse(data[2]), Int32.Parse(data[3]));
                //Debug.Log("max order za "+Int32.Parse(data[2])+" je : "+Level.getOrder(Int32.Parse(data[2])));
            }
            catch
            {
                good = false;
                break;
            }
        
        }
        year = good;
        level = good;
        load = false;
        
    }

    private IEnumerator ListYears()
    {
        WWWForm form = new WWWForm();



        WWW www = new WWW(GlobalVariables.LoginURL + "listYears.php", form);
        yield return www;

        string[] result = www.text.Split('\n');

        bool good = true;
        foreach (string line in result)
        {
            if (line == "")
            {
                continue;
            }
            string[] data = line.Split('-');
            try
            {
                Year.AddYear(Int32.Parse(data[0]), data[1]);
            }
            catch
            {
                good = false;
                break;
            }
        }
        year = good;
        load = false;
    }
}
