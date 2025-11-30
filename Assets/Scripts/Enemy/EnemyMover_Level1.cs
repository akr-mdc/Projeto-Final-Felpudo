using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover_Level1 : MonoBehaviour
{
    public float speed = 4f;

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DestroyZone"))
        {
            Destroy(gameObject);
        }
    }
}
