namespace Fire_Emblem.Characters;

public class Skills
{
    public string Name { get; set; }
    public string Description { get; set; }
    
    public Skills(string name, string description)
    {
        Name = name;
        Description = description;
    }
}