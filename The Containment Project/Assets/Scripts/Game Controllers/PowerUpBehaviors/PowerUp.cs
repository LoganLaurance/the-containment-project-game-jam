//------------------------------------------------------
//
//  File: PowerUp.cs
//  By: Logan Laurance
//  Last Edited: 3.17.2023
//  Description: Responsible for what type the PowerUp is and what it gives.
//
//------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    [Tooltip("How much this powerup should cost.")] public int price;
    [Tooltip("Value that should be given to the player. If supposed to be a Permanent Perk, treat value as multiplier (i.e: " +
        "1.05 for a 5% increase.) Otherwise, treat it as an additive value (i.e: 50 to increase stat by 50.)")]public float value;
    [Tooltip("Check this if you intend this powerup to be a permanent powerup, otherwise leave it unchecked.")] public bool isPerma;
    protected GameObject player;
    protected GameManager gm; // For convenience.
    virtual public void UpdateTempStats()
    {
    }

    virtual public void UpdatePermaStats(int currency)
    {
    }

    private void Start()
    {
        gm = GameManager.Instance;
    }

    private void Update()
    {
        // If player is on powerup and attacks to select it, call UpdateStats and then clear everything else.
        if(player != null && Input.GetButtonDown("Fire1"))
        {
            if(!isPerma)
            {
                UpdateTempStats();
                gm.ClearPowerUps();
            }
            else if(isPerma)
            {
                UpdatePermaStats(gm.GetCurrency());
                gm.UpdatePlayerPermaStats();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<playerMovement>() != null)
            player = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player = null;
    }
}
