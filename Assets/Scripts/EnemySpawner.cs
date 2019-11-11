using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField, Tooltip("For spawning enemies")]
    private GameObject[] enemyList;
    [SerializeField, Tooltip("max number of enemy spawns")]
    public int maxEnemySpawns;
    [SerializeField, Tooltip("spawn delay of the enemies")]
    private float spawnDelay;

    //store the spawn points locally
    private List<Transform> spawnList;
    //check how many enemies are currently on scene
    private int currentEnemyCount;


    private void Awake()
    {
        PopulateSpawns();
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 0f, spawnDelay);
    }

    private void SpawnEnemy()
    {
        //check if we have enough enemies, if we do then kick out the function
        if(currentEnemyCount >= maxEnemySpawns)
        {
            return;
        }

        //grab the transform of a spawn point
        Transform randoSpawn = RandomSpawn();
        //instantiates a new enemy
        GameObject enemy = Instantiate(enemyList[Random.Range(0, enemyList.Length)], randoSpawn.position, randoSpawn.rotation) as GameObject;

        //add 1 to the counter
        currentEnemyCount++;
        //check if/when an enemy dies
        enemy.GetComponent<HealthManager>().OnDie.AddListener(CheckIfDead);

    }

    //returns a random spawn point from the list for the enemies to spawn at
    private Transform RandomSpawn()
    {
        return spawnList[Random.Range(0, spawnList.Count)];
    }

    //populate the spawn point list
    private void PopulateSpawns()
    {
        spawnList = new List<Transform>();

        //cycle through all children of this gamecomponent and add to the list
        foreach(Transform spawnPoint in transform)
        {
            spawnList.Add(spawnPoint);
        }
    }

    //if enemy dies, decrement the total number
    private void CheckIfDead()
    {
        currentEnemyCount--;
    }
}
