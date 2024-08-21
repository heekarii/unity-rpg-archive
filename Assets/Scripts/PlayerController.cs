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
    [SerializeField] private Image mentalBar;
    [SerializeField] private TextMeshProUGUI magicText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI hungerText;
    [SerializeField] private TextMeshProUGUI cleanlinessText;
    [SerializeField] private TextMeshProUGUI mentalText;
    [SerializeField] private Button sleepButton;
    [SerializeField] private Button eatButton;
    [SerializeField] private Button washButton;
    
    void Start()
    {
        magicBar.fillAmount = 0f;
        healthBar.fillAmount = 1f;
        hungerBar.fillAmount = 1f;
        cleanlinessBar.fillAmount = 1f;
        StartCoroutine(DecreaseStats());
    }
    
    void OnEnable()
    {
        sleepButton.onClick.AddListener(() =>
        {
            PlayerStats.Instance.StatControl(EStatType.Health, PlayerStats.Instance.health * 0.15f);
            PlayerStats.Instance.StatControl(EStatType.Hunger, -1 * (PlayerStats.Instance.hunger * 0.08f));
            PlayerStats.Instance.StatControl(EStatType.Mental, PlayerStats.Instance.mental * 0.08f);
            Debug.Log("slept");
        });
        eatButton.onClick.AddListener(() =>
        {
            PlayerStats.Instance.StatControl(EStatType.Health, PlayerStats.Instance.health * 0.08f);
            PlayerStats.Instance.StatControl(EStatType.Hunger, PlayerStats.Instance.hunger * 0.15f);
            PlayerStats.Instance.StatControl(EStatType.Mental, PlayerStats.Instance.mental * 0.08f);
            Debug.Log("ate");
        });
        washButton.onClick.AddListener(() =>
        {
            PlayerStats.Instance.StatControl(EStatType.Cleanliness, PlayerStats.Instance.cleanliness * 0.15f);
            PlayerStats.Instance.StatControl(EStatType.Mental, PlayerStats.Instance.mental * 0.03f);
            PlayerStats.Instance.StatControl(EStatType.Health, -1 * (PlayerStats.Instance.health * 0.03f));
            PlayerStats.Instance.StatControl(EStatType.Hunger, -1 * (PlayerStats.Instance.hunger * 0.03f));
            Debug.Log("washed");
        });
    }
    
    void Update()
    {
        healthBar.fillAmount = PlayerStats.Instance.health / 100f;
        healthText.text = "health " + Mathf.RoundToInt(PlayerStats.Instance.health) + "/100";
        hungerBar.fillAmount = PlayerStats.Instance.hunger / 100f;
        hungerText.text = "hunger " + Mathf.RoundToInt(PlayerStats.Instance.hunger) + "/100";
        cleanlinessBar.fillAmount = PlayerStats.Instance.cleanliness / 100f;
        cleanlinessText.text = "cleanliness " + Mathf.RoundToInt(PlayerStats.Instance.cleanliness) + "/100";
        magicBar.fillAmount = PlayerStats.Instance.magic / 100f;
        magicText.text = "magic " + Mathf.RoundToInt(PlayerStats.Instance.magic)+ "/100";
        mentalBar.fillAmount = PlayerStats.Instance.mental / 100f;
        mentalText.text = "mental " + Mathf.RoundToInt(PlayerStats.Instance.mental) + "/100";
    }

    IEnumerator DecreaseStats()
    {
        while (true)
        {
            if (PlayerStats.Instance.health > 0)
            {
                yield return new WaitForSeconds(10f);
                PlayerStats.Instance.StatControl(EStatType.Health, -1 * PlayerStats.Instance.health * 0.03f);
                PlayerStats.Instance.StatControl(EStatType.Cleanliness, -1 * PlayerStats.Instance.cleanliness * 0.03f);
                PlayerStats.Instance.StatControl(EStatType.Hunger, -1 * PlayerStats.Instance.hunger * 0.03f);
                PlayerStats.Instance.StatControl(EStatType.Mental, -1 * PlayerStats.Instance.mental * 0.03f);
                Debug.Log("stats decreased");
            }
            else
            {
                yield return null;
            }
        }
    }
    

}
