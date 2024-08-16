using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlyaerController : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Image magicBar;
    [SerializeField] private Image healthBar;
    [SerializeField] private Image hungerBar;
    [SerializeField] private Image cleanlinessBar;
    [SerializeField] private TextMeshProUGUI magicText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI hungerText;
    [SerializeField] private TextMeshProUGUI cleanlinessText;
    [SerializeField] private Button sleepButton;
    [SerializeField] private Button eatButton;
    [SerializeField] private Button washButton;
    
    void Start()
    {
        magicBar.fillAmount = 0f;
        healthBar.fillAmount = 1f;
        hungerBar.fillAmount = 1f;
        cleanlinessBar.fillAmount = 1f;
        StartCoroutine(DecreaseHealth());
    }

    void Update()
    {
        healthBar.fillAmount = PlayerStats.Instance.health / 100f;
        healthText.text = "health" + PlayerStats.Instance.health + "/100";
        hungerBar.fillAmount = PlayerStats.Instance.hunger / 100f;
        hungerText.text = "hunger" + PlayerStats.Instance.hunger + "/100";
        cleanlinessBar.fillAmount = PlayerStats.Instance.cleanliness / 100f;
        cleanlinessText.text = "cleanliness" + PlayerStats.Instance.cleanliness + "/100";
        magicBar.fillAmount = PlayerStats.Instance.magic / 100f;
        magicText.text = "magic" + PlayerStats.Instance.magic + "/100";
    }

    IEnumerator DecreaseHealth()
    {
        while (true)
        {
            if (PlayerStats.Instance.health > 0)
            {
                yield return new WaitForSeconds(10f);
                PlayerStats.Instance.DecreaseStat(EStatType.Health, 10);
                PlayerStats.Instance.DecreaseStat(EStatType.Cleanliness, 10);
                PlayerStats.Instance.DecreaseStat(EStatType.Hunger, 10);
                Debug.Log("stats decreased");
            }
            else
            {
                yield return null;
            }
        }
    }
    

}
