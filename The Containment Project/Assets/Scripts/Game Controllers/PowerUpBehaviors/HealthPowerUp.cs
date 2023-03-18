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
    override public void UpdateTempStats()
    {
        gm.AddPlayerHealth(value);
    }

    public override void UpdatePermaStats(int currency)
    {
        if(currency < price)
        {
            Debug.Log("Not enough currency to pay for this!");
            return;
        }
        else
        {
            gm.SetCurrency(currency - price);
            gm.SetHealthBoost(value);
        }
    }
}
