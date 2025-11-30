using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialScreenController : MonoBehaviour
{
    public void OnStartLevelPressed()
    {
        int lvl = GameManager.Instance != null ? GameManager.Instance.currentLevel : 1;
        SceneManager.LoadScene($"Level{lvl}_Play");
    }

    public void OnBackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
