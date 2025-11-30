using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("UI Canvases")]
    public GameObject victoryCanvas;
    public GameObject defeatCanvas;

    void Start()
    {
        if (victoryCanvas) victoryCanvas.SetActive(false);
        if (defeatCanvas) defeatCanvas.SetActive(false);
    }

    public void OnLevelComplete()
    {
        if (victoryCanvas) victoryCanvas.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OnPlayerDefeated()
    {
        if (defeatCanvas) defeatCanvas.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OnNextLevelPressed()
    {
        Time.timeScale = 1f;
        if (GameManager.Instance != null)
        {
            int next = GameManager.Instance.currentLevel + 1;
            GameManager.Instance.currentLevel = Mathf.Clamp(next, 1, 3);
            string nextScene = GameManager.Instance.GetNextStoryOrEnd();
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void OnRetryPressed()
    {
        Time.timeScale = 1f;
        int lvl = GameManager.Instance != null ? GameManager.Instance.currentLevel : 1;
        SceneManager.LoadScene($"Level{lvl}_Play");
    }

    public void OnBackToMenuPressed()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
