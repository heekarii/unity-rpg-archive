using System.Collections.Generic;

public static class SkillDatabase
{
    public static List<Skill> GetSkillsByAge(int age)
    {
        List<Skill> skills = new List<Skill>();

        if (age >= 18)
        {
            skills.Add(new Skill("시냅스", 18));
            skills.Add(new Skill("망냥냥 킥", 18));
        }
        if (age >= 12)
        {
            skills.Add(new Skill("라니 던지기", 12));
            skills.Add(new Skill("망냥냥 펀치", 12));
        }
        if (age < 12)
        {
            skills.Add(new Skill("귀여움", 0));
        }

        return skills;
    }
}
