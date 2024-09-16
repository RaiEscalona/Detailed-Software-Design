using Fire_Emblem_View;
using Fire_Emblem.Characters;

namespace Fire_Emblem.Attacks;

public class FollowUp
{
    private Character _attacker;
    private Character _defender;
    private View _view;

    public FollowUp(Character attacker, Character defender, View view)
    {
        _attacker = attacker;
        _defender = defender;
        _view = view;
    }

    public string WhoFollowsUp()
    {
        int.TryParse(_attacker.Spd, out int spdAttacker);
        int.TryParse(_defender.Spd, out int spdDefender);
        if (spdAttacker - spdDefender >= 5)
        {
            return "attacker";
        }
        else if (spdDefender - spdAttacker >= 5)
        {
            return "defender";
        }
        else
        {
            return "nobody";
        }
    }
}