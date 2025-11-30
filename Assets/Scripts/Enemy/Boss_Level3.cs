using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_Level3 : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 2f;
    public Transform leftLimit;
    public Transform rightLimit;
    public float verticalOscillation = 0.5f;
    public float verticalSpeed = 2f;

    private Vector3 originalPosition;
    private int direction = 1; // 1 = direita, -1 = esquerda

    [Header("Health")]
    public int maxHealth = 10;
    private int currentHealth;
    public Slider healthBar;

    [Header("Attack")]
    public GameObject stalactitePrefab;
    public Transform attackSpawnPoint;
    public float attackCooldown = 3f;

    [Header("Visual")]
    public SpriteRenderer spriteRenderer;
    public Color flashColor = Color.red;
    public float flashDuration = 0.2f;

    private Color originalColor;

    void Start()
    {
        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }

        originalPosition = transform.position;

        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;

        StartCoroutine(AttackRoutine());
    }

    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        // Movimento horizontal
        transform.position += Vector3.right * speed * direction * Time.deltaTime;

        // Inverte direção nos limites
        if (transform.position.x >= rightLimit.position.x)
            direction = -1;
        else if (transform.position.x <= leftLimit.position.x)
            direction = 1;

        // Oscilação vertical
        Vector3 pos = transform.position;
        pos.y = originalPosition.y + Mathf.Sin(Time.time * verticalSpeed) * verticalOscillation;
        transform.position = pos;
    }

    IEnumerator AttackRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackCooldown);

            if (stalactitePrefab != null && attackSpawnPoint != null)
            {
                Instantiate(stalactitePrefab, attackSpawnPoint.position, Quaternion.identity);
            }
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (healthBar != null)
            healthBar.value = currentHealth;

        StartCoroutine(Flash());

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    IEnumerator Flash()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            spriteRenderer.color = originalColor;
        }
    }

    void Die()
    {
        // Desativa o Boss
        gameObject.SetActive(false);

        // Informa ao GameManager
        GameManager_Level3.Instance.BossDefeated();
    }
}





