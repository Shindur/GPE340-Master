﻿using System.Collections;
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

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            ActivateRagdoll();
        }
    }

    //when player or enemy dies, activate the ragdoll
    public void ActivateRagdoll()
    {
        
        foreach (Rigidbody rigidbody in childRBS)
        {
            rigidbody.isKinematic = false;
        }
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

    private void DeactiveRagdoll()
    {
        
        foreach (Rigidbody rigidbody in childRBS)
        {
            rigidbody.isKinematic = true;
        }
        foreach (Collider collider in childColl)
        {
            collider.enabled = false;
        }
        //set the animator to false
        animator.enabled = true;
        //set the main body to be kinematic
        mainRigidBody.isKinematic = false;
        //set the main collider to not be activated
        mainColl.enabled = true;

        //check if the body has a navmesh
        if (meshAgent != null)
        {
            //if it has one, deactivate it
            meshAgent.enabled = true;
        }

    }

    //for the health script to reference when the player/enemy dies
    private void IfDead()
    {
        ActivateRagdoll();
    }


}