using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickupBase : MonoBehaviour
{
    [SerializeField, Tooltip("Grab the object")]
    protected GameObject pickerup;

    //How fast the object spins for
    [SerializeField, Tooltip("How fast the object spins")]
    protected float spinspeed = 0f;
    //How long till the pickup respawns
    [SerializeField, Tooltip("How long till the pickup/enemy respawns")]
    protected float respawnDelay = 5.0f;
    protected float respawnCountdown = 0f;
 
    //grabs the transform of the gameobject, done below in awake
    protected Transform pickerupTransform;
    //boolean to check if the gameobject is activated
    protected bool isThisActivated;

    protected virtual void Awake()
    {
        pickerupTransform = pickerup.GetComponent<Transform>();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        isThisActivated = true;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        pickerupTransform.transform.Rotate(0, spinspeed, 0, Space.World);
        CheckIfSpawned();
    }

    //When the player hits the object, "destroys" the object, and starts a countdown
    protected virtual void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        //checks if it's the player hitting it
        if (player)
        {
            //checks if it's activated
            if (isThisActivated)
            {
                //calls onpickup below
                OnPickUp(player);
            }
        }
    }

    //works as intended. 
    //sets the gameobject to false, and sets the countdown to work
    protected virtual void OnPickUp(PlayerController player)
    {
        //set the activated to false
        isThisActivated = false;
        //set the countdown
        respawnCountdown = respawnDelay;
        //call poweruphandler, below
        PowerUpHandler();
    }


    protected virtual void CheckIfSpawned()
    {
        //checks if the respawn counter is 0
        //if it is, set bool to true and call powerup function
        if (respawnCountdown <= 0)
        {
            isThisActivated = true;
            PowerUpHandler();
        }
        else
        //if counter not 0, decrease it down till it is
        {
            respawnCountdown -= Time.deltaTime;
        }
    }

    //Checks the bool if the powerup should be activated in the scene
    protected virtual void PowerUpHandler()
    {
        //if the bool is true, make the powerup appear on screen
        if(isThisActivated == true)
        {
            pickerup.SetActive(true);
        }
        //if it's not true, make it disappear from the scene
        else if(isThisActivated == false)
        {
            pickerup.SetActive(false);
        }
    }
}
