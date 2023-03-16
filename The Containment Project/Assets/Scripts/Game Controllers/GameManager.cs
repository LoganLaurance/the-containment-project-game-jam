//------------------------------------------------------
//
//  File: GameManager.cs
//  By: Logan Laurance
//  Last Edited: 3.15.2023
//  Description: Serves as mainly an interface with other scripts. Holds perma-perks and currency.
//
//------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region [Public Variables]
    public static GameManager Instance;

    [Header("Wave Settings")]
    [Tooltip("How long the player must survive in here to clear and prevent spawning in waves. Enter time in seconds.")]public float levelTimerLimit;
    [Tooltip("The delay in between each wave spawn. Enter time in seconds.")]public float waveDelay;
    [Tooltip("The delay in between when enemies spawn from each other in a given wave. Enter time in seconds.")] public float spawnDelay;
    [Tooltip("Limits how many enemies are allowed on screen at once.")] public int enemyCap;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
