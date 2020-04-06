using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{ 
    [Header("Movement Settings")]
    [SerializeField, Tooltip("The max speed of the player")]
    //Speed of the character
    private float speed = 4f;
    //Make an animator reference for the animations
    private Animator animator;
    //health manager reference
    private HealthManager health;
    //weapon agent reference
    private WeaponAgent wpnAgent;
    //playeragent handler
    private PlayerAgent playerAgent;

    //private bool to determine if the player is alive or not
    private bool isPlayerAlive;
    //private rigidbody - so we can access it properly.
    private Rigidbody rb;
    //private bool to determine if user pressed spacebar to jump.
    private bool isSpacePressed;


    /// <summary>
    /// Initialize some variables needed in script
    /// </summary>
    private void Start()
    {
        health = GetComponent<HealthManager>();
        wpnAgent = GetComponent<WeaponAgent>();
        playerAgent = GetComponent<PlayerAgent>();
    }

    /// <summary>
    /// Initialize more variables needed in script
    /// </summary>
    private void Awake()
    {
        //grab the animator component from the prefab
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        isSpacePressed = false;
        isPlayerAlive = true;
    }

    /// <summary>
    /// Update function, runs every frame.
    /// </summary>
    private void Update()
    {
        if (isPlayerAlive)
        {
            Movement();
            //calls the look at camera function
            LookAtCamera();
            Jumping();
        }
    }

    /// <summary>
    /// Calls OnDeath, sets bool to false
    /// </summary>
    public void OnDeath()
    {
        isPlayerAlive = false;
    }


    /// <summary>
    /// Function used to make the character look at the mouse cursor
    /// </summary>
    void LookAtCamera()
    {
        //generate some raycasting variables and grab the position of the mouse
        Plane planeThing = new Plane(Vector3.up, Vector3.zero);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float distance = 20.0f;

        //checks if the raycast hits, if it does, moves the character to look at it
        if(planeThing.Raycast(ray, out distance))
        {
            Vector3 cameraLook = ray.GetPoint(distance);
            transform.LookAt(new Vector3(cameraLook.x, transform.position.y, cameraLook.z));
        }
    }

    /// <summary>
    /// Function for jumping
    /// </summary>
    void Jumping()
    {
        //Jumping of character using the spacebar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isSpacePressed = true;
        }
        else
        {
            isSpacePressed = false;
        }

        if(isSpacePressed == true)
        {
            animator.SetBool("isSpace", true);
        }
        else
        {
            animator.SetBool("isSpace", false);
        }
    }

    /// <summary>
    /// Movement function - Enables the character to move.
    /// </summary>
    void Movement()
    {
        //moves player using WASD or arrow keys
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        //Clamps the input so we don't move too fast
        input = Vector3.ClampMagnitude(input, 1f);
        //multiply the input by our speed
        input *= speed;
        //apply the X and Z values of the input to the animator's parameters
        animator.SetFloat("Horizontal", input.x);
        animator.SetFloat("Vertical", input.z);
    }

    /// <summary>
    /// Calls the heal function from HealthManager script
    /// </summary>
    /// <param name="heal"></param>
    public void HealPlayer(float heal)
    {
        health.Heal(heal);
    }

    /// <summary>
    /// Calls the damage function from the HealthManager script
    /// </summary>
    /// <param name="damageTaken"></param>
    public void DamagePlayer(float damageTaken)
    {
        health.DamageToPlayer(damageTaken);
    }


}