using Fire_Emblem_View;
using Fire_Emblem.Attacks;
using Fire_Emblem.Characters;

namespace Fire_Emblem.Turns;

public class StartFight
{
    private Dictionary<Character, string> _attackerTeam;
    private Dictionary<Character, string> _defenderTeam;
    private PickedFighterPlayer _pickAtacker;
    private PickedFighterPlayer _pickDefender;
    private KeyValuePair<Character, string> _attacker;
    private KeyValuePair<Character, string> _defender;
    private TriangleAdvantage _triangleAdvantage;
    private View _view;
    private int _turn;
    private int _round;
    private Character _harmed;
    private string _harmedTeam; // Hacer algo mejor
    private bool _isCharacterAlive;
    private bool _isGameOver;
    private int _player;
    private double _WTBAttacker;
    private double _WTBDefender;
    private int _written;
    private int _numberPlayerAttacking;

    public StartFight(Dictionary<Character, string> firstPlayerTeam, Dictionary<Character, string> secondPlayerTeam, View view)
    {
        _attackerTeam = firstPlayerTeam;
        _defenderTeam = secondPlayerTeam;
        _view = view;
        _turn = 1;
        _round = 1;
        _written = 0; // Arreglar esta variable y hacer algo decente después
        _numberPlayerAttacking = 1; // Hacer algo más bonito segunda entrega
        _isGameOver = false;
        _triangleAdvantage = new TriangleAdvantage();
    }

    public void FightSequence()
    {
        while (!_isGameOver)
        {
            this.SimulateRound();
            this.CheckGameOver();
        }
    }

    private void SimulateRound()
    {
        _isCharacterAlive = false;
        this.PickFighters();
        this.BeginRound();
        this.StartAttack();
        if (CheckCharacterDead()) return;
        this.CounterAttack();
        if (CheckCharacterDead()) return;
        this.CheckFollowUp();
        if (CheckCharacterDead()) return;
        this.PrintEndFightSequence();
        this.ChangeTurn();
        _round++;
    }
    
    private void CheckGameOver()
    {
        bool allDead = true;
        foreach (var kvp in _attackerTeam)
        {
            if (kvp.Key.HP != "0")
            {
                allDead = false;
                break;
            }
        }
        if (allDead)
        {
            if (_numberPlayerAttacking == 1 && _defenderTeam.Count == 0)
            {
                _view.WriteLine("Player 1 ganó");
            }
            else if (_numberPlayerAttacking == 2 && _defenderTeam.Count == 0)
            {
                _view.WriteLine("Player 2 ganó");
            }
            else if (_numberPlayerAttacking == 1 && _attackerTeam.Count == 0)
            {
                _view.WriteLine("Player 2 ganó");
            }
            else if (_numberPlayerAttacking == 2 && _attackerTeam.Count == 0)
            {
                _view.WriteLine("Player 1 ganó");
            }
            _isGameOver = true;
            return;
        }
        allDead = true;
        
        foreach (var kvp in _defenderTeam)
        {
            if (kvp.Key.HP != "0")
            {
                allDead = false;
                break;
            }
        }

        if (allDead)
        {
            if (_numberPlayerAttacking == 1 && _defenderTeam.Count == 0)
            {
                _view.WriteLine("Player 1 ganó");
            }
            else if (_numberPlayerAttacking == 2 && _defenderTeam.Count == 0)
            {
                _view.WriteLine("Player 2 ganó");
            }
            else if (_numberPlayerAttacking == 1 && _attackerTeam.Count == 0)
            {
                _view.WriteLine("Player 2 ganó");
            }
            else if (_numberPlayerAttacking == 2 && _attackerTeam.Count == 0)
            {
                _view.WriteLine("Player 1 ganó");
            }
            _isGameOver = true;
            return;
        }
    }

    private bool CheckCharacterDead()
    {
        _isCharacterAlive = this.CheckHealth();
        if (!_isCharacterAlive)
        {
            if (_harmedTeam == "attacker")
            {
                _attackerTeam.Remove(_attacker.Key);
            }
            else
            {
                _defenderTeam.Remove(_defender.Key);
            }
            this.PrintEndFightSequence();
            this.ChangeTurn();
            _round++;
            return true;
        }
        return false;
    }

    private void PrintTurn()
    {
        this.CalculatePlayer();
        if (_written % 4 == 0 || _written % 4 == 3)
        {
            //_view.WriteLine($"Este es el turno {_turn} y este es el jugador {_player}");
            _view.WriteLine("Player 1 selecciona una opción");
            _written++;
        }
        else
        {
            //_view.WriteLine($"Este es el turno {_turn} y este es el jugador {_player}");
            _view.WriteLine("Player 2 selecciona una opción");
            _written++;
        }
    }

    private void PickFighters()
    {
        _pickAtacker = new PickedFighterPlayer(_attackerTeam, _view);
        _pickDefender = new PickedFighterPlayer(_defenderTeam, _view);
        this.PrintTurn();
        _attacker = _pickAtacker.PickedFighter();
        this.PrintTurn();
        _defender = _pickDefender.PickedFighter();
    }

    private void StartAttack()
    {
        GeneralAttack generalAttack = new GeneralAttack(_attacker, _defender, _round, _view, _WTBAttacker);
        generalAttack.AttackSequence();
        _harmed = _defender.Key;
        _harmedTeam = "defender";
    }
    
    private void ChangeTurn()
    {
        (_attackerTeam, _defenderTeam) = (_defenderTeam, _attackerTeam);
        if (_numberPlayerAttacking == 1)
        {
            _numberPlayerAttacking = 2;
        }
        else
        {
            _numberPlayerAttacking = 1;
        }
        if (_turn == 1)
        {
            _turn = 2;
        }
        else
        {
            _turn = 1;
        }
    }

    private bool CheckHealth()
    {
        if (_harmed.HP == "0")
        {
            return false;
        }
        return true;
    }

    private void CounterAttack()
    {
        GeneralAttack counterAttack = new GeneralAttack(_defender, _attacker, _round, _view, _WTBDefender);
        counterAttack.AttackSequence();
        _harmed = _attacker.Key;
        _harmedTeam = "attacker";
    }

    private void CheckFollowUp()
    {
        FollowUp followUp = new FollowUp(_attacker.Key, _defender.Key, _view);
        string condition = followUp.WhoFollowsUp();
        if (condition == "attacker")
        {
            this.StartAttack();
        }
        else if (condition == "defender")
        {
            this.CounterAttack();
        }
        else
        {
            _view.WriteLine("Ninguna unidad puede hacer un follow up");
        }
    }

    private void PrintEndFightSequence()
    {
        _view.WriteLine($"{_attacker.Key.Name} ({_attacker.Key.HP}) : {_defender.Key.Name} ({_defender.Key.HP})");
    }
    
    private void BeginRound()
    {
        this.CalculatePlayer();
        _view.WriteLine($"Round {_round}: {_attacker.Key.Name} (Player {_player}) comienza");
        _WTBAttacker = _triangleAdvantage.CalculateAdvantage(_attacker.Key, _defender.Key, _view);
        _WTBDefender = 2 - _WTBAttacker;
    }
    
    private void CalculatePlayer()
    {
        if (_round % 2 == 0)
        {
            _player = 2;
        }
        else
        {
            _player = 1;
        }
    }
    
}