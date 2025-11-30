using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager_Level3 : MonoBehaviour
{
    public static GameManager_Level3 Instance;

[Header("Player")]
    public Player_Level3 player;
    public int maxLives = 3;
    private int currentLives;
    public Image[] lifeIcons;

    [Header("Boss")]
    public Slider bossHealthBar;
    public int bossMaxHealth = 10;

    [Header("UI")]
    public GameObject winScreen;
    public GameObject loseScreen;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        currentLives = maxLives;
        UpdateLivesUI();
        if (winScreen) winScreen.SetActive(false);
        if (loseScreen) loseScreen.SetActive(false);
        if (bossHealthBar != null) bossHealthBar.value = 1f;
    }

    // ---------------- Player ----------------
    public void TakeDamage()
    {
        currentLives--;
        if (player != null) player.TriggerFlash();
        UpdateLivesUI();

        if (currentLives <= 0)
            PlayerDefeated();
    }

    public void UpdateLivesUI()
    {
        for (int i = 0; i < lifeIcons.Length; i++)
        {
            if (lifeIcons[i] != null)
                lifeIcons[i].gameObject.SetActive(i < currentLives);
        }
    }

    private void PlayerDefeated()
    {
        if (loseScreen) loseScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    // ---------------- Boss ----------------
    public void UpdateBossHealthBar(int currentHealth, int maxHealth)
    {
        if (bossHealthBar != null)
            bossHealthBar.value = Mathf.Clamp01((float)currentHealth / maxHealth);
    }

    public void BossDefeated()
    {
        if (winScreen) winScreen.SetActive(true);
        Time.timeScale = 0f;
    }

}





