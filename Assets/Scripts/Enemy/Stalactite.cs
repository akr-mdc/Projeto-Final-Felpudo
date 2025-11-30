using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalactite : MonoBehaviour
{
    public float fallSpeed = 5f;
    public GameObject hitEffect;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.down * fallSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager_Level3.Instance.TakeDamage();
            SpawnEffect();
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            SpawnEffect();
            Destroy(gameObject);
        }
    }

    void SpawnEffect()
    {
        if (hitEffect != null)
            Instantiate(hitEffect, transform.position, Quaternion.identity);
    }
}





