using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BattleManager : MonoBehaviour
{
    public GameObject playerPrefab;        
    public GameObject enemyPrefab;        
    public Transform playerSpawnPoint;  
    public Transform enemySpawnPoint;    

    private GameObject player;
    private GameObject enemy;
    private PlayerController playerController;
    private EnemyController enemyController;

    void Start()
    {
        player = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
        enemy = Instantiate(enemyPrefab, enemySpawnPoint.position, Quaternion.identity);


        playerController = player.GetComponent<PlayerController>();
        enemyController = enemy.GetComponent<EnemyController>();

        // Start the battle
        StartPlayerTurn();
    }

    void StartPlayerTurn()
    {
        playerController.EnableMoveSelection(true);
    }

    public void OnPlayerMoveSelected(string move)
    {
        playerController.ExecuteMove(move, enemyController);
        StartCoroutine(HandleEnemyTurn());
    }

    IEnumerator HandleEnemyTurn()
    {
        yield return new WaitForSeconds(1);  // Wait for 1 second before the enemy attacks

        enemyController.ExecuteMove("Attack", playerController);

        if (playerController.IsDefeated())
        {
            EndBattle(false);
        }
        else if (enemyController.IsDefeated())
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
}
