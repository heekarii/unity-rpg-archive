using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Http;
using System.Threading.Tasks;

public class PlayerController : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DecreaseStats());
        // animator = GetComponent<Animator>();
    }

    IEnumerator DecreaseStats()
    {
        while (true)
        {
            if (StatManager.Instance.health > 0)
            {
                yield return new WaitForSeconds(10f);
                // TODO: 스탯 감소 비율 조정
                StatManager.Instance.StatControl(EStatType.Health, -1 * StatManager.Instance.health * 0.03f);
                StatManager.Instance.StatControl(EStatType.Cleanliness, -1 * StatManager.Instance.cleanliness * 0.03f);
                StatManager.Instance.StatControl(EStatType.Hunger, -1 * StatManager.Instance.hunger * 0.03f);
                StatManager.Instance.StatControl(EStatType.Mental, -1 * StatManager.Instance.mental * 0.03f);
                Debug.Log("stats decreased");
            }
            else
            {
                yield return null;
            }
        }
    }

    // 마력 단련
    // IEnumerator TrainMagic()
    // {
    //     float chance = Random.Range(0f, 1f);
    //
    //     if (chance <= 0.65f)
    //     {
    //         StatManager.Instance.StatControl(EStatType.Magic, 5);
    //         StatManager.Instance.StatControl(EStatType.Health, -5);
    //         Debug.Log("Magic training successful: +5 Magic, -5 Health");
    //     }
    //     else
    //     {
    //         StatManager.Instance.StatControl(EStatType.Health, -10);
    //         Debug.Log("Magic training failed: -10 Health");
    //     }
    //
    //     // 마력 제한 적용
    //     ClampMagic();
    //
    //     yield return SendStatsToServer(); // 서버에 업데이트된 스탯을 전송
    // }
    //
    // void ClampMagic()
    // {
    //     if (StatManager.Instance.magic >= 100)
    //     {
    //         StatManager.Instance.magic = 100; // 마력이 100이면 더 이상 떨어지지 않음
    //     }
    //     else if (StatManager.Instance.magic >= 75)
    //     {
    //         StatManager.Instance.magic = Mathf.Max(StatManager.Instance.magic, 75); // 마력이 75 이상이면 75 이하로 떨어지지 않음
    //     }
    //     else if (StatManager.Instance.magic >= 50)
    //     {
    //         StatManager.Instance.magic = Mathf.Max(StatManager.Instance.magic, 50); // 마력이 50 이상이면 50 이하로 떨어지지 않음
    //     }
    //     else if (StatManager.Instance.magic >= 25)
    //     {
    //         StatManager.Instance.magic = Mathf.Max(StatManager.Instance.magic, 25); // 마력이 25 이상이면 25 이하로 떨어지지 않음
    //     }
    // }
    //
    // async Task SendStatsToServer()
    // {
    //     using (HttpClient client = new HttpClient())
    //     {
    //         string url = "http://localhost:5000/update_stats";
    //
    //         var postData = new FormUrlEncodedContent(new[]
    //         {
    //             new KeyValuePair<string, string>("health", StatManager.Instance.health.ToString()),
    //             new KeyValuePair<string, string>("hunger", StatManager.Instance.hunger.ToString()),
    //             new KeyValuePair<string, string>("cleanliness", StatManager.Instance.cleanliness.ToString()),
    //             new KeyValuePair<string, string>("magic", StatManager.Instance.magic.ToString()),
    //             new KeyValuePair<string, string>("mental", StatManager.Instance.mental.ToString())
    //         });
    //
    //         try
    //         {
    //             HttpResponseMessage response = await client.PostAsync(url, postData);
    //             string responseBody = await response.Content.ReadAsStringAsync();
    //             Debug.Log("Server response: " + responseBody);
    //         }
    //         catch (HttpRequestException e)
    //         {
    //             Debug.LogError("Request error: " + e.Message);
    //         }
    //     }
    // }
}
