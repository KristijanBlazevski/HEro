using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject parentObj;
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

            GameObject enemy = Instantiate(
                enemyPrefab,
                spawnPoint.position,
                Quaternion.identity,
                parentObj.transform
            );
            
            availablePoints.RemoveAt(randomIndex);
        }
    }
}