using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Administrator : User {

    private List<Subject> listSubjects;

    public Administrator(int id, string name, string surname, string email) : base(id, name, surname, email)
    {
        listSubjects = new List<Subject>();
    }

    


    public override bool isAdministrator()
    {
        return true;
    }


    public void addSubject(int id)
    {
        listSubjects.Add(Subject.GetSubject(id));
    }

    public List<string> getSubjects(bool year)
    {
        List<string> result = new List<string>();
        foreach(Subject subject in listSubjects)
        {
            result.Add(subject.ToString(year));
        }
        return result;
    }
}
