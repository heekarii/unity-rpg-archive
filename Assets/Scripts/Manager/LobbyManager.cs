using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    [Header("Panel")]
    [SerializeField] private GameObject statPanel;
    //[SerializeField] private GameObject movePanel;
    
    [Header("Button")]
    [SerializeField] private Button statButton;
    [SerializeField] private Button moveButton;
    // [SerializeField] private Button shopButton;
    // [SerializeField] private Button trainingButton;
    // [SerializeField] private Button settingButton;
    
    [Header("Image")]
    [SerializeField] private Image statImage;
    [SerializeField] private Image healthImage;
    [SerializeField] private Image hungerImage;
    [SerializeField] private Image cleanlinessImage;
    [SerializeField] private Image mentalImage;

    [Header("Sprite")] 
    [SerializeField] private Sprite[] statSprites;
    [SerializeField] private Sprite[] healthSprites;
    [SerializeField] private Sprite[] hungerSprites;
    [SerializeField] private Sprite[] cleanlinessSprites;
    [SerializeField] private Sprite[] mentalSprites;

    [SerializeField] private bool isPanelActive;
    
    
    [SerializeField] private float _lowStat;
    private List<AgeRange> ageRanges;
    
    public struct AgeRange
    {
        public int minAge;
        public int maxAge;
        public float goodMin;
        public float normalMin;
        
        public AgeRange(int minAge, int maxAge, float goodMin, float normalMin)
        {
            this.minAge = minAge;
            this.maxAge = maxAge;
            this.goodMin = goodMin;
            this.normalMin = normalMin;
        }
    }



    private void Start()
    {
        statPanel.SetActive(false);
        InitializeRange();
    }

    private void OnEnable()
    {
        statButton.onClick.AddListener(() =>
        {
            if (!isPanelActive)
            {
                statPanel.SetActive(true);
                isPanelActive = true;
                Time.timeScale = 0;
            }
            else
            {
                statPanel.SetActive(false);
                isPanelActive = false;
                Time.timeScale = 1;
            }
            UpdateStatImages();
        });
        // moveButton.onClick.AddListener(() =>
        // {
        //     movePanel.SetActive(true);
        // });
    }

    private void Update()
    {
        _lowStat = PlayerStats.Instance.GetLowestStat();
        UpdateStatImage(_lowStat);
    }
    private void InitializeRange()
    {
        ageRanges = new List<AgeRange>
        {
            new AgeRange(0, 24, 85, 65),
            new AgeRange(25, 49, 80, 50),
            new AgeRange(50, 74, 75, 40),
            new AgeRange(75, 99, 70, 35)
        };
    }

    private string GetStatus(float magic, float percentage)
    {
        foreach (var range in ageRanges)
        {
            if (magic >= range.minAge && magic <= range.maxAge)
            {
                if (percentage >= range.goodMin)
                    return "good";
                else if (percentage >= range.normalMin)
                    return "normal";
                else
                    return "bad";
            }
        }

        return "error";
    }

    // UpdateImages 함수를 호출해주는 함수
    private void UpdateStatImages()
    {
        float magic = PlayerStats.Instance.magic;
        float health = PlayerStats.Instance.health;
        float hunger = PlayerStats.Instance.hunger;
        float cleanliness = PlayerStats.Instance.cleanliness;
        float mental = PlayerStats.Instance.mental;
        
        UpdateImages(healthImage, healthSprites, magic, health);
        UpdateImages(hungerImage, hungerSprites, magic, hunger);
        UpdateImages(cleanlinessImage, cleanlinessSprites, magic, cleanliness);
        UpdateImages(mentalImage, mentalSprites, magic, mental);
    }
    
    // 스탯 패널 내에 있는 이미지 업데이트
    private void UpdateImages(Image image, Sprite[] sprites, float magic, float percentage)
    {
        string status = GetStatus(magic, percentage);

        switch (status)
        {
            case "good":
                image.sprite = sprites[0];
                break;
            case "normal":
                image.sprite = sprites[1];
                break;
            case "bad":
                image.sprite = sprites[2];
                break;
            default:
                Debug.LogError("error");
                break;
        }
    }
    
    // 종합 스탯 표시
    private void UpdateStatImage(float val)
    {
        float magic = PlayerStats.Instance.magic;
        string status = GetStatus(magic, val);

        switch (status)
        {
            case "good":
                statImage.sprite = statSprites[0];
                break;
            case "normal":
                statImage.sprite = statSprites[1];
                break;
            case "bad":
                statImage.sprite = statSprites[2];
                break;
            default:
                Debug.LogError("status error");
                break;
        }
    }
    
}
