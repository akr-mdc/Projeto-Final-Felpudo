using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat_Level2_Touch : MonoBehaviour
{
    public Animator anim;

    [Header("Hitboxes")]
    public GameObject leftHitbox;
    public GameObject rightHitbox;

    [Header("Attack")]
    public float attackDuration = 0.15f;
    private bool isAttacking = false;

    private int lastDir = 1; // 1 direita, -1 esquerda

    [Header("Damage Flash")]
    public SpriteRenderer spriteRenderer;
    public float flashDuration = 0.1f;
    public int flashCount = 4;

    void Start()
    {
        leftHitbox.SetActive(false);
        rightHitbox.SetActive(false);
    }

    void Update()
    {
        UpdateDirection();

        if (!isAttacking && TouchInput.actionPressed)
        {
            TouchInput.actionPressed = false;

            if (lastDir == -1)
                StartCoroutine(AttackLeft());
            else
                StartCoroutine(AttackRight());
        }
    }

    void UpdateDirection()
    {
        if (TouchInput.moveLeft)
            lastDir = -1;
        else if (TouchInput.moveRight)
            lastDir = 1;
    }

    IEnumerator AttackLeft()
    {
        isAttacking = true;
        anim.Play("Left Stomp");
        leftHitbox.SetActive(true);

        yield return new WaitForSeconds(attackDuration);

        leftHitbox.SetActive(false);
        anim.Play("Idle");
        isAttacking = false;
    }

    IEnumerator AttackRight()
    {
        isAttacking = true;
        anim.Play("Right Stomp");
        rightHitbox.SetActive(true);

        yield return new WaitForSeconds(attackDuration);

        rightHitbox.SetActive(false);
        anim.Play("Idle");
        isAttacking = false;
    }

    public void TriggerFlash()
    {
        StartCoroutine(FlashRoutine());
    }

    IEnumerator FlashRoutine()
    {
        for (int i = 0; i < flashCount; i++)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(flashDuration);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(flashDuration);
        }
    }
}
