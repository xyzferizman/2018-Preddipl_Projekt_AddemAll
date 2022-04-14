using System;
using System.Collections.Generic;

public class Task {
    private int _id;
    private string _problem;
    private string _correct = null;
    private List<string> _incorrect = null;
    private Administrator _author;
    private int _level;
    private int _strenght;



    public Task(int id, string problem, Administrator author, int level, int strenght)
    {
        _id = id;
        _problem = problem;
        _author = author;
        _level = level;
        _strenght = strenght;
    }

    public int Id
    {
        get
        {
            return _id;
        }

        
    }

    public int Level
    {
        get
        {
            return _level;
        }

    }

    public int Strenght
    {
        get
        {
            return _strenght;
        }
        
    }

    public Administrator Author
    {
        get
        {
            return _author;
        }

    }

    public string Problem
    {
        get
        {
            return _problem;
        }

    }

    public void AddIncorrect(string v)
    {
        if(_incorrect == null)
        {
            _incorrect = new List<string>();
        }
        _incorrect.Add(v);
    }
    public void AddCorrect(string s)
    {
        _correct = s;
    }

    public bool isCorrectAnswer(string s)
    {
        return (s == _correct);
    }
}
