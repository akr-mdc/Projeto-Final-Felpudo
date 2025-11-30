using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager_Level1 : MonoBehaviour
{
    [Header("Lives")]
    public int maxLives = 3;
    private int currentLives;

    public Image[] lifeIcons; // arraste os sprites de coração aqui

    [Header("Timer")]
    public float survivalTime = 30f;
    private float timeLeft;

    public TMP_Text timerText; // AGORA É TEXTMESHPRO

    [Header("Level End Handling")]
    public GameObject victoryCanvas;
    public GameObject defeatCanvas;

    [Tooltip("Cena a carregar quando vencer (ex: Level2_Story)")]
    public string nextSceneName = "Level2_Story";

    void Start()
    {
        currentLives = maxLives;
        timeLeft = survivalTime;

        if (victoryCanvas != null) victoryCanvas.SetActive(false);
        if (defeatCanvas != null) defeatCanvas.SetActive(false);

        UpdateLifeUI();
        UpdateTimerUI();
    }

    void Update()
    {
        HandleTimer();
    }

    void HandleTimer()
    {
        if (currentLives <= 0 || (victoryCanvas != null && victoryCanvas.activeSelf))
            return;

        timeLeft -= Time.deltaTime;
        if (timeLeft < 0f) timeLeft = 0f;

        UpdateTimerUI();

        if (timeLeft <= 0f)
        {
            Victory();
        }
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
        {
            timerText.text = Mathf.CeilToInt(timeLeft).ToString();
        }
    }

    public void LoseLife()
    {
        if (currentLives <= 0) return;

        currentLives--;
        UpdateLifeUI();

        if (currentLives <= 0)
        {
            Defeat();
        }
    }

    void UpdateLifeUI()
    {
        if (lifeIcons == null) return;

        for (int i = 0; i < lifeIcons.Length; i++)
        {
            if (lifeIcons[i] == null) continue;
            lifeIcons[i].enabled = i < currentLives;
        }
    }

    void Victory()
    {
        if (victoryCanvas != null) victoryCanvas.SetActive(true);
        Time.timeScale = 0f;
    }

    void Defeat()
    {
        if (defeatCanvas != null) defeatCanvas.SetActive(true);
        Time.timeScale = 0f;
    }

    // Botões da UI
    public void OnNextPressed()
    {
        Time.timeScale = 1f;

        if (!string.IsNullOrEmpty(nextSceneName))
            SceneManager.LoadScene(nextSceneName);
        else
            SceneManager.LoadScene("MainMenu");
    }

    public void OnRetryPressed()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnBackToMenuPressed()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
