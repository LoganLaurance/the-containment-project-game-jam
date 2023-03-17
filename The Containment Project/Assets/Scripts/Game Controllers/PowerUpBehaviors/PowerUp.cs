//------------------------------------------------------
//
//  File: PowerUp.cs
//  By: Logan Laurance
//  Last Edited: 3.16.2023
//  Description: Responsible for what type the PowerUp is and what it gives.
//
//------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    [Tooltip("Value that should be given to the player.")]public int value;
    protected GameObject player;
    virtual public void UpdateStats()
    {

    }

    private void Update()
    {
        // If player is on powerup and attacks to select it, call UpdateStats and then clear everything else.
        if(player != null && Input.GetButtonDown("Fire1"))
        {
            UpdateStats();
            GameManager.Instance.ClearPowerUps();
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
