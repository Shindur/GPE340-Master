using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRifle : WeaponHandler
{

    protected override void Awake()
    {
        base.Awake();
    }

    //override the shoot function
    public override void Shoot()
    {
        if (canShoot)
        {
            //can fire the bullet
            Fire();
            //set the timer determined by firing rate
            bulletTimer = (60f / rateOfFire);
            //Cannot shoot again
            canShoot = false;
        }
    }

    //override the release function
    //but since it's auto anyway, doesn't really matter
    public override void Release()
    {
        ;
    }
}
