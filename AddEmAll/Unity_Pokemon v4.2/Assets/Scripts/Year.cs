using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Year
{
    private int _id;
    private string _name;
    private static List<Year> listOfYears = null;

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

    private Year(int id, string name)
    {
        this._id = id;
        this._name = name;
    }

    public static void AddYear(int id, string name)
    {
        if(listOfYears == null)
        {
            listOfYears = new List<Year>();
        }
        if(GetYear(id) != null || GetYear(name) != null)
        {
            return;
        }
        listOfYears.Add(new Year(id, name));
    }

    public static Year GetYear(int id)
    {
        foreach (Year year in listOfYears)
        {
            if(year.Id == id)
            {
                return year;
            }
        }
        return null;
    }
    public static Year GetYear(string name)
    {
        foreach(Year year in listOfYears)
        {
            if(year.Name == name)
            {
                return year;
            }
        }
        return null;
    }
    override
    public string ToString()
    {
        return Id + "-" + Name;
    }

    public static List<Year> getYears()
    {
        return new List<Year>(listOfYears);
    }

    public static List<string> getYearsInString()
    {
        List<string> result = new List<string>();
        foreach(Year year in listOfYears)
        {
            result.Add(year.ToString());
        }
        return result;
    }
}
