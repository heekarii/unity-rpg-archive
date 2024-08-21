using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Character
{
    public int Age { get; private set; } // 나이
    public Weapon CurrentWeapon { get; private set; } // 현재 소지 무기
    public List<Skill> AvailableSkills { get; private set; } // 사용 가능 스킬 목록

    public Character(int initialAge)
    {
        Age = initialAge;
        AvailableSkills = new List<Skill>();
        AddSkillsByAge();
    }

    // 나이 설정 함수
    public void SetAge(int newAge)
    {
        Age = newAge;
        AddSkillsByAge(); 
    }

    // 무기 장착 함수
    public void EquipWeapon(Weapon newWeapon)
    {
        CurrentWeapon = newWeapon;
        AddSkillsByWeapon(); 
    }

    // 나이에 따른 스킬 추가 함수
    private void AddSkillsByAge()
    {
        foreach (Skill skill in SkillDatabase.GetSkillsByAge(Age))
        {
            if (!AvailableSkills.Contains(skill))
            {
                AvailableSkills.Add(skill);
                Debug.Log($"New Skill Acquired by Age: {skill.SkillName}");
            }
        }
    }

    // 무기에 따른 스킬 추가 함수
    private void AddSkillsByWeapon()
    {
        if (CurrentWeapon != null)
        {
            foreach (Skill skill in CurrentWeapon.WeaponSkills)
            {
                if (!AvailableSkills.Contains(skill))
                {
                    AvailableSkills.Add(skill);
                    Debug.Log($"New Skill Acquired by Weapon: {skill.SkillName}");
                }
            }
        }
    }
}
