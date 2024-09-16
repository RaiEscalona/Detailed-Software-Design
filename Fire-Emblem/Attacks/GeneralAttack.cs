using Fire_Emblem_View;
using Fire_Emblem.Characters;

namespace Fire_Emblem.Attacks;

public class GeneralAttack
{
    private Character _attacker;
    private Character _defender;
    private TriangleAdvantage _triangleAdvantage;
    private CalculateDamage _calculateDamage;
    private View _view;
    private int _round;
    private int _player;
    private double _WTB;
    
    public GeneralAttack(KeyValuePair<Character, string> attacker, KeyValuePair<Character, string> defender, int round, View view, double wtb)
    {
        _attacker = attacker.Key;
        _defender = defender.Key;
        _round = round;
        _view = view;
        _WTB = wtb;
        _triangleAdvantage = new TriangleAdvantage();
    }

    public void AttackSequence()
    {
        this.CalculateAttack();
        this.CombatEnd();
    }

    /*
    private void AnounceAttack()
    {
        this.CalculatePlayer();
        _view.WriteLine($"Round {_round}: {_attacker.Name} (Player {_player}) comienza");
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
    */

    private void CalculateAttack()
    {
        _calculateDamage = new CalculateDamage(_attacker, _defender, _WTB, _view);
        _defender = _calculateDamage.getDefenderAfterAttack();
    }

    private void CombatEnd()
    {
        
    }
}