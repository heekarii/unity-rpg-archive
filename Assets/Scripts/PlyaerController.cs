using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlyaerController : MonoBehaviour
{
    void Start()
    {
        
    }


    IEnumerator DecreaseHealth()
    {
        while (true)
        {
            if (PlayerStats.Instance.health > 0)
            {
                PlayerStats.Instance.DecreaseStat("health", 10);
            }
        }
    }

}
