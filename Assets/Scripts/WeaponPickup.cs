using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : PickupBase
{
    //variable to call WeaponHandler stuff
    private WeaponHandler wpnHandler;
    private PlayerAgent playerAgent;

    private GameObject g;
    private GameObject weapon;

    private int wpnType;

    protected override void Awake()
    {
        base.Awake();
        g = GameObject.FindGameObjectWithTag("Player");
        playerAgent = g.GetComponent<PlayerAgent>();
        wpnHandler = GetComponentInChildren<WeaponHandler>();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnPickUp(PlayerController player)
    {
        EquipWeapon();
        //references PickUpBase script as parent
        base.OnPickUp(player);
    }

    private void EquipWeapon()
    {
        //calls the EquipWeapon function from WeaponAgent
        playerAgent.EquipWeapon(wpnHandler.name);
    }

    //calls the base on trigger to ensure it gets called
    protected override void OnTriggerEnter(Collider other)
    { 
        base.OnTriggerEnter(other);
    }
}
