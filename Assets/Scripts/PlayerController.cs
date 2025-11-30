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
        if (currentHealth <= 0) Die();
    }

    void Die()
    {
        var lm = FindObjectOfType<LevelManager>();
        if (lm != null) lm.OnPlayerDefeated();
        enabled = false;
    }
}

