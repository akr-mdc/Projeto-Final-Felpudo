using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Level2 : MonoBehaviour
{
    public float speed = 2f;
    public int maxHealth = 1;
    private int currentHealth;

    private Transform player;
    private Animator anim;
    private bool isDying = false;
    private Rigidbody2D rb;
    private Collider2D col;

    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (isDying) return;

        if (player != null)
        {
            Vector2 dir = (player.position - transform.position).normalized;
            transform.position += (Vector3)dir * speed * Time.deltaTime;
        }
    }

    // ----------------------------- DANO TOMADO -----------------------------

    public void TakeDamage(int amount)
    {
        if (isDying) return;

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // ----------------------------- MORTE -----------------------------

    void Die()
    {
        isDying = true;

        // Desativa o collider para não causar mais dano
        if (col != null) col.enabled = false;

        // Zera velocidade
        if (rb != null) rb.velocity = Vector2.zero;

        // Toca animação
        if (anim != null)
            anim.Play("Barrel Destroy");

        // Destroi após o fim da animação
        float animLength = anim.GetCurrentAnimatorStateInfo(0).length;
        Destroy(gameObject, animLength);
    }

    // ----------------------------- COLISÕES -----------------------------

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDying) return;

        // Player Hitboxes
        if (collision.CompareTag("PlayerHitbox"))
        {
            TakeDamage(1);
        }

        // Player
        if (collision.CompareTag("Player"))
        {
            GameManager_Level2.Instance.TakeDamage();
            Die(); // inimigo morre ao encostar no player
        }
    }
}
