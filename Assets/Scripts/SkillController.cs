using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    void Start()
    {
        Character player = new Character(10);

        // 나이를 15로 변경하여 나이에 따른 스킬 추가
        player.SetAge(15);

        // 무기 생성 및 설정
        Weapon sword = new Weapon("Sword", new List<Skill>
        {
            new Skill("Sword Slash", 0),
            new Skill("Power Strike", 15)
        });

        // 무기에 따른 스킬 추가
        player.EquipWeapon(sword);

        // 나이를 20으로 변경하여 나이에 따른 스킬 추가
        player.SetAge(20);

        // 사용 가능한 스킬 출력
        foreach (Skill skill in player.AvailableSkills)
        {
            Debug.Log($"Available Skill: {skill.SkillName}");
        }
    }
}
