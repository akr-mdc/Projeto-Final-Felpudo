using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_Level1 : MonoBehaviour
{
    [Header("Movement")]
    public float jumpForce = 6f;
    public float maxFallSpeed = -12f;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    [Header("Damage Feedback")]
    public float flashDuration = 0.1f;
    public int flashCount = 4;
    private bool isInvulnerable = false;

    private GameManager_Level1 gm;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        gm = FindObjectOfType<GameManager_Level1>();
    }

    void Update()
    {
        HandleInput();
        LimitVerticalSpeed();
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void LimitVerticalSpeed()
    {
        if (rb.velocity.y < maxFallSpeed)
            rb.velocity = new Vector2(rb.velocity.x, maxFallSpeed);
    }

    public void TakeDamage()
    {
        if (isInvulnerable) return;

        gm.LoseLife();
        StartCoroutine(FlashRed());
    }

    IEnumerator FlashRed()
    {
        isInvulnerable = true;

        for (int i = 0; i < flashCount; i++)
        {
            sr.color = Color.red;
            yield return new WaitForSeconds(flashDuration);
            sr.color = Color.white;
            yield return new WaitForSeconds(flashDuration);
        }

        isInvulnerable = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            TakeDamage();
        }
    }
}
