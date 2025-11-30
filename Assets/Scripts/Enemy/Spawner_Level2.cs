using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner_Level2 : MonoBehaviour
{
    [Header("Enemy Settings")]
    public GameObject enemyPrefab;

    [Header("Spawn Timing")]
    public float spawnInterval = 1.5f;
    public float randomOffset = 0.3f;

    private float timer;

    void Start()
    {
        ResetTimer();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SpawnEnemy();
            ResetTimer();
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }

    void ResetTimer()
    {
        timer = spawnInterval + Random.Range(-randomOffset, randomOffset);

        // Garante que nunca fique muito rápido
        if (timer < 0.2f)
            timer = 0.2f;
    }
}