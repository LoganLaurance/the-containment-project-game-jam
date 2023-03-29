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
    override public void UpdateTempStats()
    {
        playerMovement player = FindObjectOfType<playerMovement>();
        if (player != null)
        {
            if(player.runspeed >= player.speedCap)
            {
                Debug.Log("Cannot give this because we are at capped value.");
                return;
            }
        }
        gm.AddPlayerSpeed(value);
        gm.UpdateInternalTempStats();
        gm.UpdateUIStatsText();
    }

    public override void UpdatePermaStats(int currency)
    {
        playerMovement player = FindObjectOfType<playerMovement>();
        if (currency < price)
        {
            Debug.Log("Not enough currency to pay for this!");
            return;
        }
        else if(player != null)
        {
            if(player.runspeed >= player.speedCap)
            {
                Debug.Log("Cannot buy this as we are at capped value.");
                return;
            }
        }
        gm.SetCurrency(currency - price);
        gm.AddSpeedBoost(value);
        gm.UpdatePlayerPermaStats();
    }
}
