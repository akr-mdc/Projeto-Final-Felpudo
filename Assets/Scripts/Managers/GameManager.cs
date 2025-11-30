using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int currentLevel = 1; // nível atual (1 a 3)

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Inicia o fluxo de um nível (Story → Tutorial → Play)
    public void StartLevel(int level)
    {
        currentLevel = Mathf.Clamp(level, 1, 3);
        SceneManager.LoadScene($"Level{currentLevel}_Story");
    }

    // Retorna o nome da cena Play do nível atual
    public string GetPlaySceneName() => $"Level{currentLevel}_Play";

    // Retorna a próxima cena Story ou EndGame
    public string GetNextStoryOrEnd()
    {
        int next = currentLevel + 1;
        if (next <= 3) return $"Level{next}_Story";
        return "EndGame";
    }

    // Carregar qualquer cena por nome
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
