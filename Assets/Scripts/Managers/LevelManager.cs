using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("UI Canvases")]
    public GameObject victoryCanvas;
    public GameObject defeatCanvas;

    private void Start()
    {
        // Garantir que o jogo comece ativo
        Time.timeScale = 1f;

        if (victoryCanvas) victoryCanvas.SetActive(false);
        if (defeatCanvas) defeatCanvas.SetActive(false);
    }

    // Chamado quando o jogador vence
    public void OnLevelComplete()
    {
        if (victoryCanvas) victoryCanvas.SetActive(true);
        Time.timeScale = 0f;
    }

    // Chamado quando o jogador perde
    public void OnPlayerDefeated()
    {
        if (defeatCanvas) defeatCanvas.SetActive(true);
        Time.timeScale = 0f;
    }

    // Botões UI
    public void OnNextLevelPressed()
    {
        Time.timeScale = 1f;

        if (GameManager.Instance != null)
        {
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

        if (GameManager.Instance != null)
        {
            string playScene = GameManager.Instance.GetPlaySceneName();
            SceneManager.LoadScene(playScene);
        }
    }

    public void OnBackToMenuPressed()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}


