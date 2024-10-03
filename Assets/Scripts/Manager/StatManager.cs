using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.Serialization;

public enum EStatType
{
    Health,
    Hunger,
    Cleanliness,
    Magic,
    Mental
}

public class StatManager : Singleton<StatManager>
{
    public float health;
    public float hunger;
    public float cleanliness;
    public float magic;
    public float mental;
    public float lowStat;
    
    void Start()
    {
        health = 100;
        hunger = 100;
        cleanliness = 100;
        mental = 100;
        magic = 0;
        
        UpdateLowestStat();
    }
    
    /// <summary>
    /// 플레이어의 스탯의 증감을 관리하는 함수<br>
    /// </br>
    /// 기본적으로 스탯을 증가시킵니다. 감소하고자 하는 경우 파라미터에 음수를 넣어주세요.
    /// </summary>
    /// <param name="type">변경할 스탯의 타입</param>
    /// <param name="amt">변경할 스탯의 양</param>
    public void StatControl(EStatType type, float amt)
    {
        switch (type)
        {
            case EStatType.Health:
                health = Mathf.Clamp(health + amt, 0, 100);
                break;
            case EStatType.Hunger:
                hunger = Mathf.Clamp(hunger + amt, 0, 100);
                break;
            case EStatType.Cleanliness:
                cleanliness = Mathf.Clamp(cleanliness + amt, 0, 100);
                break; 
            case EStatType.Magic:
                magic = Mathf.Clamp(magic + amt, 0, 100);
                break;
            case EStatType.Mental:
                mental = Mathf.Clamp(mental + amt, 0, 100);
                break;
            default:
                Debug.LogWarning(type + "Invalid Stat Type");
                break;
        }
        UpdateLowestStat();
    }

    private void UpdateLowestStat()
    {
        float[] stats = { health, hunger, cleanliness, mental };

        lowStat = Mathf.Min(stats);
        Debug.Log("Lowest stat: " + lowStat); 

    }

    public float GetLowestStat()
    {
        return lowStat;
    }
    
}
