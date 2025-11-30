using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    // Botão "Iniciar Jogo" no menu principal
    public void OnStartGamePressed()
    {
        if (GameManager.Instance != null)
        {
            // Começa pelo Nível 1
            GameManager.Instance.StartLevel(1);
        }
    }

    // Botão "Sair" no menu principal
    public void OnQuitPressed()
    {
        Application.Quit();
    }
}

