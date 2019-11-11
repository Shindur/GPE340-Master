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

    //for handling the weapons 
    private void weaponFunction()
    {
        if (Input.GetAxis("Fire") > 0)
        {
            if (wpnHandler != null)
            {
                PullTrigger();
            }
        }

        if(Input.GetButtonUp("Fire"))
        {
            if(wpnHandler != null)
            {
                ReleaseTrigger();
            }
        }
    }

    //override the pulltrigger, just to call it for the player
    protected override void PullTrigger()
    {
        wpnHandler.Shoot();
    }
    //override the releasetrigger, just to call it for the player
    protected override void ReleaseTrigger()
    {
        wpnHandler.Release();
    }

}
