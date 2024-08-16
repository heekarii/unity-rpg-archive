using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlyaerController player;
    public static GameManager Instance;

    [SerializeField] private int day;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        player = FindObjectOfType<PlyaerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DaySkip()
    {
    }
}
