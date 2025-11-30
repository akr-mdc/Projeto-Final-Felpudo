using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner_Level1 : MonoBehaviour
{
    public GameObject enemyPrefab;

    [Header("Spawn Timing")]
    public float minSpawnTime = 1.0f;
    public float maxSpawnTime = 2.0f;

    [Header("Vertical Variation")]
    public float minY = -2f;
    public float maxY = 2f;

    private float nextSpawnTime = 0f;

    void Start()
    {
        ScheduleNextSpawn();
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            ScheduleNextSpawn();
        }
    }

    void SpawnEnemy()
    {
        Vector3 spawnPos = new Vector3(transform.position.x, Random.Range(minY, maxY), 0f);
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }

    void ScheduleNextSpawn()
    {
        nextSpawnTime = Time.time + Random.Range(minSpawnTime, maxSpawnTime);
    }
}

