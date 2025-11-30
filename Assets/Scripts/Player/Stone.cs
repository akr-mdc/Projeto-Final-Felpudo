using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    public int damage = 5;
    public float lifeTime = 5f;
    public GameObject hitEffect;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Pedra atinge o Boss
        if (collision.gameObject.CompareTag("Boss"))
        {
            Boss_Level3 boss = collision.gameObject.GetComponent<Boss_Level3>();
            if (boss != null)
            {
                boss.TakeDamage(damage);
            }
            else
            {
                Debug.LogWarning("Stone hit a Boss but Boss_Level3 component not found on " + collision.gameObject.name);
            }

            if (hitEffect != null)
                Instantiate(hitEffect, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }

        // Pedra atinge o chão
        else if (collision.gameObject.CompareTag("Ground"))
        {
            if (hitEffect != null)
                Instantiate(hitEffect, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }

}


