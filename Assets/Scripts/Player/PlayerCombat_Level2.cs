using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat_Level2 : MonoBehaviour
{
    public Animator anim;

    [Header("Hitboxes")]
    public GameObject leftHitbox;
    public GameObject rightHitbox;

    [Header("Attack")]
    public float attackDuration = 0.15f;
    private bool isAttacking = false;

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
        if (!isAttacking)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                StartCoroutine(AttackLeft());
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                StartCoroutine(AttackRight());
            }
        }
    }

    // ----------------- ATAQUES -----------------

    System.Collections.IEnumerator AttackLeft()
    {
        isAttacking = true;
        anim.Play("Left Stomp");
        leftHitbox.SetActive(true);

        yield return new WaitForSeconds(attackDuration);

        leftHitbox.SetActive(false);
        anim.Play("Idle");
        isAttacking = false;
    }

    System.Collections.IEnumerator AttackRight()
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

    System.Collections.IEnumerator FlashRoutine()
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
