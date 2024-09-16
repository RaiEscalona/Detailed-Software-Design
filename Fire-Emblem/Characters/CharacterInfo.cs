namespace Fire_Emblem.Characters;

public class Character
{
    public string Name { get; set; }
    public string Weapon { get; set; }
    public string Gender { get; set; }
    public string DeathQuote { get; set; }
    public string HP { get; set; }
    public string Atk { get; set; }
    public string Spd { get; set; }
    public string Def { get; set; }
    public string Res { get; set; }

    public Character(string name, string weapon, string gender, string deathQuote, string hp, string atk, string spd, string def, string res)
    {
        Name = name;
        Weapon = weapon;
        Gender = gender;
        DeathQuote = deathQuote;
        HP = hp;
        Atk = atk;
        Spd = spd;
        Def = def;
        Res = res;
    }
}
