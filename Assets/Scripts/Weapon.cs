using System.Collections.Generic;

// 무기별 사용 가능 스킬 지정
public class Weapon
{
    public string WeaponName { get; private set; }
    public List<Skill> WeaponSkills { get; private set; }

    public Weapon(string name, List<Skill> skills)
    {
        WeaponName = name;
        WeaponSkills = skills;
    }
}
