//------------------------------------------------------
//
//  File: GameManager.cs
//  By: Logan Laurance
//  Last Edited: 3.21.2023
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
    [Tooltip("Provide all temporary power up prefabs in here.")] public List<GameObject> powerUps;
    [HideInInspector] public bool resetStats = false;
    #endregion

    #region [Private Variables]
    private WaveSpawner ws; // For convenience.
    private GameObject player; // For convenience.
    private int currency;

    private float permaHealthBoost;
    private float permaSpeedBoost;
    private float permaDamageBoost;

    private float defPlayerMaxHealth;
    private float defPlayerSpeed;
    private float defPlayerDamage;

    private float tempPlayerMaxHealth;
    private float tempPlayerSpeed;
    private float tempPlayerDamage;
    #endregion
    private void Awake()
    {
        if (Instance)
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
        permaDamageBoost = 1.0f;

        player = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (ws == null)
        {
            ws = FindObjectOfType<WaveSpawner>();
        }
        if (player == null && FindObjectOfType<playerMovement>())
        {
            player = FindObjectOfType<playerMovement>().gameObject;
        }
        if (resetStats && player != null)
        {
            ResetPlayerStats();
        }
    }

    public void SpawnPowerUp(int reward)
    {
        Debug.Log("Spawning in temporary power ups.");

        // Spawn in temporary power ups and give currency.
        if (powerUps == null)
        {
            Debug.LogError("Cannot spawn any power ups because our list is empty.");
            return;
        }
        else
        {
            float pos = -(powerUps.Count / 2.0f) + 0.5f;
            for (int i = 0; i < powerUps.Count; i++)
            {
                if (powerUps[i].GetComponent<PowerUp>().isPerma == false) // Spawn only temporary.
                {
                    Instantiate(powerUps[i], new Vector3((pos + i) * 1.5f, 0.0f, 0.0f), Quaternion.identity, transform);
                }
            }
        }
        currency += reward;
    }
    /// <summary>
    /// Clears out remaining powerups. Should only be called when the player has selected a powerup. Also enables the 
    /// transition triggers.
    /// </summary>
    public void ClearPowerUps()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.layer == 8) // If the object connected is marked as a PowerUp
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }

        LevelTransition[] triggers = FindObjectsOfType<LevelTransition>();

        for(int i = 0; i < triggers.Length; i++)
        {
            triggers[i].EnableTriggers();
        }
    }

    public void UpdatePlayerPermaStats()
    {
        player.GetComponent<playerMovement>().maxPlayerHealth = defPlayerMaxHealth * permaHealthBoost;
        player.GetComponent<playerMovement>().runspeed = defPlayerSpeed * permaSpeedBoost;
        player.GetComponent<shooter>().bulletDamage = defPlayerDamage * permaDamageBoost;
    }

    private void ResetPlayerStats()
    {
        resetStats = false;

        permaHealthBoost = 1.0f;
        permaSpeedBoost = 1.0f;
        permaDamageBoost = 1.0f;

        defPlayerMaxHealth = player.GetComponent<playerMovement>().maxPlayerHealth;
        defPlayerSpeed = player.GetComponent<playerMovement>().runspeed;
        defPlayerDamage = player.GetComponent<shooter>().bulletDamage;
    }
    #region [Accessors And Mutators]
    public int GetCurrency()
    {
        return currency;
    }
    public void SetCurrency(int value)
    {
        currency = value;
    }
    public void AddHealthBoost(float value)
    {
        permaHealthBoost += value;
    }
    public void AddSpeedBoost(float value)
    {
        permaSpeedBoost += value;
    }
    public void AddDamageBoost(float value)
    {
        permaDamageBoost += value;
    }
    public void AddPlayerHealth(float value)
    {
        player.GetComponent<playerMovement>().maxPlayerHealth += value;
        player.GetComponent<playerMovement>().playerHealth = player.GetComponent<playerMovement>().maxPlayerHealth;
    }
    public void AddPlayerSpeed(float value)
    {
        player.GetComponent<playerMovement>().runspeed += value;
    }
    public void AddPlayerDamage(float value)
    {
        player.GetComponent<shooter>().bulletDamage += value;
    }
    #endregion
}
