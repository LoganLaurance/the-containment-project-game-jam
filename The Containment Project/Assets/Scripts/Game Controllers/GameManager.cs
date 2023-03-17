//------------------------------------------------------
//
//  File: GameManager.cs
//  By: Logan Laurance
//  Last Edited: 3.17.2023
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
    [Header("Power Ups")]
    [Tooltip("Provide all temporary power up prefabs in here.")]public List<GameObject> powerUps;
    #endregion

    #region [Private Variables]
    private WaveSpawner ws; // For convenience.
    private int currency;

    private float permaHealthBoost;
    private float permaSpeedBoost;
    private float permaDamageBoost;

    private float playerMaxHealth;
    private float playerSpeed;
    private float playerDamage;
    private float tempMaxPlayerHealth;
    private float tempPlayerSpeed;
    private float tempPlayerDamage;
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
        // Set them to default when first loaded in.
        permaHealthBoost = 1.0f;
        permaSpeedBoost = 1.0f;

        GameObject player = FindObjectOfType<playerMovement>().gameObject;

        playerMaxHealth = player.GetComponent<playerMovement>().maxPlayerHealth;
        playerSpeed = player.GetComponent<playerMovement>().runspeed;
        playerDamage = player.GetComponent<shooter>().bulletDamage;
        tempMaxPlayerHealth = player.GetComponent<playerMovement>().maxPlayerHealth;
        tempPlayerSpeed = player.GetComponent<playerMovement>().runspeed;
        tempPlayerDamage = player.GetComponent<shooter>().bulletDamage;
    }

    // Update is called once per frame
    void Update()
    {
        if(ws == null)
        {
            ws = FindObjectOfType<WaveSpawner>();
        }
    }

    public void SpawnPowerUp()
    {
        Debug.Log("Spawning in temporary power ups.");

        // Spawn in temporary power ups and give currency.
        if(powerUps == null)
        {
            Debug.LogError("Cannot spawn any power ups because our list is empty.");
            return;
        }
        else
        {
            float pos = -(powerUps.Count / 2.0f) + 0.5f;
            for(int i = 0; i < powerUps.Count; i++)
            {
                Instantiate(powerUps[i], new Vector3(pos + i, 0.0f, 0.0f), Quaternion.identity, transform);
            }
        }
        currency += 50;
    }
    /// <summary>
    /// Clears out remaining powerups. Should only be called when the player has selected a powerup.
    /// </summary>
    public void ClearPowerUps()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).gameObject.layer == 8) // If the object connected is marked as a PowerUp
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
    }

    public void UpdatePlayerPermaStats()
    {
        GameObject player = FindObjectOfType<playerMovement>().gameObject;

        playerMaxHealth *= permaHealthBoost;
        playerSpeed *= permaSpeedBoost;
        playerDamage *= permaDamageBoost;

        player.GetComponent<playerMovement>().maxPlayerHealth = playerMaxHealth;
        player.GetComponent<playerMovement>().playerHealth = playerMaxHealth;
        player.GetComponent<playerMovement>().runspeed = playerSpeed;
        player.GetComponent<shooter>().bulletDamage = playerDamage;
    }

    public void UpdatePlayerTempStats()
    {
        GameObject player = FindObjectOfType<playerMovement>().gameObject;

        tempMaxPlayerHealth = player.GetComponent<playerMovement>().maxPlayerHealth;
        tempPlayerSpeed = player.GetComponent<playerMovement>().runspeed;
        tempPlayerDamage = player.GetComponent<shooter>().bulletDamage;
    }
}
