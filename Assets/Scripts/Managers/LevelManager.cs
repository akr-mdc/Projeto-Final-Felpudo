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
        string nextScene = GetNextSceneName(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(nextScene);
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

    private string GetNextSceneName(string currentScene)
    {
        if (string.IsNullOrEmpty(currentScene)) return "MainMenu";

        string[] parts = currentScene.Split('_'); // Ex: Level3_Story -> ["Level3","Story"]
        if (parts.Length != 2) return "MainMenu";

        string levelPart = parts[0];
        string typePart = parts[1];

        string nextScene = "";

        if (typePart == "Story")
        {
            // Passa para a cena Play do mesmo nível
            string potentialNext = levelPart + "_Play";
            if (Application.CanStreamedLevelBeLoaded(potentialNext))
                nextScene = potentialNext;
            else
                nextScene = "EndGame"; // caso final
        }
        else if (typePart == "Play")
        {
            // Passa para o Story do próximo nível
            int levelNumber = 0;
            if (int.TryParse(levelPart.Substring(5), out levelNumber))
            {
                levelNumber++;
                string potentialNext = "Level" + levelNumber + "_Story";
                if (Application.CanStreamedLevelBeLoaded(potentialNext))
                    nextScene = potentialNext;
                else
                    nextScene = "EndGame"; // último nível
            }
            else
            {
                nextScene = "EndGame";
            }
        }
        else
        {
            nextScene = "MainMenu";
        }

        return nextScene;
    }

}



