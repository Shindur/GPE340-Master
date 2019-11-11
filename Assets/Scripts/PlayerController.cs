using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private bool isSpacePressed;

    //Taken from the lecture
    [SerializeField, Tooltip("The max speed of the player")]
    //Speed of the character
    private float speed = 4f;
    //Make an animator reference for the animations
    private Animator animator;

    private HealthManager health;

    private WeaponAgent wpnAgent;

    //private bool to determine if the player is alive or not
    private bool isPlayerAlive;

    //playeragent handler
    private PlayerAgent playerAgent;

    private void Start()
    {
        health = GetComponent<HealthManager>();
        wpnAgent = GetComponent<WeaponAgent>();
        playerAgent = GetComponent<PlayerAgent>();
    }

    private void Awake()
    {
        //grab the animator component from the prefab
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        isSpacePressed = false;
        isPlayerAlive = true;
    }

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


    public void OnDeath()
    {
        isPlayerAlive = false;
    }


    //Function used to make the character look at the mouse
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

    //Calls the heal function from HealthManager script
    public void HealPlayer(float heal)
    {
        health.Heal(heal);
    }

    //Calls the damage function from the HealthManager script
    public void DamagePlayer(float damageTaken)
    {
        health.DamageToPlayer(damageTaken);
    }


}