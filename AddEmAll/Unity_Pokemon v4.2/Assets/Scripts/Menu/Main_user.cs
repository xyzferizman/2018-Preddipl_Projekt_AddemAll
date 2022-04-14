using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class Main_user : MonoBehaviour {
    public Text welcome;
    public Dropdown year;
    public Dropdown subject;
    public Dropdown level;
    public InputField expirience;
    public InputField statistics;
    private Player user;
    private bool statisticsdone;
    private bool upload;




    // Use this for initialization
    void Awake () {
        user = (Player)GlobalVariables.user;
        welcome.text = "Dobrodošao, " + user.getName() + " " + user.getSurname();

        year.AddOptions(Year.getYearsInString());
        if (Year.getYearsInString() == null)
        {
            int position = Year.getYears().IndexOf(Year.GetYear(user.year));
            year.value = position;
        }
        ChangeYear();
        expirience.text = user.Expirience.ToString();

        statisticsdone = false;

        StartCoroutine(Statistics());
        StartCoroutine(LoadPositions());

    }

    

    public void ChangeYear()
    {
        upload = true;
        subject.ClearOptions();
        if (year.captionText.text != "")
        {
            subject.AddOptions(Subject.GetSubjectsForYearInString(Int32.Parse(year.captionText.text.Split('-')[0])));
        }
        changeSubject();
    }

    public void changeSubject()
    {
        upload = true;
        level.ClearOptions();
        if(subject.captionText.text != "")
        {
            level.AddOptions(Level.getLevelForSubject(Int32.Parse(subject.captionText.text.Split('-')[0]),user.getOrder(Int32.Parse(subject.captionText.text.Split('-')[0]))));
        }
        upload = false;
    }

    

    // Update is called once per frame
    public void Update () {
    }



    public void Play()
    {
        if (!upload&&!statisticsdone)
        {
            int year = Int32.Parse(this.year.captionText.text.Split('-')[0]);
            int order = Int32.Parse(level.captionText.text.Split('-')[0]);
            GlobalVariables.year = year;
            GlobalVariables.subject = Subject.GetSubject(year).Id;
            GlobalVariables.order = order;
            GlobalVariables.strenght = user.getStrenght(GlobalVariables.subject, order);
            GlobalVariables.lastQuestCompleted = GlobalVariables.strenght - 1;
            SceneManager.LoadScene(7);
        }
    }

    public void LogOut()
    {
        if (!upload)
        {
            StartCoroutine(ConnectWithDataBase());
            
        }

    }


    private IEnumerator ConnectWithDataBase()
    {
        DateTime start = GlobalVariables.start;
        TimeSpan duration = DateTime.Now - start;
        WWWForm form = new WWWForm();
        form.AddField("id", user.Getid());
        form.AddField("start", GlobalVariables.start.ToString("yyyy-MM-dd HH:mm:ss"));
        form.AddField("duration", duration.ToString());


        WWW www = new WWW(GlobalVariables.LoginURL + "insertLog.php", form);
        yield return www;

        SceneManager.LoadScene(0);


    }

    private IEnumerator Statistics()
    {
        statisticsdone = true;
        WWWForm form = new WWWForm();
        form.AddField( "user_id" , user.Getid());
        form.AddField("admin_id", 0);


        WWW www = new WWW(GlobalVariables.LoginURL + "statistics.php", form);
        yield return www;
        //Debug.Log(www.text);
        user.AddStatistics(www.text);

        

        if (year.captionText.text == "")
        {
            statistics.text = "0/0";
        }
        else 
        {
            statistics.text = user.GetStatistics(Level.GetLevelId(Int32.Parse(subject.captionText.text.Split('-')[0]), Int32.Parse(level.captionText.text.Split('-')[0])));
        }
        statisticsdone = false;


    }

    private IEnumerator LoadPositions()
    {
        WWWForm form = new WWWForm();
        form.AddField("user", user.Getid());
        form.AddField("expirience", user.Expirience);
        form.AddField("subject", GlobalVariables.subject);
        form.AddField("order", GlobalVariables.order);
        form.AddField("strenght", GlobalVariables.strenght);


        WWW www = new WWW(GlobalVariables.LoginURL + "Position.php", form);
        yield return www;
        Debug.Log("upload: " + www.text);
        try
        {
            string[] lines = www.text.Split('\n');
            foreach (string line in lines)
            {
                if (line == "")
                {
                    continue;
                }
                string[] data = line.Split('|');
                user.Expirience = Int32.Parse(data[4]);

                expirience.text = data[4];
                user.AddPositionOfSubject(Int32.Parse(data[1]), Int32.Parse(data[2]));
                user.AddStrenghtOfLevel(Int32.Parse(data[2]), Int32.Parse(data[1]), Int32.Parse(data[3]));
                changeSubject();
            }
        }
        catch
        {
        }
        
        
    }


}
