using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

//makes sure attached gameobject has health manager
[RequireComponent(typeof(HealthManager))]
//makes sure attached gameobject has colliders
[RequireComponent(typeof(Collider))]
//makes sure attached gameobject has rigidbodys
[RequireComponent(typeof(Rigidbody))]
//makes sure attached gameobject has an animator
[RequireComponent(typeof(Animator))]

public class RagdollController : MonoBehaviour
{
    //variable for the mainbody rigidbody
    private Rigidbody mainRigidBody;
    //variable for the mainbody collider
    private Collider mainColl;
    //variable for the animator
    private Animator animator;
    //variable for the AI's nav mesh
    private NavMeshAgent meshAgent;

    //array of rigidbodies on the ragdoll
    private Rigidbody[] childRBS;
    //array of the colliders on the ragdoll
    private Collider[] childColl;

    private void Awake()
    {
        //grab the main rigidbody
        mainRigidBody = gameObject.GetComponent<Rigidbody>();
        //grab the main collider
        mainColl = gameObject.GetComponent<Collider>();
        //grab the animator
        animator = gameObject.GetComponent<Animator>();
        //grab the navmesh for the AI
        meshAgent = gameObject.GetComponent<NavMeshAgent>();
        //fill the array with the child rigid bodies
        childRBS = gameObject.GetComponentsInChildren<Rigidbody>();
        //fill the array with the child colliders
        childColl = gameObject.GetComponentsInChildren<Collider>();

        DeactiveRagdoll();
    }

    /// <summary>
    /// when player or enemy dies, activate the ragdoll
    /// </summary>
    public void ActivateRagdoll()
    {
        //go through each rigidbody child, set them to not be kinematic
        foreach (Rigidbody rigidbody in childRBS)
        {
            rigidbody.isKinematic = false;
        }
        //go through each collider child, set them to true
        foreach(Collider collider in childColl)
        {
            collider.enabled = true;
        }
        //set the animator to false
        animator.enabled = false;
        //set the main body to be kinematic
        mainRigidBody.isKinematic = true;
        //set the main collider to not be activated
        mainColl.enabled = false;

        //check if the body has a navmesh
        if (meshAgent != null)
        {
            //if it has one, deactivate it
            meshAgent.enabled = false;
        }

    }
    /// <summary>
    /// For when the ragdoll needs to be deactivated, assumed for when lives are introduced
    /// </summary>
    private void DeactiveRagdoll()
    {
        //go through each rigidbody child, set them to be kinematic
        foreach (Rigidbody rigidbody in childRBS)
        {
            rigidbody.isKinematic = true;
        }
        //go through each collider child, set them to false
        foreach (Collider collider in childColl)
        {
            collider.enabled = false;
        }
        //set the animator to true
        animator.enabled = true;
        //set the main body to not be kinematic
        mainRigidBody.isKinematic = false;
        //set the main collider to be activated
        mainColl.enabled = true;

        //check if the body has a navmesh
        if (meshAgent != null)
        {
            //if it has one, activate it
            meshAgent.enabled = true;
        }

    }

    /// <summary>
    /// for the health script to reference when the player/enemy dies
    /// activates the ragdoll function
    /// </summary>
    private void IfDead()
    {
        ActivateRagdoll();
    }


}