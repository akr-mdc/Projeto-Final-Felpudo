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

        string currentScene = SceneManager.GetActiveScene().name;
        string nextScene = GetNextSceneName(currentScene);

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

    // Retorna a próxima cena seguindo o padrão LevelX_Story → LevelX_Play → Level(X+1)_Story
    // Se não houver próximo nível, retorna "EndGame"
    private string GetNextSceneName(string currentScene)
    {
        if (string.IsNullOrEmpty(currentScene)) return "MainMenu";

        string[] parts = currentScene.Split('_'); // Ex: Level2_Play -> ["Level2","Play"]
        if (parts.Length != 2) return "MainMenu";

        string levelPart = parts[0]; // "Level2"
        string typePart = parts[1];  // "Story" ou "Play"

        string nextScene = "";

        if (typePart == "Story")
        {
            nextScene = levelPart + "_Play";
        }
        else if (typePart == "Play")
        {
            int levelNumber = 0;
            if (int.TryParse(levelPart.Substring(5), out levelNumber)) // "Level2" -> 2
            {
                levelNumber++;
                string potentialNextScene = "Level" + levelNumber + "_Story";

                // Verifica se a cena existe no Build Settings
                if (Application.CanStreamedLevelBeLoaded(potentialNextScene))
                    nextScene = potentialNextScene;
                else
                    nextScene = "EndGame"; // Último nível atingido
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


