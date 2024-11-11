namespace FinanceTracker;

public class User{
    private string? name = "none";
    private int age = 0;
    public string? Name
    {
        get{ return name; }
        set {  name = value; }
    }
    public int Age
    { 
        get { return age; } 
        set { age = value; } 
    }

    public User(string? name, int age){
        this.Name = name;
        this.Age = age;
    }
}