//------------------------------------------------------
//
//  File: DamagePowerUp.cs
//  By: Logan Laurance
//  Last Edited: 3.17.2023
//  Description: Gives temporary damage boost to player.
//
//------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePowerUp : PowerUp
{
    override public void UpdateStats()
    {
        shooter player = FindObjectOfType<shooter>(); // Player is the only one supposed to have this script.

        player.bulletDamage += value;

        GameManager.Instance.UpdatePlayerTempStats();
    }
}
