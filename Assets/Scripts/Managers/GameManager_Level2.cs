using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager_Level2 : MonoBehaviour
{
    public static GameManager_Level2 Instance;

    [Header("Lives")]
    public int maxLives = 3;
    private int currentLives;

    public Image[] lifeIcons;

    [Header("Timer")]
    public float surviveTime = 30f;
    private float timer;
    public TextMeshProUGUI timerText;

    [Header("Screens")]
    public GameObject winScreen;
    public GameObject loseScreen;

    [Header("Player Flash")]
    public PlayerCombat_Level2 playerCombat;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        currentLives = maxLives;
        timer = surviveTime;
        UpdateLivesUI();
        UpdateTimerUI();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        UpdateTimerUI();

        if (timer <= 0)
        {
            Win();
        }
    }

    public void TakeDamage()
    {
        currentLives--;

        if (playerCombat != null)
            playerCombat.TriggerFlash();

        UpdateLivesUI();

        if (currentLives <= 0)
        {
            Lose();
        }
    }

    // ----------------- UI -----------------

    void UpdateLivesUI()
    {
        for (int i = 0; i < lifeIcons.Length; i++)
        {
            lifeIcons[i].gameObject.SetActive(i < currentLives);
        }
    }

    void UpdateTimerUI()
    {
        timerText.text = "Tempo: " + Mathf.Ceil(timer);
    }

    // ----------------- FIM DE JOGO -----------------

    void Win()
    {
        winScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    void Lose()
    {
        loseScreen.SetActive(true);
        Time.timeScale = 0f;
    }
}
