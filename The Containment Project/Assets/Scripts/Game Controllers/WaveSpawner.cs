//------------------------------------------------------
//
//  File: WaveSpawner.cs
//  By: Logan Laurance
//  Last Edited: 3.14.2023
//  Description: Handles spawning in and regulating enemy waves.
//
//------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    #region [Public Variables]
    public int enemyCap;

    public static WaveSpawner Instance;
    #endregion

    #region [Private Variables]
    private GameManager gm; // For convenience
    private Transform[] spawnPositions;
    private bool isWaveFinished = false;
    private float levelTimer;
    #endregion

    private void Awake()
    {
        if(Instance)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;

        levelTimer = gm.levelTimerLimit;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!isWaveFinished)
        {
            // Figure out how to check all the current enemies we have spawned in.
            // Send out waves in sections, not all together. Also send out individual waves over time so player is not
            // immediately overwhelmed. (Keep looping with one type of wave)
            levelTimer -= Time.deltaTime;
            if (levelTimer <= 0.0f) // When player has survived the time limit.
            {
                ClearLevel();
            }
        }
    }

    private void SpawnWave()
    {
        Debug.Log("Spawning new wave");
        // Spawn the next wave.
    }

    /// <summary>
    /// Clears out the current level and prevents further spawns.
    /// </summary>
    private void ClearLevel()
    {
        Debug.Log("Clearing level");
        isWaveFinished = true;
        // Destroy all enemies in the level.
    }
}
