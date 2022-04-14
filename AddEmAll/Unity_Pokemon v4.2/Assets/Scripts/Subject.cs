using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subject
{
    private int _id;
    private string _name;
    private Year year;

    private static Dictionary<int, List<Subject>> listSubjects = new Dictionary<int, List<Subject>>();

    public int Id
    {
        get
        {
            return _id;
        }
        
    }

    

    public string Name
    {
        get
        {
            return _name;
        }
        
    }

    public Year Year
    {
        get
        {
            return year;
        }

        
    }

    private Subject(int id, string name, int id_year)
    {
        _id = id;
        _name = name;
        year = Year.GetYear(id_year);

    }

    public static void AddSubject(int id, string name, int id_year)
    {
        if (!listSubjects.ContainsKey(id_year))
        {
            listSubjects.Add(id_year, new List<Subject>());
        }
        if (GetSubject(id) == null)
        {
            listSubjects[id_year].Add(new Subject(id, name, id_year));

        }

    }

    internal static List<string> getSubjectForAdministrator(int v)
    {
        throw new NotImplementedException();
    }

    public static List<Subject> GetSubjectsForYear(int id_year)
    {
        return listSubjects[id_year];
    }

    public static Subject GetSubject(int id)
    {
        foreach(List<Subject> list in listSubjects.Values)
        {
            foreach(Subject s in list)
            {
                if(s.Id  == id)
                {
                    return s;
                }
            }
        }
        return null;
    }

    public static List<string> GetSubjectsForYearInString(int id)
    {
        List<string> result = new List<string>();
        foreach(Subject subject in listSubjects[id])
        {
            result.Add(subject.ToString());
        }
        return result;
    }
    override
    public string ToString()
    {
        return Id + "-" + Name;
    }

    public string ToString(bool year)
    {
        if (year)
        {
            return Id+ "-"+ Name + "-" + Year.Name;
        }
        return ToString();
    }



}
