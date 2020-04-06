using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : PickupBase
{
    //variable to call WeaponHandler functions
    private WeaponHandler wpnHandler;
    //variable to call playeragent functions
    private PlayerAgent playerAgent;

    //to reference the player gameobject
    private GameObject g;
    //to reference the weapon being used at the time
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

    /// <summary>
    /// Used for equipping the weapon being picked up
    /// </summary>
    /// <param name="player"></param>
    protected override void OnPickUp(PlayerController player)
    {
        EquipWeapon();
        //references PickUpBase script as parent
        base.OnPickUp(player);
    }

    /// <summary>
    /// Aids in the above function
    /// </summary>
    private void EquipWeapon()
    {
        //calls the EquipWeapon function from WeaponAgent
        playerAgent.EquipWeapon(wpnHandler.name);
    }

    /// <summary>
    /// calls the base on trigger to ensure it gets called
    /// </summary>
    /// <param name="other"></param>
    protected override void OnTriggerEnter(Collider other)
    { 
        base.OnTriggerEnter(other);
    }
}
