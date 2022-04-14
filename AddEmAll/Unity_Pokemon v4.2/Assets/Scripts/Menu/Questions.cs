using System;
using System.Collections.Generic;
using UnityEngine;

public struct IdWithStrenght
{
    public int id;
    public int strenght;
};
public static class Questions
{
    private static Dictionary<IdWithStrenght, List<Task>> levels = null;
    private static Dictionary<int, List<Task>> professor = null;
    private static List<Task> questions = new List<Task>();

    public static List<Task> quests
    {
        get
        {
            return new List<Task>(questions);
        }
        
    }

    internal static void AddQuestions(string text)
    {
        string[] tasks = text.Split('\n');
        foreach (string task in tasks)
        {
            if(task == "")
            {
                continue;
            }
            string[] data = task.Split('|');

            Administrator author = new Administrator(Int32.Parse(data[4]), data[5], data[6], data[7]);
            Task t = null;
            foreach (Task question in questions)
            {
                if (question.Id == Int32.Parse(data[0]))
                    {
                        t = question;
                        break;
                    }
            }
            if (t == null)
            {
                t = new Task(Int32.Parse(data[0]), data[1], author, Int32.Parse(data[2]), Int32.Parse(data[3]));
                questions.Add(t);
            }
            if (data[9] == "0")
            {
                t.AddIncorrect(data[8]);
            }
            else
            {
                t.AddCorrect(data[8]);
            }
        }
    }

    public static List<Task> getListForProffesor(int id)
    {
        if(professor == null)
        {
            professor = new Dictionary<int, List<Task>>();
            foreach (Task quest in questions)
            {
                if (professor.ContainsKey(quest.Id))
                {
                    professor.Add(quest.Id, new List<Task>());
                }
                professor[quest.Id].Add(quest);
            }
        }
        return new List<Task>( professor[id]);


    }

    public  static List<Task> getListQuestion(int subject, int level, int strenght)
    {
        int idLevel = Level.GetLevelId(subject, level);
        if(idLevel == 0)
        {
            return null;
        }
        if(levels == null)
        {
            levels = new Dictionary<IdWithStrenght, List<Task>>();
            foreach (Task quest in questions)
            {
                IdWithStrenght iwith;
                iwith.id = quest.Level;
                iwith.strenght = quest.Strenght;

                //Debug.Log(iwith.id + " " + iwith.strenght);
                if (!levels.ContainsKey(iwith))
                {
                    levels.Add(iwith, new List<Task>());
                }
                levels[iwith].Add(quest);
            }
        }
        IdWithStrenght iw;
        iw.id = idLevel;
        iw.strenght = strenght;
        //Debug.Log(iw.id + " " + iw.strenght);
        if (levels.ContainsKey(iw))
        {
            return new List<Task>(levels[iw]);
        }
        return null;
    }

    internal static void AddTask(Task task)
    {
        if(professor != null)
        {
            if(professor.ContainsKey(task.Author.Getid()))
            {
                professor[task.Author.Getid()].Add(task);
            }
            else
            {
                professor.Add(task.Author.Getid(), new List<Task>() { task });
            }
        }
        if(levels != null)
        {
            IdWithStrenght iwith;
            iwith.id = task.Level;
            iwith.strenght = task.Strenght;
            if (levels.ContainsKey(iwith))
            {
                levels[iwith].Add(task);
            }
            else
            {
                levels.Add(iwith, new List<Task>() { task });
            }
        }
        questions.Add(task);
        
    }
    

}

