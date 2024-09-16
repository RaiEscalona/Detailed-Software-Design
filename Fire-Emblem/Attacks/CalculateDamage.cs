using Fire_Emblem_View;
using Fire_Emblem.Characters;

namespace Fire_Emblem.Attacks;

public class CalculateDamage
{
    private Character _attacker;
    private Character _defender;
    private string _weaponAttacker;
    private string _weaponDefender;
    private double _WTB;
    private View _view;
    private int _damage;

    public CalculateDamage(Character attacker, Character defender, double wtb, View view)
        {
            _attacker = attacker;
            _defender = defender;
            _weaponAttacker = attacker.Weapon;
            _weaponDefender = defender.Weapon;
            _WTB = wtb;
            _view = view;
            _damage = 0;
            
        }

    public Character getDefenderAfterAttack()
    {
        this.AttackSequence();
        return _defender;
    }

    private void AttackSequence()
    {
        this.CheckAttackType();
        this.DealDamage();
    }

    private void CheckAttackType()
    {
        if (_weaponAttacker == "Magic")
        {
            this.calculateDamage(_attacker.Atk, _defender.Res);
        }
        else
        {
            this.calculateDamage(_attacker.Atk, _defender.Def);
        }
    }

    private void calculateDamage(string attack, string defense)
    {
        int.TryParse(attack, out var atk);
        int.TryParse(defense, out var def);
        _damage = (int)Math.Floor(atk * _WTB) - def;
        if (_damage < 0)
        {
            _damage = 0;
        }
    }

    private void DealDamage()
    {
        _view.WriteLine($"{_attacker.Name} ataca a {_defender.Name} con {_damage} de daño");
        int.TryParse(_defender.HP, out int oldHp);
        int newHp = oldHp - _damage;
        if (newHp > 0)
        {
            string newHpString = newHp.ToString();
            _defender.HP = newHpString;
        }
        else
        {
            _defender.HP = "0";
        }
    }
}