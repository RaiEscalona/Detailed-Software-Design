using System.Text.RegularExpressions;
using Fire_Emblem_View;

namespace Fire_Emblem.Turns;
using Fire_Emblem.Characters;
using System;

public class InitializeGame
{
    private View _view;
    private string _file;
    private int _chosenTeam;
    private string _pattern;
    private bool _isPlayable;
    private List<string> _characters;
    private Dictionary<string, string> _FirstPlayerTeam;
    private Dictionary<string, string> _SecondPlayerTeam;
    private List<string> _filesName;

    public InitializeGame(View view, string file)
    {
        _characters = ReadFiles.ReadTeams(file);
        _view = view;
        _file = file;
        _isPlayable = true;
        _FirstPlayerTeam = new Dictionary<string, string>();
        _SecondPlayerTeam = new Dictionary<string, string>();
        this.ShowTeams();
        this.AskForInput();
        this.CheckTeamsErrors();
    }

    private void CheckTeamsErrors()
    {
        HandleTeamExceptions checkingFirstTeam = new HandleTeamExceptions(_FirstPlayerTeam);
        checkingFirstTeam.CheckErrors();
        bool isFirstTeamGood = checkingFirstTeam.GetViability();
        HandleTeamExceptions checkingSecondTeam = new HandleTeamExceptions(_SecondPlayerTeam);
        checkingSecondTeam.CheckErrors();
        bool isSecondTeamGood = checkingSecondTeam.GetViability();
        if (!isFirstTeamGood || !isSecondTeamGood)
        {
            _isPlayable = false;
        }
    }

    public bool GetGamePlayability()
    {
        if (!_isPlayable)
        {
            _view.WriteLine("Archivo de equipos no válido");
        }
        return _isPlayable;
    }

    private void ShowTeams()
    {
        _view.WriteLine("Elige un archivo para cargar los equipos");
        _filesName = ReadFiles.ReadTeams(_file);
        int i = 0;
        foreach (var file in _filesName)
        {
            _view.WriteLine($"{i}: {file}");
            i++;
        }
    }

    private void AskForInput()
    {
        string input = _view.ReadLine();
        if (int.TryParse(input, out _chosenTeam))
        {
            this.AssignTeam(_chosenTeam);
        }
    }
    

    private void AssignTeam(int chosenTeam)
    {
        int i = 0;
        foreach (var file in _filesName)
        {
            if (i == chosenTeam)
            {
                string searchedFile = Path.Combine(_file, file);
                string content = File.ReadAllText(searchedFile); 
                ReadPropertiesCharacters(content);
            }
            i++;
        }
    }

    private void ReadPropertiesCharacters(string content)
    {
        _pattern = @"([^\(\)]+)\s*(?:\(([^)]+)\))?";
        // Split the content in lines and go through each one
        string[] lines = content.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        bool isPlayerOneTeam = true;
        foreach (string line in lines)
        {
            if (line.Contains("Player 1 Team"))
            {
                isPlayerOneTeam = true;
            }
            else if (line.Contains("Player 2 Team"))
            {
                isPlayerOneTeam = false;
            }
            else
            {
                if (isPlayerOneTeam)
                {
                    this.AssignTeamToPlayer(_FirstPlayerTeam, line);
                }
                else
                {
                    this.AssignTeamToPlayer(_SecondPlayerTeam, line);
                }
            }
        }
    }

    private void AssignTeamToPlayer(Dictionary<string, string> nameSkillDict, string line)
    {
        var match = Regex.Match(line, _pattern);
        string name = match.Groups[1].Value.Trim();
        string skills = match.Groups[2].Success ? match.Groups[2].Value : "No skill assigned";
        if (nameSkillDict.ContainsKey(name))
        {
            _isPlayable = false;
        }
        else
        {
            nameSkillDict[name] = skills;   
        }
    }

    public List<Dictionary<string, string>> GetPlayersTeams()
    {
        List<Dictionary<string, string>> playersTeams = new List<Dictionary<string, string>>();
        playersTeams.Add(_FirstPlayerTeam);
        playersTeams.Add(_SecondPlayerTeam);
        return playersTeams;
    }
}