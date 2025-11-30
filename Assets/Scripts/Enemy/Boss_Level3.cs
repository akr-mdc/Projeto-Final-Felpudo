using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_Level3 : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 2f;
    public Transform leftLimit;
    public Transform rightLimit;
    private bool movingRight = true;

    [Header("HP")]
    public int maxHealth = 50;
    private int currentHealth;

    [Header("Damage Flash")]
    public SpriteRenderer spriteRenderer;
    public float flashDuration = 0.2f;

    [Header("Attack")]
    public Transform stalactiteSpawnPoint;
    public GameObject stalactitePrefab;
    public float attackInterval = 3f;

    void Start()
    {
        currentHealth = maxHealth;
        StartCoroutine(AttackRoutine());
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        if (movingRight)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            if (transform.position.x >= rightLimit.position.x) movingRight = false;
        }
        else
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            if (transform.position.x <= leftLimit.position.x) movingRight = true;
        }
    }

    System.Collections.IEnumerator AttackRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackInterval);

            if (stalactitePrefab != null && stalactiteSpawnPoint != null)
            {
                Instantiate(stalactitePrefab, stalactiteSpawnPoint.position, Quaternion.identity);
            }
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0) currentHealth = 0;
        StartCoroutine(Flash());

        GameManager_Level3.Instance?.TakeBossDamage(amount); // Atualiza barra de HP
    }

    private System.Collections.IEnumerator Flash()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(flashDuration);
            spriteRenderer.color = Color.white;
        }
    }
}






