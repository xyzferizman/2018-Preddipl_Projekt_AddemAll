using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StvaranjePitanja : MonoBehaviour {

    public InputField problem;
    public InputField rjesenje;
    public Dropdown level;
    public Dropdown year;
    public Dropdown strength;
    public Dropdown subject;
    public Button newLevel;
    public InputField inputLevel;
    public Text welcome;
    public InputField answer;
    
    
    private bool createLevel;
    private bool upload;
    private int idOfNewQuest;


    // Use this for initialization
    void Awake () {
        year.AddOptions(Year.getYearsInString());
        ChangeYear();
        inputLevel.gameObject.SetActive(false);
        createLevel = false;
        upload = false;
    }

    public void Start()
    {
        
    }



    // Update is called once per frame
    void Update () {
        if(upload)
        {
            int year = Int32.Parse(this.year.captionText.text.Split('-')[0]);
            int subject = Int32.Parse(this.subject.captionText.text.Split('-')[0]);
            int order = Int32.Parse(this.level.captionText.text.Split('-')[0]);
            Questions.AddTask(new Task(idOfNewQuest, problem.text, (Administrator)GlobalVariables.user, Level.GetLevelId(subject, order),
                Int32.Parse(strength.captionText.text)));
            problem.text = "";
            rjesenje.text = "";
            welcome.text = "Unesite novi zadatak";
            upload = false;

        }
	}

    
    public void Spremi()
    {
        if(this.problem.text != "" && this.year.captionText.text != ""  && this.subject.captionText.text != "")
        {
            if(createLevel)
            {
                if(inputLevel.text != "" && answer.text != "")
                {
                    StartCoroutine(UploadQuest());
                }
                else
                {
                    welcome.text = "Popunite sva polja.";
                }
                
            }
            else
            {
                if (level.captionText.text != "" && answer.text != "") 
                {
                    StartCoroutine(UploadQuest());
                }
                else
                {
                    welcome.text = "Popunite sva polja.";
                }
            }

        } 
    }
    

    

    public void CreateLevel()
    {
        if (year.captionText.text != "")
        {
            level.gameObject.SetActive(false);
            inputLevel.gameObject.SetActive(true);
            createLevel = true;
        }
    }

    public void ReturnOnExistLevel()
    {
        inputLevel.gameObject.SetActive(false);
        level.gameObject.SetActive(true);
        createLevel = false;
    }

   

    public IEnumerator UploadQuest()
    {
        WWWForm form = new WWWForm();
        form.AddField("problem", problem.text);
        if (createLevel)
        {
            form.AddField("level", inputLevel.text);

            form.AddField("order", Level.getOrder(Int32.Parse(subject.captionText.text.Split('-')[0])));
        }
        else
        {
            form.AddField("level", Level.GetLevelId(Int32.Parse(subject.captionText.text.Split('-')[0]), Int32.Parse(level.captionText.text.Split('-')[0])));
            form.AddField("order", 0);//nije bitno
        }
        form.AddField("id_year", Int32.Parse(year.captionText.text.Split('-')[0]));
        form.AddField("strenght", Int32.Parse(strength.captionText.text));
        form.AddField("id_subject", Int32.Parse(subject.captionText.text.Split('-')[0]));
        form.AddField("id_author", GlobalVariables.user.Getid());
        form.AddField("answer", answer.text);


        WWW www = new WWW(GlobalVariables.LoginURL + "insertQuest.php", form);
        yield return www;
        Debug.Log(www.text);
        try
        {
            idOfNewQuest = Int32.Parse(www.text);
        }
        catch
        {
            welcome.text = www.text;
        }
        upload = true;

        
    }

    public void ChangeYear()
    {
        level.ClearOptions();
        if (year.captionText.text != "")
        {
            this.subject.AddOptions(Subject.GetSubjectsForYearInString(Int32.Parse(year.captionText.text.Split('-')[0])));
        }
        ChangeSubject();
    }

    public void ChangeSubject()
    {
        level.ClearOptions();
        if(subject.captionText.text != "")
        {
            this.level.AddOptions(Level.getLevelForSubject(Int32.Parse(subject.captionText.text.Split('-')[0])));
        }
    }

    

    

    public void Nazad()
    {
        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }


}
