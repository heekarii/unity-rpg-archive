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
    [SerializeField] private Button magicTrainingButton;

    void Start()
    {
        StartCoroutine(DecreaseStats());
    }

    void OnEnable()
    {
        magicTrainingButton.onClick.AddListener(() => StartCoroutine(TrainMagic())); // MagicTraining 추가
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
