

using System;
using System.Collections.Generic;

public class Player : User {
    private int _year;
    private int _expirience;
    private Dictionary<int, int> correct = new Dictionary<int, int>();
    private Dictionary<int, int> total = new Dictionary<int, int>();
    private Dictionary<int, int> subjectState = new Dictionary<int, int>();
    private Dictionary<Level, int> save = new Dictionary<Level, int>();

    public Player(int id,string name, string surname, string email,int expirience, int year):base(id, name, surname, email)
    {
        this._year = year;
    }

    
    public int year
    {
        get { return _year; }
        set { _year = value; }
    }

    public int Expirience
    {
        get
        {
            return _expirience;
        }
        set
        {
            if(value>_expirience)
            {
                _expirience = value;
            }
        }
        
    }

    

    public override bool isAdministrator()
    {
        return false;
    }

    public void SavePosition(int subject, int level, int strenght)
    {
        save[Level.getLevel(subject, level)] = strenght;
    }

    internal void AddStatistics(string text)
    {
        string[] lines = text.Split('\n');
        foreach (string line in lines)
        {
            if (line == "")
            {
                continue;
            }
            string[] data = line.Split('|');
            bool correct = data[12] != data[13] ? false : true;
            int level = Int32.Parse(data[15]);
            if (correct)
            {

                if (this.correct.ContainsKey(level))
                {
                    this.correct[level]++;
                }
                else
                {
                    this.correct[level] = 1;
                }

            }
            if (total.ContainsKey(level))
            {
                total[level]++;
            }
            else
            {
                total[level] = 1;
            }
        }

    }

    public string GetStatistics(int level)
    {
        int cor = correct.ContainsKey(level) ?  correct[level]:0;
        int tot = total.ContainsKey(level) ? total[level]:0; 
        return cor + "/" + tot;
    }

    public void AddCorrect(int level)
    {
        if(!correct.ContainsKey(level))
        {
            correct[level] = 0;
        }
        if(!total.ContainsKey(level))
        {
            total[level] = 0;
        }
        correct[level]+=1;
        total[level]++;
    }

    public void AddIncorrect(int level)
    {
        total[level]++;
    }

    public int getStrenght(int subject,int level)
    {
        if(save.ContainsKey(Level.getLevel(subject, level)))
        {
            return save[Level.getLevel(subject, level)];
        }
        else
        {
            save[Level.getLevel(subject, level)] = 1;
            return 1;
        }
       
    }

    public int getOrder(int subject)
    {
        if (subjectState.ContainsKey(subject))
        {
            return subjectState[subject];
        }
        else
        {
            subjectState[subject] = 1;
            return 1;
        }
    }


    public void AddPositionOfSubject(int subject, int order)
    {
        if (subjectState.ContainsKey(subject))
        {
            if (subjectState[subject] < order)
            {
                subjectState[subject] = order;
            }
        }
        else
        {
            subjectState[subject] = order;
        }
    }

    public void AddStrenghtOfLevel(int order,int subject, int strenght)

    {
        if (save.ContainsKey(Level.getLevel(subject, order)))
        {
            if (save[Level.getLevel(subject, order)] <= strenght)
            {
                save[Level.getLevel(subject, order)] = strenght;
            }
        }
        else
        {
            save[Level.getLevel(subject, order)] = strenght;
        }
    }
}
