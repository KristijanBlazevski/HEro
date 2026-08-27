using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private bool isPlayerInside = false;
    [SerializeField] private bool hasSpawned = false;
    [SerializeField] List<GameObject> enemiesAlive = new List<GameObject>();
    [SerializeField] EnemySpawner meleeSpawner;
    [SerializeField] EnemySpawner rangeSpawner;
    [SerializeField] GameObject portal;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        meleeSpawner = transform.Find("MeleeSpawner").GetComponent<EnemySpawner>();
        rangeSpawner = transform.Find("RangeSpawner").GetComponent<EnemySpawner>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!hasSpawned && isPlayerInside)
        {
            if(meleeSpawner != null)
            {
                enemiesAlive.AddRange(meleeSpawner.SpawnEnemies());
            }

            if(rangeSpawner != null)
            {
                enemiesAlive.AddRange(rangeSpawner.SpawnEnemies());
            }

            hasSpawned = true;
        }

        if(hasSpawned && enemiesAlive.Count == 0)
        {
            portal.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInside= true;
        }
        //Kasni spawnot vo trigger i zatoa hasSpawned e true i enemies use ne se spawnale
        // if (collision.CompareTag("Enemy"))
        // {
        //     enemiesAlive.Add(collision.gameObject);
        // }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
         if (collision.CompareTag("Player"))
        {
            isPlayerInside= false;
        }

        if (collision.CompareTag("Enemy"))
        {
            enemiesAlive.Remove(collision.gameObject);
        }
    }
}
