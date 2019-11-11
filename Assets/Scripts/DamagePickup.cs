﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePickup : PickupBase
{
    //variable to deal damage to player
    [SerializeField, Tooltip("This will hurt the player when picked up")]
    private float damageTaken = 5.0f;

    protected override void OnPickUp(PlayerController player)
    {
        //calls the HealPlayer function below
        DamagePlayer(player);
        //references PickUpBase script as parent
        base.OnPickUp(player);
    }

    //calls into the PlayerController script for the HealPlayer function there
    private void DamagePlayer(PlayerController player)
    {
        player.DamagePlayer(damageTaken);
    }
}