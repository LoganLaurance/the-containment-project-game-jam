//------------------------------------------------------
//
//  File: HealthPowerUp.cs
//  By: Logan Laurance
//  Last Edited: 3.17.2023
//  Description: Gives temporary health boost to player.
//
//------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerUp : PowerUp
{
    override public void UpdateStats()
    {
        playerMovement player = FindObjectOfType<playerMovement>();

        player.maxPlayerHealth += value;
        player.playerHealth = player.maxPlayerHealth;

        GameManager.Instance.UpdatePlayerTempStats();
    }
}