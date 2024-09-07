using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Http;
using System.Threading.Tasks;

public class PlayerController : MonoBehaviour
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
    [SerializeField] private Button magicTrainingButton;

    // private Animator animator;
    private bool moveSelectionEnabled = false;
    public int health = 100;


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

    public bool IsDefeated()
    {
        return health <= 0;
    }
    
    void Start()
    {
        magicBar.fillAmount = 0f;
        healthBar.fillAmount = 1f;
        hungerBar.fillAmount = 1f;
        cleanlinessBar.fillAmount = 1f;
        StartCoroutine(DecreaseStats());
        //     animator = GetComponent<Animator>();
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

        magicTrainingButton.onClick.AddListener(() => StartCoroutine(TrainMagic())); // MagicTraining 추가
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
        magicText.text = "magic " + Mathf.RoundToInt(PlayerStats.Instance.magic) + "/100";
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

    IEnumerator TrainMagic()
    {
        float chance = Random.Range(0f, 1f);

        if (chance <= 0.65f)
        {
            PlayerStats.Instance.StatControl(EStatType.Magic, 5);
            PlayerStats.Instance.StatControl(EStatType.Health, -5);
            Debug.Log("Magic training successful: +5 Magic, -5 Health");
        }
        else
        {
            PlayerStats.Instance.StatControl(EStatType.Health, -10);
            Debug.Log("Magic training failed: -10 Health");
        }

        // 마력 제한 적용
        ClampMagic();

        yield return SendStatsToServer(); // 서버에 업데이트된 스탯을 전송
    }

    void ClampMagic()
    {
        if (PlayerStats.Instance.magic >= 100)
        {
            PlayerStats.Instance.magic = 100; // 마력이 100이면 더 이상 떨어지지 않음
        }
        else if (PlayerStats.Instance.magic >= 75)
        {
            PlayerStats.Instance.magic = Mathf.Max(PlayerStats.Instance.magic, 75); // 마력이 75 이상이면 75 이하로 떨어지지 않음
        }
        else if (PlayerStats.Instance.magic >= 50)
        {
            PlayerStats.Instance.magic = Mathf.Max(PlayerStats.Instance.magic, 50); // 마력이 50 이상이면 50 이하로 떨어지지 않음
        }
        else if (PlayerStats.Instance.magic >= 25)
        {
            PlayerStats.Instance.magic = Mathf.Max(PlayerStats.Instance.magic, 25); // 마력이 25 이상이면 25 이하로 떨어지지 않음
        }
    }

    async Task SendStatsToServer()
    {
        using (HttpClient client = new HttpClient())
        {
            string url = "http://localhost:5000/update_stats";

            var postData = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("health", PlayerStats.Instance.health.ToString()),
                new KeyValuePair<string, string>("hunger", PlayerStats.Instance.hunger.ToString()),
                new KeyValuePair<string, string>("cleanliness", PlayerStats.Instance.cleanliness.ToString()),
                new KeyValuePair<string, string>("magic", PlayerStats.Instance.magic.ToString()),
                new KeyValuePair<string, string>("mental", PlayerStats.Instance.mental.ToString())
            });

            try
            {
                HttpResponseMessage response = await client.PostAsync(url, postData);
                string responseBody = await response.Content.ReadAsStringAsync();
                Debug.Log("Server response: " + responseBody);
            }
            catch (HttpRequestException e)
            {
                Debug.LogError("Request error: " + e.Message);
            }
        }
    }
}
