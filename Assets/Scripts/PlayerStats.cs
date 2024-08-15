using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerStats : MonoBehaviour
{
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

    void IncreaseStat(string str, int amount)
    {
        switch (str)
        {
            case "health":
                health += amount;
                break;
            case "hunger":
                hunger += amount;
                break;
            case "cleanliness":
                cleanliness += amount;
                break;
            case "magic":
                magic += amount;
                break;
        }
    }

    void Decrease(string str, int amount)
    {
        switch (str)
        {
            case "health":
                health -= amount;
                break;
            case "hunger":
                hunger -= amount;
                break;
            case "cleanliness":
                cleanliness -= amount;
                break;
            case "magic":
                magic -= amount;
                break;
        }
    }
    
}
