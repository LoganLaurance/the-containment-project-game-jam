//------------------------------------------------------
//
//  File: SpeedPowerUp.cs
//  By: Logan Laurance
//  Last Edited: 3.17.2023
//  Description: Gives temporary speed boost to player.
//
//------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : PowerUp
{
    override public void UpdateStats()
    {
        playerMovement player = FindObjectOfType<playerMovement>();

        player.runspeed += value;

        GameManager.Instance.UpdatePlayerTempStats();
    }
}
