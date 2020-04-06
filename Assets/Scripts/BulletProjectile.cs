using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class BulletProjectile : MonoBehaviour
{
    private Transform tf;

    //for the rigidbody component
    [HideInInspector]
    public Rigidbody body;

    //amount of damage to inflict
    [HideInInspector]
    public float bulletDamage;

    //timer to determine range
    private float timer;

    private void Awake()
    {
        //Grabs the transform component for storage
        tf = gameObject.GetComponent<Transform>();
        //grabs the rigidbody component for storage
        body = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleTimer();
    }

    /// <summary>
    /// sets the time to the projectile range
    /// </summary>
    /// <param name="range"></param>
    public void SetBulletRange(float range)
    {
        //sets the time to the projectile range
        timer = range / 60f;
    }

    /// <summary>
    /// checks if the timer is set to 0, if it is destroy the bullet
    /// if not count down to 0
    /// </summary>
    private void HandleTimer()
    {
        //if timer is 0 destroy the bullet
        if(timer <= 0)
        {
            DestroyBullet();
        }

        //if timer is not less than 0, decrement it
        timer -= Time.deltaTime;
    }

    /// <summary>
    /// When the bullet collides with a player/ai
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        //creates a reference to the health component on hitting object if it has one
        HealthManager checkingHealth = collision.gameObject.GetComponent<HealthManager>();

        //if the collider has health
        if(checkingHealth != null)
        {
            //if it has a collider, it takes damage
            checkingHealth.DamageToPlayer(bulletDamage);
        }

        //destroy the bullet after collision
        DestroyBullet();

    }

    /// <summary>
    /// Function to destroy the bullet when needed
    /// </summary>
    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
