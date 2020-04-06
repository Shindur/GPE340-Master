using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class EnemyChase : WeaponAgent
{

    [SerializeField, Tooltip("Enemy's speed multiplyer")]
    private float enemySpeed;
    [SerializeField, Tooltip("Enemy's fire rate, but only 1 type of weapon anyway so")]
    private float enemyFireRate;
    //For the nav mesh agent, grabbed in awake
    private NavMeshAgent meshyMesh;

    //transform for the target of the AI, in this case the player
    private Transform targetPlayer;
    //transform of the AI itself
    private Transform aiTransform;


    protected override void Awake()
    {
        meshyMesh = GetComponent<NavMeshAgent>();
        aiTransform = gameObject.GetComponent<Transform>();
        base.Awake();
        //set the movement of the AI to the controller
        animator.SetFloat("enemyRunSpeed", enemySpeed);
        //equips random gun to AI
        AIEquipRandomWeapon();
    }
    // Start is called before the first frame update
    void Start()
    {
        //grab the transform of the player
        targetPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(meshyMesh.enabled)
        {
            //handles the AI movement
            AIMovement();
            //handles AI animations
            AIAnimations();
            //handles AI aiming
            AIAiming();
            //handles AI shooting
            AIShooting();
        }
    }

    /// <summary>
    /// Allows the AI to shoot
    /// </summary>
    protected override void PullTrigger()
    {
        wpnHandler.Shoot();
    }

    /// <summary>
    /// Stops AI from shooting
    /// </summary>
    protected override void ReleaseTrigger()
    {
        wpnHandler.Release();
    }

    /// <summary>
    /// Allows the AI to move towards the player
    /// </summary>
    private void AIMovement()
    {
        if(!targetPlayer)
        {
            //stops nav mesh
            meshyMesh.isStopped = true;
            //stops the animator from moving AI
            animator.SetFloat("Horizontal", 0f);
            animator.SetFloat("Vertical", 0f);
            //stops function from running
            return;
        }

        //if not in range of player, move towards them
        MoveToPlayer();
    }

    /// <summary>
    /// called to move AI to player
    /// </summary>
    private void MoveToPlayer()
    {
        meshyMesh.SetDestination(targetPlayer.position);
    }

    /// <summary>
    /// For AI animations
    /// </summary>
    private void AIAnimations()
    {
        Vector3 input = meshyMesh.desiredVelocity;
        input = transform.InverseTransformDirection(input);
        animator.SetFloat("Horizontal", input.x);
        animator.SetFloat("Vertical", input.z);
    }

    /// <summary>
    /// Used in aiding AI to move
    /// </summary>
    private void OnAnimatorMove()
    {
        meshyMesh.velocity = animator.velocity;
    }

    /// <summary>
    /// For AI aiming at player
    /// </summary>
    private void AIAiming()
    {
        //create a vector for the target to aim at
        Vector3 aiTarget = new Vector3(targetPlayer.position.x, equippedTransform.position.y + .001f, targetPlayer.position.z);
        //create a quaternion using the vector for rotation
        Quaternion targetPlayerRotation = Quaternion.LookRotation(aiTarget - equippedTransform.position);
        //rotate the weapon towards the player to aim at
        equippedTransform.rotation = Quaternion.RotateTowards(equippedTransform.rotation, targetPlayerRotation, meshyMesh.angularSpeed * Time.deltaTime);
    }

    /// <summary>
    /// /handles the shooting, which is just auto in this case anyway
    /// </summary>
    private void AIShooting()
    {
        //if AI is within range of player
        if(IsAIInRange())
        {
            //if AI can see the player
            if(CanAISeePlayer())
            {
                //fire the gun
                PullTrigger();
            }
        }
        //ai is not within range of player
        else
        {
            //don't fire the guns
            ReleaseTrigger();
        }
    }

    /// <summary>
    /// checks if AI is within range of player
    /// </summary>
    /// <returns></returns>
    private bool IsAIInRange()
    {
        //checks if we're within the range of the player, set in inspector
        if(Vector3.Distance(aiTransform.position, targetPlayer.position) <= wpnHandler.AIRange())
        {
            //if we are, returns true
            return true;
        }
        else
        {
            //if we're not, returns false
            return false;
        }
    }

    /// <summary>
    /// checks if AI can see the player
    /// </summary>
    /// <returns></returns>
    private bool CanAISeePlayer()
    {
        //reference of the angle between player and AI
        Vector3 targetPlayerDirection = targetPlayer.position - aiTransform.position;

        //if the player is within AI shooting angle
        if(Vector3.Angle(targetPlayerDirection, aiTransform.forward) <= wpnHandler.AIAngle())
        {
            //if we are, return true
            return true;
        }
        else
        {
            //if we're not, return false
            return false;
        }
    }

    /// <summary>
    /// have enemy equip random weapons at start
    /// </summary>
    private void AIEquipRandomWeapon()
    {
        EquipWeapon(wpnList[Random.Range(0, wpnList.Count)].GetComponent<WeaponHandler>().name);
    }
}
