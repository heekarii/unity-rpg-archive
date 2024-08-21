public class Skill
{
    public string SkillName { get; private set; }
    public int RequiredAge { get; private set; }

    public Skill(string name, int requiredAge)
    {
        SkillName = name;
        RequiredAge = requiredAge;
    }
}
