using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAgent : WeaponAgent
{

    protected override void Awake()
    {
        base.Awake();
    }

    // Update is called once per frame
    void Update()
    {
        weaponFunction();
    }

    /// <summary>
    /// for handling the weapons 
    /// </summary>
    private void weaponFunction()
    {
        //If we push down on the fire command, fire gun
        if (Input.GetAxis("Fire") > 0)
        {
            if (wpnHandler != null)
            {
                PullTrigger();
            }
        }
        //If we let go of fire command, stop shooting
        if(Input.GetButtonUp("Fire"))
        {
            if(wpnHandler != null)
            {
                ReleaseTrigger();
            }
        }
    }

    /// <summary>
    /// override the pulltrigger, just to call it for the player
    /// </summary>
    protected override void PullTrigger()
    {
        wpnHandler.Shoot();
    }
    /// <summary>
    /// override the releasetrigger, just to call it for the player
    /// </summary>
    protected override void ReleaseTrigger()
    {
        wpnHandler.Release();
    }

}
