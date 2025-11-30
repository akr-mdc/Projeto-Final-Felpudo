using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryScreenController : MonoBehaviour
{
    public void OnContinuePressed()
    {
        int lvl = GameManager.Instance != null ? GameManager.Instance.currentLevel : 1;
        SceneManager.LoadScene($"Level{lvl}_Tutorial");
    }

    public void OnBackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
