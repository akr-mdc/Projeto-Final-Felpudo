using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Level3 : MonoBehaviour
{
    public int maxHealth = 20;
    private int currentHealth;
    public float moveSpeed = 2f;
    public float verticalAmplitude = 0.5f;
    public Transform leftLimit, rightLimit;
    private Vector3 targetPosition;

    [Header("Visuals")]
    public SpriteRenderer spriteRenderer;
    public float flashDuration = 0.2f;

    [Header("Attacks")]
    public GameObject stalactitePrefab;
    public Transform attackPoint;
    public float attackInterval = 2f;

    void Start()
    {
        currentHealth = maxHealth;
        targetPosition = rightLimit.position;
        StartCoroutine(AttackCoroutine());
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector3 pos = transform.position;
        pos = Vector3.MoveTowards(pos, targetPosition, moveSpeed * Time.deltaTime);
        pos.y += Mathf.Sin(Time.time * moveSpeed) * verticalAmplitude;
        transform.position = pos;

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            targetPosition = (targetPosition == leftLimit.position) ? rightLimit.position : leftLimit.position;
    }

    public void TakeDamage(int amount)
    {
        if (currentHealth <= 0) return;

        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0);

        StartCoroutine(FlashCoroutine());

        // Atualiza barra de vida
        if (GameManager_Level3.Instance != null)
        {
            GameManager_Level3.Instance.UpdateBossHealthBar(currentHealth, maxHealth);
        }

        if (currentHealth <= 0)
        {
            if (GameManager_Level3.Instance != null)
                GameManager_Level3.Instance.BossDefeated();

            Destroy(gameObject);
        }
    }

    private System.Collections.IEnumerator FlashCoroutine()
    {
        if (spriteRenderer == null) yield break;
        Color original = spriteRenderer.color;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = original;
    }

    private System.Collections.IEnumerator AttackCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackInterval);
            if (stalactitePrefab != null && attackPoint != null)
                Instantiate(stalactitePrefab, attackPoint.position, Quaternion.identity);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && GameManager_Level3.Instance != null)
        {
            GameManager_Level3.Instance.TakeDamage();
        }
    }

}






