using System;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    // Singleton instance
    public static CombatManager Instance { get; private set; }

    [Header("Enemy Settings")]
    [SerializeField] private GameObject enemyPrefab = null;
    [SerializeField] private List<SpawnEntry> enemyList = new();

    [Header("Spawn Settings")]
    [SerializeField] private float spawnDelay = 5f;
    [SerializeField] private float initialDelay = 2f;

    [Header("Loot")]
    [SerializeField] private List<Item> loot = new();
    [SerializeField] private int moneyAmount = 0;

    // Internal counters
    private int currentEntryIndex = 0;
    private int spawnedOfCurrent = 0;

    private void Awake()
    {
        // Singleton setup
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("Multiple CombatManager instances detected! Destroying duplicate.");
            Destroy(gameObject);
            return;
        }

        Instance = this;
        // Optional: persist between scenes
        // DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // Basic validation
        if (enemyPrefab == null)
        {
            Debug.LogWarning("CombatManager: enemyPrefab is null. Spawning disabled.");
            return;
        }

        if (enemyList.Count <= 0)
        {
            Debug.Log("CombatManager: enemyList is empty. Spawning disabled.");
            return;
        }

        // Start spawning
        InvokeRepeating(nameof(SpawnEnemies), initialDelay, Mathf.Max(0.01f, spawnDelay));
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(SpawnEnemies));
    }

    private void SpawnEnemies()
    {
        // Check if we've reached the end of the list
        if (currentEntryIndex >= enemyList.Count)
        {
            CancelInvoke(nameof(SpawnEnemies));
            return;
        }

        // Spawn one enemy of the current type
        SpawnEntry entry = enemyList[currentEntryIndex];

        // Calculate floor-aligned position
        float enemyHeight = entry.character.size; // assuming size is total height
        float spawnY = transform.position.y + enemyHeight / 2f;

        GameObject clone = Instantiate(enemyPrefab, new Vector2(transform.position.x, spawnY), Quaternion.identity);
        clone.transform.localScale = Vector3.one * entry.character.size; // scale uniformly
        clone.GetComponent<Character>().Initialize(entry.character);

        spawnedOfCurrent++;

        // If we've spawned enough of this type, move to next entry
        if (spawnedOfCurrent >= entry.amount)
        {
            currentEntryIndex++;
            spawnedOfCurrent = 0;
        }

        // Stop if we just finished the last entry
        if (currentEntryIndex >= enemyList.Count)
        {
            CancelInvoke(nameof(SpawnEnemies));
        }
    }


    // Optional: allow restarting from code
    public void StartSpawning()
    {
        if (enemyPrefab == null || enemyList.Count <= 0) return;

        // Reset counters
        currentEntryIndex = 0;
        spawnedOfCurrent = 0;

        InvokeRepeating(nameof(SpawnEnemies), initialDelay, Mathf.Max(0.01f, spawnDelay));
    }

    public void AddLoot(Item item, int money = 0)
    {
        moneyAmount += money;
        if (item != null)
        {
            loot.Add(item);
        }
    }

    public void PlayerDied()
    {
        CancelInvoke(nameof(SpawnEnemies));
    }
}

[Serializable]
public class SpawnEntry
{
    public CharacterStats character;
    public int amount;
}
