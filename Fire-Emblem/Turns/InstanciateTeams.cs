namespace Fire_Emblem.Turns;

using Fire_Emblem_View;
using Fire_Emblem.Characters;

public class InstanciateTeams
{
    private List<Character> _characterInstances;
    private Dictionary<string, string> _firstPlayerPick;
    private Dictionary<string, string> _secondPlayerPick;
    private Dictionary<Character, string> _firstPlayerTeam;
    private Dictionary<Character, string> _secondPlayerTeam;

    public InstanciateTeams(List<Dictionary<string, string>> playersPicks)
    {
        _characterInstances = ReadCharactersJson.ReadJson();
        _firstPlayerPick = playersPicks[0];
        _secondPlayerPick = playersPicks[1];
    }

    public void AssignTeams()
    {
        _firstPlayerTeam = AssignCharacters(_firstPlayerPick);
        _secondPlayerTeam = AssignCharacters(_secondPlayerPick);
    }

    private Dictionary<Character, string> AssignCharacters(Dictionary<string, string> selectedTeam)
    {
        Dictionary<Character, string> assignedTeam = new Dictionary<Character, string>();

        foreach (var kvp in selectedTeam)
        {
            string characterName = kvp.Key;
            string skill = kvp.Value;
            
            Character character = _characterInstances.FirstOrDefault(c => c.Name == characterName);

            Character newAddition = new Character(name: character.Name, weapon: character.Weapon,
                gender: character.Gender, deathQuote: character.DeathQuote, hp: character.HP, atk: character.Atk,
                spd: character.Spd, def: character.Def, res: character.Res);
                
            assignedTeam[newAddition] = skill;

        }

        return assignedTeam;
    }

    public Dictionary<Character, string> GetFirstPlayerTeam()
    {
        return _firstPlayerTeam;
    }

    public Dictionary<Character, string> GetSecondPlayerTeam()
    {
        return _secondPlayerTeam;
    }
    
}