using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Animator animator;
    public int health = 100;

    void Start()
    {
        // animator = GetComponent<Animator>();
    }

    public void ExecuteMove(string move, PlayerController player)
    {
        // animator.SetTrigger(move);

        player.TakeDamage(20);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            health = 0;
            // animator.SetTrigger("Defeated");
            // Handle enemy defeat logic
        }
        else
        {
            // animator.SetTrigger("TakeDamage");
        }
    }

    public bool IsDefeated()
    {
        return health <= 0;
    }
}
