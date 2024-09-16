using Fire_Emblem_View;
using Fire_Emblem.Characters;
using Fire_Emblem.Turns;

namespace Fire_Emblem;

public class Game
{
    private View _view;
    private string _teamsFolder;
    
    public Game(View view, string teamsFolder)
    {
        _view = view;
        _teamsFolder = teamsFolder;
    }

    public void Play()
    { 
        InitializeGame initializeGame = new InitializeGame(_view, _teamsFolder);
        if (initializeGame.GetGamePlayability())
        {
            InstanciateTeams instanciateTeams = new InstanciateTeams(initializeGame.GetPlayersTeams());
            instanciateTeams.AssignTeams();
            Dictionary<Character, String> firstPlayerTeam = instanciateTeams.GetFirstPlayerTeam();
            Dictionary<Character, String> secondPlayerTeam = instanciateTeams.GetSecondPlayerTeam();
            StartFight startFight = new StartFight(firstPlayerTeam, secondPlayerTeam, _view);
            startFight.FightSequence();
        }
    }
}
