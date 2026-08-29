using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private bool isPlayerInside = false;
    [SerializeField] private bool hasSpawned = false;
    [SerializeField] private bool upgradeShown = false;

    [SerializeField] private List<GameObject> enemiesAlive = new List<GameObject>();

    [SerializeField] private EnemySpawner meleeSpawner;
    [SerializeField] private EnemySpawner rangeSpawner;
    [SerializeField] private GameObject portal;
    [SerializeField] private UpgradeManager upgradeManager;
    [SerializeField] private bool showUpgrade = true;

    void Start()
    {
        meleeSpawner = transform.Find("MeleeSpawner").GetComponent<EnemySpawner>();
        rangeSpawner = transform.Find("RangeSpawner").GetComponent<EnemySpawner>();

        portal.SetActive(false);
    }

    void Update()
    {
        // Spawn enemies when player enters room
        if (!hasSpawned && isPlayerInside)
        {
            if (meleeSpawner != null)
            {
                enemiesAlive.AddRange(meleeSpawner.SpawnEnemies());
            }

            if (rangeSpawner != null)
            {
                enemiesAlive.AddRange(rangeSpawner.SpawnEnemies());
            }

            hasSpawned = true;
        }

        // All enemies are dead
        if (hasSpawned && enemiesAlive.Count == 0 && !upgradeShown)
        {
            upgradeShown = true;

            if (showUpgrade)
            {
                upgradeManager.ShowUpgrades(this);
            }
            else
            {
                portal.SetActive(true);
            }
        }
    }

    public void UpgradeFinished()
    {
        portal.SetActive(true);
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInside = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInside = false;
        }

        if (collision.CompareTag("Enemy"))
        {
            enemiesAlive.Remove(collision.gameObject);
        }
    }
}