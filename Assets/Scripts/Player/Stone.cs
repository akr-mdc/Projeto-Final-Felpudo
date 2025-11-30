using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    public int damage = 5;                  // Quanto dano a pedra causa ao Boss
    public float lifeTime = 5f;             // Destrói automaticamente após x segundos
    public GameObject hitEffect;            // Opcional: efeito de partículas ao colidir

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Pedra atinge o Boss
        if (collision.gameObject.CompareTag("Boss"))
        {
            if (GameManager_Level3.Instance != null)
            {
                GameManager_Level3.Instance.TakeBossDamage(damage);
            }
            if (hitEffect != null) Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        // Pedra atinge o chão
        else if (collision.gameObject.CompareTag("Ground"))
        {
            if (hitEffect != null) Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}

