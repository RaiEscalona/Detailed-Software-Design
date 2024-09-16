using Fire_Emblem_View;
using Fire_Emblem.Characters;

namespace Fire_Emblem.Attacks;

public class TriangleAdvantage
{
    private Character _attacker;
    private Character _defender;
    private string _weaponAttacker;
    private string _weaponDefender;
    private double _WTB;
    private View _view;
    public double CalculateAdvantage(Character attacker, Character defender, View view)
    {
        _WTB = 1;
        _attacker = attacker;
        _defender = defender;
        _weaponAttacker = attacker.Weapon;
        _weaponDefender = defender.Weapon;
        _view = view;
        this.IsThereAdvantage();
        return _WTB;
    }

    private void IsThereAdvantage()
    {
        if ((_weaponAttacker == "Sword" && _weaponDefender == "Axe") || (_weaponAttacker == "Lance" && _weaponDefender == "Sword") || (_weaponAttacker == "Axe" && _weaponDefender == "Lance"))
        {
            _view.WriteLine($"{_attacker.Name} ({_weaponAttacker}) tiene ventaja con respecto a {_defender.Name} ({_weaponDefender})");
            _WTB = 1.2;
        }
        else if ((_weaponAttacker == "Sword" && _weaponDefender == "Lance") ||
                 (_weaponAttacker == "Lance" && _weaponDefender == "Axe") ||
                 (_weaponAttacker == "Axe" && _weaponDefender == "Sword"))
        {
            _view.WriteLine($"{_defender.Name} ({_weaponDefender}) tiene ventaja con respecto a {_attacker.Name} ({_weaponAttacker})");
            _WTB = 0.8;
        }
        else
        {
            _view.WriteLine("Ninguna unidad tiene ventaja con respecto a la otra");
        }
    }
}