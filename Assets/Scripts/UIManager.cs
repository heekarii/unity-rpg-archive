using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button moveButton1;
    public Button moveButton2;
    public Button moveButton3;
    public Button moveButton4;
    public Text statusText;
    public Slider playerHealthBar;
    public Slider enemyHealthBar;

    public void SetMoveButtonsActive(bool active)
    {
        moveButton1.interactable = active;
        moveButton2.interactable = active;
        moveButton3.interactable = active;
        moveButton4.interactable = active;
    }

    public void UpdateHealthBars(int playerHealth, int enemyHealth)
    {
        playerHealthBar.value = playerHealth;
        enemyHealthBar.value = enemyHealth;
    }

    public void ShowStatusMessage(string message)
    {
        statusText.text = message;
    }
}
