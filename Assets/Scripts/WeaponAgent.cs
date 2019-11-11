using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public abstract class WeaponAgent : MonoBehaviour
{
    [Header("Settings for the weapons")] 
    [SerializeField, Tooltip("The area in which the gun will be located")]
    protected Transform wpnTransform;
    [SerializeField, Tooltip("The weapon to be equipped")]
    protected List<GameObject> wpnList;
    
    //For the Animator component
    protected Animator animator;
    //For the currently equipped weapon
    protected GameObject equippedWeapon;
    //Transform of the weapon
    protected Transform equippedTransform;
    //WeaponHandler reference variable
    [HideInInspector]
    public WeaponHandler wpnHandler;
    //for the animator controller
    protected int wpnType;

    protected virtual void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        wpnHandler = GetComponent<WeaponHandler>();
    }

    //Used to equip a weapon
    public virtual void EquipWeapon(string name)
    {
        //unequip current weapon first
        UnequipWeapon();
        //cycles through the list of weapons.. all 2
        foreach (GameObject weapon in wpnList)
        {
            if (weapon.name == name)
            {
                //puts the weapon in the players hands
                equippedWeapon = Instantiate(weapon, wpnTransform.position, wpnTransform.rotation) as GameObject;
                //sets the game object as a parent of the game object
                equippedWeapon.transform.SetParent(wpnTransform);
                //puts the weapon on the same layer as the player
                equippedWeapon.layer = gameObject.layer;
                //grab the component of the equipped weapon
                wpnHandler = equippedWeapon.GetComponent<WeaponHandler>();
                //grab the type of gun from the enum list
                wpnType = (int) wpnHandler.wpnType;
                //tell the animator we have a gun equipped
                animator.SetInteger("WpnAniType", wpnType);
                equippedTransform = equippedWeapon.GetComponent<Transform>();
            }
        }
        
    }

    //unequips currently equipped weapon when called
    //will only be called when changing weapons
    public void UnequipWeapon()
    {
        if(equippedWeapon)
        {
            Destroy(equippedWeapon.gameObject);
            equippedWeapon = null;
            wpnType = 0;
        }
    }
    //handles IK animations for weapons
    protected virtual void OnAnimatorIK()
    {
        //exits function if no weapon eqipped
        if(!equippedWeapon)
        {
            return;
        }

        WeaponHandler handler = equippedWeapon.GetComponent<WeaponHandler>();

        //if weapon has IK handler for right hand
        if(handler.RightHandIK())
        {
            animator.SetIKPosition(AvatarIKGoal.RightHand, handler.RightHandIKTrans().position);

            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);

            animator.SetIKRotation(AvatarIKGoal.RightHand, handler.RightHandIKTrans().rotation);

            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);
        }

        //this runs through if equipped weapon has no IK for right hand
        else
        {
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0f);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0f);
        }

        //IK handler for left hand
        if (handler.LeftHandIK())
        {
            animator.SetIKPosition(AvatarIKGoal.LeftHand, handler.LeftHandIKTrans().position);

            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);

            animator.SetIKRotation(AvatarIKGoal.LeftHand, handler.LeftHandIKTrans().rotation);

            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);
        }

        //this runs through if equipped weapon has no IK for Left hand
        else
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0f);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0f);
        }
    }

    //Mainly for AI after this point

    //Allows AI to shoot
    protected abstract void PullTrigger();

    //Stops AI from shooting
    protected abstract void ReleaseTrigger();
}
