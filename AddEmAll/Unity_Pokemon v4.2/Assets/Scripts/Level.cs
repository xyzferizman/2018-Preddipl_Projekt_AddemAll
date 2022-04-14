using System.Collections.Generic;

struct Order
{
    public int subject;
    public int order;
}



internal class Level
{
    private static Dictionary<Order, Level> values = new Dictionary<Order, Level>();
    private static Dictionary<int, int> maxOrder = new Dictionary<int, int>();
    private int _id;
    private string _name;
    public Level(int id, string name)
    {
        _id = id;
        _name = name;
    }

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

    public static void AddLevel(int id, string name, int subject)
    {
        Order o;
        o.subject = subject;
        if (maxOrder.ContainsKey(subject))
        {
            o.order =maxOrder[subject]++;
        }
        else
        {
            maxOrder.Add(subject, 1);
            o.order = 1;
        }
        values.Add(o, new Level(id, name));
    }
    public static void AddLevel(int id, string name, int subject, int order)
    {
        Order o;
        o.subject = subject;
        if(GetLevelId(subject, order) != 0)
        {
            return;
        }
        if (maxOrder.ContainsKey(subject))
        {
            if(maxOrder[subject]<order)
            {
                maxOrder[subject] = order;
            }
        }
        else
        {
            maxOrder[subject] = order;
        }
        o.order = order;
        if (!values.ContainsKey(o))
        {
            values.Add(o, new Level(id, name));
        }

    }

    public static Level getLevel(int subject, int order)
    {
        Order o;
        o.subject = subject;
        o.order = order;
        if(values.ContainsKey(o))
        {
            return values[o];
        }
        return null;
    }


    public static int GetLevelId(int subject, int order)
    {
        Order o;
        o.subject = subject;
        o.order = order;
        if (values.ContainsKey(o))
        {

            return values[o].Id;
        }
        return 0;
    }

    public static int getOrder(int subject)
    {
        return maxOrder[subject]++;
    }

    public static List<string> getLevelForSubject(int subject, int max)
    {
        List<string> result = new List<string>();
        if(subject == 0)
        {
            return null;
        }
        Order o;
        o.subject = subject;
        o.order = 1;
        
        while (o.order<=max)
        {                
                Level l = values[o];
                result.Add(o.order+"-"+l._name);
                o.order++;
        }

        return result;
    }

    public static List<string> getLevelForSubject(int subject)
    {
        List<string> result = new List<string>();
        if (subject == 0)
        {
            return null;
        }
        Order o;
        o.subject = subject;
        o.order = 1;

        while (true)
        {
            try
            {

                Level l = values[o];
                result.Add(o.order + "-" + l._name);
                o.order++;
            }
            catch
            {
                break;
            }
        }

        return result;
    }

    public static void change(int subject, int order1, int order2)
    {
        Order ord1 = new Order { subject = subject, order = order1 };
        Order ord2 = new Order { subject = subject, order = order2 };
        Level level1 = values[ord1];
        values[ord1] = values[ord2];
        values[ord2] = level1;
    }

}


    
