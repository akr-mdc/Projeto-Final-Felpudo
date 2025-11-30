using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public int maxHealth = 3;
    int currentHealth;

Rigidbody2D rb;
    Vector2 movement;

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // DEBUG: aperte K para testar dano
        if (Input.GetKeyDown(KeyCode.K)) TakeDamage(1);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Primeiro tenta Level 1
        var gm1 = FindObjectOfType<GameManager_Level1>();
        if (gm1 != null)
        {
            gm1.LoseLife();
            enabled = false;
            return;
        }

        // Depois tenta Level 2
        var gm2 = FindObjectOfType<GameManager_Level2>();
        if (gm2 != null)
        {
            gm2.TakeDamage();
            enabled = false;
            return;
        }

        // Caso nenhuma cena tenha GameManager válido
        Debug.LogWarning("Nenhum GameManager encontrado para processar a morte do jogador.");
        enabled = false;
    }

}
