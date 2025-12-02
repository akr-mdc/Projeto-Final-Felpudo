using UnityEngine;

public class TouchInput : MonoBehaviour
{
    public static bool moveLeft;
    public static bool moveRight;
    public static bool actionPressed;
    public static bool attackLeft;
    public static bool attackRight;

    void Awake()
    {
        ResetAll();
    }

    public static void ResetAll()
    {
        moveLeft = false;
        moveRight = false;
        actionPressed = false;
        attackLeft = false;
        attackRight = false;
    }

    // Movimento contínuo
    public void OnLeftDown() { moveLeft = true; }
    public void OnLeftUp() { moveLeft = false; }

    public void OnRightDown() { moveRight = true; }
    public void OnRightUp() { moveRight = false; }

    // Ação de clique único
    public void OnAction() { actionPressed = true; }

    // Ataques do Level 2
    public void PressAttackLeft() { attackLeft = true; }
    public void PressAttackRight() { attackRight = true; }
}
