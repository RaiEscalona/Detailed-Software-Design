namespace Fire_Emblem.Turns;
using Fire_Emblem_View;
using Fire_Emblem.Characters;

public class PickedFighterPlayer
{
    private Dictionary<Character, string> _playerTeam;
    private View _view;
    
    public PickedFighterPlayer(Dictionary<Character, string> playerTeam, View view)
    {
        _playerTeam = playerTeam;
        _view = view;
    }
    
    public KeyValuePair<Character, string> PickedFighter()
    {
        ShowCharacters();
        
        string input = _view.ReadLine();
        if (int.TryParse(input, out int chosenCharacter))
        {
            var selectedCharacter = _playerTeam.ElementAt(chosenCharacter);
            return selectedCharacter;
        }
        _view.WriteLine("Invalid selection. Please try again.");
        return PickedFighter();
    }
    
    private void ShowCharacters()
    {
        int i = 0;
        foreach (var character in _playerTeam.Keys)
        {
            int hp;
            int.TryParse(character.HP, out hp);
            if (hp <= 0) continue;
            _view.WriteLine($"{i}: {character.Name}");
            i++;
        }
    }
}