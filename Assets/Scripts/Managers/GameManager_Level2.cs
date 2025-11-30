using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager_Level2 : MonoBehaviour
{
    public static GameManager_Level2 Instance;

    public int maxLives = 3;
    private int currentLives;
    public Image[] lifeIcons;

    public float surviveTime = 30f;
    private float timer;
    public TextMeshProUGUI timerText;

    public PlayerCombat_Level2 playerCombat;

    private LevelManager levelManager;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Time.timeScale = 1f;
        currentLives = maxLives;
        timer = surviveTime;
        levelManager = FindObjectOfType<LevelManager>();

        UpdateLivesUI();
        UpdateTimerUI();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0f) timer = 0f;

        UpdateTimerUI();

        if (timer <= 0)
        {
            Victory();
        }
    }

    public void TakeDamage()
    {
        currentLives--;
        playerCombat?.TriggerFlash();

        UpdateLivesUI();

        if (currentLives <= 0)
            Defeat();
    }

    void UpdateLivesUI()
    {
        for (int i = 0; i < lifeIcons.Length; i++)
            lifeIcons[i].gameObject.SetActive(i < currentLives);
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
            timerText.text = "Tempo: " + Mathf.Ceil(timer);
    }

    void Victory() => levelManager?.OnLevelComplete();
    void Defeat() => levelManager?.OnPlayerDefeated();
}



