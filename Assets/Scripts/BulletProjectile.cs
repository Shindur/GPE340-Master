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

    public void SetBulletRange(float range)
    {
        //sets the time to the projectile range
        timer = range / 60f;
    }

    private void HandleTimer()
    {
        //if timer is 0
        if(timer <= 0)
        {
            DestroyBullet();
        }

        //if timer is not less than 0, decrement it
        timer -= Time.deltaTime;
    }

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

        DestroyBullet();

    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
