
public abstract class User {
    private int id;
    private string name;
    private string surname;
    private string email;
    private int year;

    public User(int id, string name, string surname, string email)
    {
        this.id = id;
        this.name = name;
        this.surname = surname;
        this.email = email;
    }

    public int Getid()
    {
        return id;
    }
    public string getName()
    {
        return name;
    }
    public string getSurname()
    {
        return surname;
    }
    public string getemail()
    {
        return email;
    }
   
    
    public void SetName(string name)
    {
        if (!name.Equals(null))
            this.name = name;
    }

    public void setSurname(string surname)
    {
        if (!surname.Equals(null))
            this.surname = surname;
    }

    public void setEmail(string email)
    {
        if (email.Contains("@"))
        {
            this.email = email;
        }

    }

    public abstract bool isAdministrator();
    

}
