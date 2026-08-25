using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private int enemyCount = 4;

    private void Start()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
    List<Transform> availablePoints = new List<Transform>(spawnPoints);

    for (int i = 0; i < enemyCount; i++)
        {
            int randomIndex = Random.Range(0, availablePoints.Count);

            Transform spawnPoint = availablePoints[randomIndex];

            Instantiate(
                enemyPrefab,
                spawnPoint.position,
                Quaternion.identity
            );

            availablePoints.RemoveAt(randomIndex);
        }
    }
}