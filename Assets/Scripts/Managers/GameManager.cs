using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // nível atual
    public int currentLevel = 1;

    void Awake()
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

    // começa o fluxo Story -> Tutorial -> Play para um nível
    public void StartLevel(int level)
    {
        currentLevel = Mathf.Clamp(level, 1, 3);
        SceneManager.LoadScene($"Level{currentLevel}_Story");
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public string GetPlaySceneName() => $"Level{currentLevel}_Play";

    public string GetNextStoryOrEnd()
    {
        int next = currentLevel + 1;
        if (next <= 3) return $"Level{next}_Story";
        return "EndGame";
    }
}
