using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Stalactite : MonoBehaviour
{
    public float fallSpeed = 5f;
    public GameObject hitEffect;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 1f;
        rb.velocity = Vector2.down * fallSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Se colidir com o Player ou chão, aplica efeito e destrói a estalactite
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Ground"))
        {
            if (collision.gameObject.CompareTag("Player") && GameManager_Level3.Instance != null)
            {
                GameManager_Level3.Instance.TakeDamage();
            }

            if (hitEffect != null)
                Instantiate(hitEffect, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }

}





