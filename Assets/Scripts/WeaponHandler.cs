using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponHandler : MonoBehaviour
{
    [Header("Components of the weapons")]
    [SerializeField, Tooltip("Name of weapon")]
    protected string nameOfWeapon = "";
    [SerializeField, Tooltip("Left hand placement")]
    protected bool leftIK = false;
    [SerializeField, Tooltip("Right hand placement")]
    protected bool rightIK = false;
    [SerializeField, Tooltip("Position of left hand")]
    protected Transform leftHandTarget;
    [SerializeField, Tooltip("Position of right hand")]
    protected Transform rightHandTarget;

    [Header("Projectile settings")]
    [SerializeField, Tooltip("Bullet Prefab")]
    protected GameObject bullet;
    [SerializeField, Tooltip("Barrel Location on the gun")]
    protected Transform barrelPos;

    [Space(10)]

    [Header("AI variables")]
    [SerializeField, Tooltip("Range for the AI")]
    protected float rangeAI;
    [SerializeField, Tooltip("Angle at which AI shoot, in degrees")]
    private float aiAngleShooting;

    [Space(15)]

    [Header("Weapon Statistics")]

    [SerializeField, Tooltip("Rate of Fire")]
    protected float rateOfFire;
    [SerializeField, Tooltip("Weapon Damage"), Range(1f, 20f)]
    protected float wpnDamage;
    [SerializeField, Tooltip("Range of bullet"), Range(1f, 30f)]
    protected float wpnRange;
    [SerializeField, Tooltip("Weapon Accuracy"), Range(0f, 7.0f)]
    protected float wpnAccuracy;
    [SerializeField, Tooltip("The bullet's velocity")]
    protected float wpnVelocity;

    /// <summary>
    /// enum for the different weapon types
    /// </summary>
    public enum WeaponAnimationType
    {
        None = 0,
        Rifle = 1
    }
    
    [Space(5)]
    //for the changing of weapon types
    public WeaponAnimationType wpnType;

    //to set which weapon it is
    public GameObject weapon;

    //how long between each shot
    protected float bulletTimer;
    //Check to see if the trigger has been released
    protected bool canShoot;

    protected virtual void Awake()
    {
        //Tells the code that the player can shoot
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        ShotTimer();
    }

    /// <summary>
    /// returns the weapons name
    /// </summary>
    /// <returns></returns>
    public string NameOfWeapon()
    {
        return nameOfWeapon;
    }

    /// <summary>
    /// returns the AI's range
    /// </summary>
    /// <returns></returns>
    public float AIRange()
    {
        return rangeAI;
    }

    /// <summary>
    /// returns the AI's angle
    /// </summary>
    /// <returns></returns>
    public float AIAngle()
    {
        return aiAngleShooting;
    }

    /// <summary>
    /// returns weapon rate of fire
    /// </summary>
    /// <returns></returns>
    public float RateOfFire()
    {
        return rateOfFire;
    }

    /// <summary>
    /// returns the left hand IK
    /// </summary>
    /// <returns></returns>
    public bool LeftHandIK()
    {
        return leftIK;
    }

    /// <summary>
    /// returns the right hand IK
    /// </summary>
    /// <returns></returns>
    public bool RightHandIK()
    {
        return rightIK;
    }

    /// <summary>
    /// Next 2 functions returns the transform
    /// of the hands transforms for IK
    /// </summary>
    /// <returns></returns>
    public Transform LeftHandIKTrans()
    {
        return leftHandTarget;
    }
    public Transform RightHandIKTrans()
    {
        return rightHandTarget;
    }

    /// <summary>
    /// To be handled by other scripts
    /// </summary>
    public abstract void Shoot();

    /// <summary>
    /// Used to fire the guns, can be changed by other scripts
    /// </summary>
    protected virtual void Fire()
    {
        //generates prefab of the bullet
        GameObject bulletNew = Instantiate(bullet, barrelPos.position, barrelPos.rotation * Quaternion.Euler(Random.onUnitSphere * wpnAccuracy)) as GameObject;
        //assigns the new bullet to the imported layer
        bulletNew.layer = gameObject.layer;
        //create a reference to the bulletprojectle script
        BulletProjectile bullProj = bulletNew.GetComponent<BulletProjectile>();
        //Set damage, velocity, and launche the bullet
        bullProj.bulletDamage = wpnDamage;
        bullProj.SetBulletRange(wpnRange);
        bullProj.body.AddRelativeForce(Vector3.forward * wpnVelocity, ForceMode.VelocityChange);
    }

    /// <summary>
    /// to be used in the weapon rifle script
    /// </summary>
    public abstract void Release();

    /// <summary>
    /// determines when the weapon is good to shoot
    /// </summary>
    protected virtual void ShotTimer()
    {
        //if the timer is set
        if(bulletTimer > 0)
        {
            //reduces the timer every second
            bulletTimer -= Time.deltaTime;
        }
        //when bulletTimer = 0 or less
        else
        {
            canShoot = true;
        }
    }
}
