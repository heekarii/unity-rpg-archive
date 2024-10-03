using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BattleManager : MonoBehaviour
{
    public GameObject playerPrefab;        
    public GameObject enemyPrefab;        
    public Transform playerSpawnPoint;  
    public Transform enemySpawnPoint;    

    private GameObject _player;
    private GameObject _enemy;
    private BattleManager _battleManager;
    private EnemyController _enemyController;
    private PlayerController _playerController;

    [SerializeField] private float health = 100;
    [SerializeField] private bool moveSelectionEnabled = false;

    void Start()
    {
        _player = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
        _enemy = Instantiate(enemyPrefab, enemySpawnPoint.position, Quaternion.identity);
        

        // playerController = player.GetComponent<PlayerController>();
        // enemyController = enemy.GetComponent<EnemyController>();

        // Start the battle
        StartPlayerTurn();
    }

    void StartPlayerTurn()
    {
        EnableMoveSelection(true);
    }

    public void OnPlayerMoveSelected(string move)
    {
        ExecuteMove(move, _enemyController);
        StartCoroutine(HandleEnemyTurn());
    }

    IEnumerator HandleEnemyTurn()
    {
        yield return new WaitForSeconds(1);  // Wait for 1 second before the enemy attacks

        _enemyController.ExecuteMove("Attack");

        if (IsDefeated())
        {
            EndBattle(false);
        }
        else if (_enemyController.IsDefeated())
        {
            EndBattle(true);
        }
        else
        {
            StartPlayerTurn();
        }
    }

    void EndBattle(bool playerWon)
    {
        
        Debug.Log(playerWon ? "Player Won!" : "Player Lost!");
    }
    
    public void EnableMoveSelection(bool enable)
    {
        moveSelectionEnabled = enable;
    }

    public void ExecuteMove(string move, EnemyController enemy)
    {
        if (moveSelectionEnabled)
        {
            EnableMoveSelection(false);

            // animator.SetTrigger(move);

            enemy.TakeDamage(20);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            health = 0;
            // animator.SetTrigger("Defeated");
            // Handle player defeat logic
        }
        else
        {
            // animator.SetTrigger("TakeDamage");
        }
    }

    private bool IsDefeated()
    {
        return health <= 0;
    }
}
