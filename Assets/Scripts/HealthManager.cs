using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

public class HealthManager : MonoBehaviour
{
    //the next 2 variables are related to player health, as indicated by the tooltip
    [SerializeField, Tooltip("Max player health")]
    protected float maxHealth = 100.0f;
    [SerializeField, Tooltip("Initial Player Health")]
    protected float startingHealth = 30.0f;

    [Tooltip("When someone dies")]
    public UnityEvent OnDie;

    private float currentHealth;

    [SerializeField, Tooltip("How long till the body disappears")]
    private float deathTimer;

    //For handling the actual countdown of the death timer
    private float deathCountdown;

    //private bools for the character and enemy
    private bool isPlayerDead;
    private bool isEnemyDead;

    //sets a variable for the player's health that's read only
    public float OverallHealth
    {
        get { return currentHealth; }
    }

    //sets a variable for the percentage of the player's health that is read only
    public float OverallHealthPercent
    {
        get { return currentHealth / maxHealth; }
    }

    // Start is called before the first frame update
    void Start()
    {
        //death countdown set to 0
        deathCountdown = 0;
        //enemy and player start alive
        isPlayerDead = false;
        isEnemyDead = false;
        currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        DeathCountdown();
    }

    /// <summary>
    /// Heals the player and does not go over max health
    /// </summary>
    /// <param name="heal"></param>
    public void Heal(float heal)
    {
        currentHealth = Mathf.Clamp(currentHealth + heal, 0f, maxHealth);
    }

    /// <summary>
    /// Damages the player and does not go below 0
    /// </summary>
    /// <param name="damageTaken"></param>
    public void DamageToPlayer(float damageTaken)
    {
        //deals damage to player and enemies and clamps it to 0
        currentHealth = Mathf.Clamp(currentHealth - damageTaken, 0f, maxHealth);

        if(currentHealth == 0)
        {
            //calls the OnDie event to happen
            OnDie.Invoke();
            //destroy the character in question
            DestroyCharacter();
        }

    }

    /// <summary>
    /// for setting the stage for destroying the character
    /// </summary>
    private void DestroyCharacter()
    {
        //if it's the player set the bool to true
        if (gameObject.layer == 8)
        {
            isPlayerDead = true;
            //start the death countdown
            deathCountdown = deathTimer;
        }
        //if it's the enemy set the bool to true
        if(gameObject.layer == 9)
        {
            isEnemyDead = true;
            //start the death countdown
            deathCountdown = deathTimer;
        }

    }

    /// <summary>
    /// decrements deathcountdown every frame and destroys once 0 and appropriate layer
    /// </summary>
    private void DeathCountdown()
    {
        //decrement the deathtimer
        if(deathCountdown > 0)
        {
            deathCountdown -= Time.deltaTime;
        }
        //if player destroy
        if (deathCountdown <= 0 && isPlayerDead == true)
        {
            Debug.Log("Game over suckah!");
            Destroy(gameObject);
        }
        //if enemy destroy
        if (deathCountdown <= 0 && isEnemyDead == true)
        {
            //show enemy killed and destroy
            Debug.Log("Enemy Down!");
            Destroy(gameObject);
        }
    }

}
