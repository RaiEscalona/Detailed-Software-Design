namespace Fire_Emblem.Characters;

public class HandleTeamExceptions
{
    private Dictionary<string, string> _nameSkillDict;
    private bool _isTeamGood;

    public HandleTeamExceptions(Dictionary<string, string> team)
    {
        _nameSkillDict = team;
        _isTeamGood = true;
    }

    public void CheckErrors()
    {
        CheckMaximumCharactersTeam();
        CheckMaximumHabilitiesPerCharacter();
        CheckRepeatedHabilities();
    }

    public bool GetViability()
    {
        return _isTeamGood;
    }

    private void CheckMaximumCharactersTeam()
    {
        int amountOfCharacters = _nameSkillDict.Count;
        if (amountOfCharacters < 1 || amountOfCharacters > 3)
        {
            _isTeamGood = false;
        }
    }
    
    private void CheckMaximumHabilitiesPerCharacter()
    {
        if (_nameSkillDict.Values.Any(value => value == "No skill assigned"))
        {
            // Handle the case where there's at least one "No skill assigned"
            return;
        }
        int amounfOfSkills = _nameSkillDict.Values.Sum(skill => skill.Split(',').Length);
        if (amounfOfSkills > 2)
        {
            _isTeamGood = false;
        }
    }

    private void CheckRepeatedHabilities()
    {
        if (_nameSkillDict.Values.Any(value => value == "No skill assigned"))
        {
            // Handle the case where there's at least one "No skill assigned"
            return;
        }
        bool hasDuplicateValues = false;
        HashSet<string> uniqueValues = new HashSet<string>();
        foreach (string value in _nameSkillDict.Values)
        {
            string[] skills = value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string skill in skills)
            {
                string trimmedSkill = skill.Trim();
                if (!uniqueValues.Add(trimmedSkill))
                {
                    hasDuplicateValues = true;
                }
            }
        }

        if (hasDuplicateValues)
        {
            _isTeamGood = false;
        }
    }
}