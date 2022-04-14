using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AdministratorMenu : MonoBehaviour {

    Administrator admin = (Administrator)GlobalVariables.user;
    bool upload = false;
    Button[] buttons;

    public void Start()
    {
        buttons = GetComponentsInChildren<Button>();
        foreach (Button button in buttons)
        {
            button.interactable = false;
        }

        upload = true;
        StartCoroutine(LoadSubjects());
    }

    public void Update()
    {
        
    }

    public void DodajPitanjaScena()
    {
        SceneManager.LoadScene(5);
    }

    public void StatistikaScena()
    {
        SceneManager.LoadScene(6);
    }

    public void LoadLevels()
    {
        SceneManager.LoadScene(8);
    }

    public void LogOut()
    {
        SceneManager.LoadScene(0);
    }


    private IEnumerator LoadSubjects()
    {
        WWWForm form = new WWWForm();
        form.AddField("admin_id", admin.Getid());



        WWW www = new WWW(GlobalVariables.LoginURL + "authorities.php", form);
        yield return www;
        string[] lines = www.text.Split('\n');
        foreach (string line in lines)
        {
            if (line == "")
            {
                continue;
            }
            admin.addSubject(Int32.Parse(line));
            
        }
        upload = false;
        foreach (Button button in buttons)
        {
            button.interactable = true;
        }



    }
}
