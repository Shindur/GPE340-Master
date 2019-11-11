using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : PickupBase
{
    //healing variable
    [SerializeField, Tooltip("How much the player heals")]
    protected float healAmount = 10.0f;

    protected override void OnPickUp(PlayerController player)
    {
        //calls the HealPlayer function below
        HealPlayer(player);
        //references PickUpBase script as parent
        base.OnPickUp(player);
    }

    //calls into the PlayerController script for the HealPlayer function there
    private void HealPlayer(PlayerController player)
    {
        player.HealPlayer(healAmount);
    }
}
