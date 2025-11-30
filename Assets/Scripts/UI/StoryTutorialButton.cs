using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryTutorialButton : MonoBehaviour
{
    // Cena que este botão deve carregar
    [Tooltip("Nome da cena que será carregada ao clicar no botão")]
    public string nextSceneName;

    public void OnButtonPressed()
    {
        if (string.IsNullOrEmpty(nextSceneName))
        {
            Debug.LogWarning("StoryTutorialButton: nextSceneName não definido!");
            return;
        }

        SceneManager.LoadScene(nextSceneName);
    }
}

