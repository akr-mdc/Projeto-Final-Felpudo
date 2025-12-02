using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : MonoBehaviour
{
    public static bool moveLeft;
    public static bool moveRight;
    public static bool actionPressed;

    // Chamado quando botão "segura"
    public void OnLeftDown() { moveLeft = true; }
    public void OnRightDown() { moveRight = true; }

    // Chamado quando botão “solta”
    public void OnLeftUp() { moveLeft = false; }
    public void OnRightUp() { moveRight = false; }

    // Botões de clique único
    public void OnAction() { actionPressed = true; }

}

