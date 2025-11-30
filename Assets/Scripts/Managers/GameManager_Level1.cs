using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager_Level1 : MonoBehaviour
{
    public int maxLives = 3;
    private int currentLives;

    public Image[] lifeIcons;
    public TMP_Text timerText;
    public float survivalTime = 30f;
    private float timer;

    private LevelManager levelManager;

    void Start()
    {
        Time.timeScale = 1f; // garantir que o jogo não fique travado
        currentLives = maxLives;
        timer = survivalTime;
        levelManager = FindObjectOfType<LevelManager>();

        UpdateLivesUI();
        UpdateTimerUI();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0f) timer = 0f;

        UpdateTimerUI();

        if (timer <= 0f)
        {
            Victory();
        }
    }

    public void LoseLife()
    {
        if (currentLives <= 0) return;

        currentLives--;
        UpdateLivesUI();

        if (currentLives <= 0)
        {
            Defeat();
        }
    }

    void UpdateLivesUI()
    {
        for (int i = 0; i < lifeIcons.Length; i++)
            lifeIcons[i].gameObject.SetActive(i < currentLives);
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
            timerText.text = Mathf.CeilToInt(timer).ToString();
    }

    void Victory() => levelManager?.OnLevelComplete();
    void Defeat() => levelManager?.OnPlayerDefeated();
}


