using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

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
    /// <param name="stat">감소시킬 스탯의 이름</param>
    /// <param name="amt">감소시킬 스탯의 양</param>
    public void DecreaseStat(string stat, int amt)
    {
        switch (stat)
        {
            case "health":
                health = Mathf.Max(health - amt, 0);
                break;
            case "hunger":
                hunger = Mathf.Max(hunger - amt, 0);
                break;
            case "cleanliness":
                hunger = Mathf.Max(cleanliness - amt, 0);
                break; 
            case "magic":
                magic = Mathf.Max(magic - amt, 0);
                break;
            default:
                Debug.LogWarning(stat + "Invalid stat name");
                break;
        }
    }
    
}
