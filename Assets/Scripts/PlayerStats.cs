using System.Collections;
using System.Collections.Generic;
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

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this);
    }
    
    public float health;
    public float hunger;
    public float cleanliness;
    public float magic;
    public float mental;
    
    void Start()
    {
        health = 100;
        hunger = 100;
        cleanliness = 100;
        mental = 100;
        magic = 0;
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
    }
    
    
}
