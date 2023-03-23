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
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region [Public Variables]
    public static GameManager Instance;
    [Tooltip("How many text elements that we need for the game UI. This is a bad method to do!")]public int textElements = 6;
    [Header("Power Ups")]
    [Tooltip("Provide all temporary power up prefabs in here.")] public List<GameObject> powerUps;
    [HideInInspector] public bool hardResetStats = false;
    [HideInInspector] public bool changedLevels = false;
    #endregion

    #region [Private Variables]
    private WaveSpawner ws; // For convenience.
    private GameObject player; // For convenience.
    private int currency;
    private TMPro.TextMeshProUGUI health;
    private TMPro.TextMeshProUGUI speed;
    private TMPro.TextMeshProUGUI damage;
    private TMPro.TextMeshProUGUI waveTimer;
    private TMPro.TextMeshProUGUI currentCurrency;
    private TMPro.TextMeshProUGUI enemiesRemaining;

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

        if(currentCurrency != null && enemiesRemaining != null && ws != null)
        {
            UpdateWaveUIText();
        }

        if(health != null && speed != null && damage != null && waveTimer != null
            && currentCurrency != null && enemiesRemaining != null)
        {
            UpdateUIStatsText();
        }
    }

    private void FixedUpdate()
    {
        if (changedLevels && player != null)
        {
            UpdatePlayerTempStats();
        }
        if (hardResetStats && player != null)
        {
            HardResetPlayerStats();
        }
        if (player != null && (health == null || speed == null || damage == null || waveTimer == null
            || currentCurrency == null || enemiesRemaining == null))
        {
            GrabUIText();
        }
    }

    private void UpdateWaveUIText()
    {
        waveTimer.text = Mathf.RoundToInt(ws.levelTimer).ToString();
        enemiesRemaining.text = "Enemies: " + ws.CurrentEnemiesSpawned().ToString();
    }

    public void UpdateUIStatsText()
    {
        health.text = "Health: " + player.GetComponent<playerMovement>().playerHealth.ToString();
        speed.text = "Speed: " + player.GetComponent<playerMovement>().runspeed.ToString();
        damage.text = "Damage: " + player.GetComponent<shooter>().bulletDamage.ToString();
        currentCurrency.text = "Currency: " + currency.ToString();
    }

    private void GrabUIText()
    {
        Canvas canvas = FindObjectOfType<Canvas>();
        if(canvas.gameObject.transform.childCount < textElements) // If there is not enough elements in the canvas, don't grab them.
        {
            return;
        }

        health = canvas.gameObject.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>();
        speed = canvas.gameObject.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>();
        damage = canvas.gameObject.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>();
        waveTimer = canvas.gameObject.transform.GetChild(3).GetComponent<TMPro.TextMeshProUGUI>();
        currentCurrency = canvas.gameObject.transform.GetChild(4).GetComponent<TMPro.TextMeshProUGUI>();
        enemiesRemaining = canvas.gameObject.transform.GetChild(5).GetComponent<TMPro.TextMeshProUGUI>();

        Debug.Log("Please find a better method to do this before the project ends!");
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

        LevelTransition[] triggers = FindObjectsOfType<LevelTransition>();

        for (int i = 0; i < triggers.Length; i++)
        {
            triggers[i].EnableTriggers();
        }

        currency += reward;
    }
    /// <summary>
    /// Clears out remaining powerups. Should only be called when the player has selected a powerup.
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
    }

    public void UpdatePlayerPermaStats()
    {
        player.GetComponent<playerMovement>().maxPlayerHealth = defPlayerMaxHealth * permaHealthBoost;
        player.GetComponent<playerMovement>().playerHealth = defPlayerMaxHealth * permaHealthBoost;
        player.GetComponent<playerMovement>().runspeed = defPlayerSpeed * permaSpeedBoost;
        player.GetComponent<shooter>().bulletDamage = defPlayerDamage * permaDamageBoost;
    }

    public void UpdateInternalTempStats()
    {
        if (player != null)
        {
            tempPlayerMaxHealth = player.GetComponent<playerMovement>().maxPlayerHealth;
            tempPlayerSpeed = player.GetComponent<playerMovement>().runspeed;
            tempPlayerDamage = player.GetComponent<shooter>().bulletDamage;
        }
        else
            Debug.LogError("Player cannot be found.");
    }

    public void UpdatePlayerTempStats()
    {
        Debug.Log("UpdatePlayerTempStats() called");
        changedLevels = false;

        player.GetComponent<playerMovement>().maxPlayerHealth = tempPlayerMaxHealth;
        player.GetComponent<playerMovement>().playerHealth = tempPlayerMaxHealth;
        player.GetComponent<playerMovement>().runspeed = tempPlayerSpeed;
        player.GetComponent<shooter>().bulletDamage = tempPlayerDamage;
    }

    private void HardResetPlayerStats()
    {
        Debug.Log("HardResetPlayerStats() called");
        hardResetStats = false;

        permaHealthBoost = 1.0f;
        permaSpeedBoost = 1.0f;
        permaDamageBoost = 1.0f;

        defPlayerMaxHealth = player.GetComponent<playerMovement>().maxPlayerHealth;
        defPlayerSpeed = player.GetComponent<playerMovement>().runspeed;
        defPlayerDamage = player.GetComponent<shooter>().bulletDamage;

        tempPlayerMaxHealth = defPlayerMaxHealth;
        tempPlayerSpeed = defPlayerSpeed;
        tempPlayerDamage = defPlayerDamage;
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
