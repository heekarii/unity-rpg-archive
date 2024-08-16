using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
public enum EStatType
{
    Health,
    Hunger,
    Cleanliness,
    Magic
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
    
    public int health;
    public int hunger;
    public int cleanliness;
    public int magic;
    void Start()
    {
        health = 100;
        hunger = 100;
        cleanliness = 100;
        magic = 0;
    }
    
    public void IncreaseMagic()
    {
        magic += 5;
    }
    
    /// <summary>
    /// 플레이어의 스탯을 감소시키는 함수
    /// </summary>
    /// <param name="type">감소시킬 스탯의 타입</param>
    /// <param name="amt">감소시킬 스탯의 양</param>
    public void DecreaseStat(EStatType type, int amt)
    {
        switch (type)
        {
            case EStatType.Health:
                health = Mathf.Max(health - amt, 0);
                break;
            case EStatType.Hunger:
                hunger = Mathf.Max(hunger - amt, 0);
                break;
            case EStatType.Cleanliness:
                cleanliness = Mathf.Max(cleanliness - amt, 0);
                break; 
            case EStatType.Magic:
                magic = Mathf.Max(magic - amt, 0);
                break;
            default:
                Debug.LogWarning(type + "Invalid stat name");
                break;
        }
    }
    
}
