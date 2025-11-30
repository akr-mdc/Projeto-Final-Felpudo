using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager_Level3 : MonoBehaviour
{
    public static GameManager_Level3 Instance;

    [Header("Player")]
    public int maxLives = 3;
    private int currentLives;
    public Image[] lifeIcons;
    public Player_Level3 player;

    [Header("Boss")]
    public Slider bossHealthBar;
    public int bossMaxHealth = 10;
    private int bossCurrentHealth;

    [Header("UI")]
    public GameObject winScreen;
    public GameObject loseScreen;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentLives = maxLives;
        bossCurrentHealth = bossMaxHealth;
        UpdateLivesUI();
        UpdateBossHealthUI();

        if (winScreen) winScreen.SetActive(false);
        if (loseScreen) loseScreen.SetActive(false);
    }

    // ---------------- Player ----------------  

    public void TakeDamage()
    {
        currentLives--;
        if (player != null) player.TriggerFlash();
        UpdateLivesUI();

        if (currentLives <= 0)
        {
            PlayerDefeated();
        }
    }

    public void UpdateLivesUI()
    {
        for (int i = 0; i < lifeIcons.Length; i++)
        {
            if (lifeIcons[i] != null)
                lifeIcons[i].gameObject.SetActive(i < currentLives);
        }
    }

    public void PlayerDefeated()
    {
        if (loseScreen) loseScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    // ---------------- Boss ----------------  

    public void TakeBossDamage(int amount)
    {
        bossCurrentHealth -= amount;
        if (bossCurrentHealth < 0) bossCurrentHealth = 0;
        UpdateBossHealthUI();

        if (bossCurrentHealth <= 0)
        {
            BossDefeated();
        }
    }

    public void UpdateBossHealthUI()
    {
        if (bossHealthBar != null)
            bossHealthBar.value = (float)bossCurrentHealth / bossMaxHealth;
    }

    public void BossDefeated()
    {
        if (winScreen) winScreen.SetActive(true);
        Time.timeScale = 0f;
    }

}




