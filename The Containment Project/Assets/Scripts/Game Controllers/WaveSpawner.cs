//------------------------------------------------------
//
//  File: WaveSpawner.cs
//  By: Logan Laurance
//  Last Edited: 3.16.2023
//  Description: Handles spawning in and regulating enemy waves.
//
//------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    #region [Public Variables]
    [Header("Wave Settings")]
    [Tooltip("How long the player must survive in here to clear and prevent spawning in waves. Enter time in seconds.")] public float levelTimerLimit;
    [Tooltip("The delay in between each wave spawn. Enter time in seconds.")] public float waveDelay;
    [Tooltip("The delay in between when enemies spawn from each other in a given wave. Enter time in seconds.")] public float spawnDelay;
    [Tooltip("Limits how many enemies are allowed on screen at once.")] public int enemyCap;
    [Tooltip("Parent GameObject that holds all spawning positions goes here.")] public GameObject spawnPosList;
    [Tooltip("Parent GameObject that points to where all the enemies spawned in will be at.")]public GameObject spawnList;
    [Tooltip("Prefab list of all enemy types. Please order this from weakest to strongest.")]public List<GameObject> enemyPrefabs;
    [Min(1.0f)]
    [Tooltip("Scalar value that is used to affect enemies stats. Defaulted to 1 for base enemy stats.")] public float scalar = 1.0f;
    #endregion
    #region [Private Variables]
    private GameManager gm; // For convenience
    private Transform[] spawnPositions;
    private bool isWaveFinished = false;
    private float levelTimer;
    private float waveDelayTimer;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;

        levelTimer = levelTimerLimit;
        waveDelayTimer = waveDelay - 2.0f; // Add 2 seconds of delay to the first wave spawn to have the player familiarize with the level.
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(spawnPositions == null)
        {
            UpdateSpawnPositions();
        }
        if(!isWaveFinished)
        {
            levelTimer -= Time.deltaTime;
            if (levelTimer <= 0.0f) // When player has survived the time limit.
            {
                ClearLevel();
            }

            waveDelayTimer += Time.deltaTime;
            if(waveDelayTimer >= waveDelay)
            {
                StartCoroutine(SpawnNewWave());
                waveDelayTimer = 0.0f;
            }
        }
    }

    IEnumerator SpawnNewWave()
    {
        if (enemyPrefabs.Count == 0) // If no enemy prefabs are available
        {
            Debug.LogError("Cannot spawn in any enemies from an empty list.");
            yield break;
        }
        for (int i = 0; i < enemyCap; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void SpawnEnemy()
    {
        //Debug.Log("Spawning in enemy");
        // Spawn in new enemy.
        int randEnemy = Random.Range(0, 100);
        if(randEnemy <= 50) // If landed in lower half of range, set it to spawn in the first enemy in the list.
        {
            randEnemy = 0;
        }
        else
        {
            float equalChance = 50.0f / enemyPrefabs.Count;
            randEnemy = Random.Range(0, 50);
            for(int i = 1; i < enemyPrefabs.Count; i++) // Start at 1 because we exclude the first element handled earlier.
            {
                if(randEnemy >= equalChance * (i - 1) && randEnemy < equalChance * i) // If randEnemy matches to the enemy element in its range.
                {
                    randEnemy = i;
                    break;
                }
            }
            if(randEnemy >= enemyPrefabs.Count) // If larger than array size, we know it's at the end of array so set to end of array.
            {
                randEnemy = enemyPrefabs.Count - 1;
            }
        }
        int randSpawnPos = Random.Range(0, spawnPosList.transform.childCount);
        GameObject enemy = Instantiate(enemyPrefabs[randEnemy], spawnPositions[randSpawnPos].transform.position, 
            Quaternion.identity, spawnList.transform);

        // Spawn in enemy with scaled stats.
        enemy.GetComponent<enemyBehavior>().enemyHealth *= scalar;
        enemy.GetComponent<enemyBehavior>().enemyDamage *= scalar;
    }

    /// <summary>
    /// Clears out the current level and prevents further spawns.
    /// </summary>
    private void ClearLevel()
    {
        isWaveFinished = true;
        // Destroy all enemies in the level.
        int spawnsLength = spawnList.transform.childCount;
        for(int i = 0; i < spawnsLength; i++)
        {
            Destroy(spawnList.transform.GetChild(i).gameObject);
        }

        gm.SpawnPowerUp();
    }

    public void UpdateSpawnPositions()
    {
        int spawnerLength = spawnPosList.transform.childCount;
        spawnPositions = new Transform[spawnerLength];
        for (int i = 0; i < spawnerLength; i++)
        {
            spawnPositions[i] = spawnPosList.transform.GetChild(i).GetComponent<Transform>();
        }
    }
}
