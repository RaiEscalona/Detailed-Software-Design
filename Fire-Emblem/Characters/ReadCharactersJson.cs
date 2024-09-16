namespace Fire_Emblem.Characters;
using System.Text.Json;
using Fire_Emblem_View;

public class ReadCharactersJson
{
    public static List<Character> ReadJson()
    {
        string myJson = File.ReadAllText("characters.json");
        var characters = JsonSerializer.Deserialize<List<Character>>(myJson);
        return characters;
    }

}