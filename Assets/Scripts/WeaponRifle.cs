using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRifle : WeaponHandler
{

    protected override void Awake()
    {
        base.Awake();
    }

    /// <summary>
    /// override the shoot function
    /// </summary>
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

    /// <summary>
    /// override the release function
    /// but since it's auto anyway, doesn't really matter
    /// </summary>
    public override void Release()
    {
        ;
    }
}
