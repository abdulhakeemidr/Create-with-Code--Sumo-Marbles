using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;
    public GameObject[] enemyPrefabClones;
    public int EnemyCount;
    public int waveNumber = 1;

    private float SpeedCeiling = 8f;
    private float spawnRange = 9.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        // takes the waveNumber from the editor and spawns a number
        // of enemies based on the number given when the game starts
        SpawnWave(waveNumber);
        // spawns the powerupPrefab on startup
        Instantiate(powerupPrefab, RandomPosition(), powerupPrefab.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        // Finds out how many Enemies are currently in the scene
        // i.e. how many enemy prefab instances
        EnemyCount = FindObjectsOfType<EnemyBehaviour>().Length;

        // if there are no enemies in the scene and the initial waveNumber is less
        // than 5. This if-else determines if the player has completed a wave (by remaining in the dojo)
        if(EnemyCount == 0 && waveNumber < 5)
        {
            // increases the number of enemies to be spawned by 1
            waveNumber++;
            // spawns that new number of enemies
            SpawnWave(waveNumber);
            // spawns a new powerUp prefab with those enemies to assist the player
            
            //if(waveNumber % 2 != 0)
                Instantiate(powerupPrefab, RandomPosition(), powerupPrefab.transform.rotation);
        }
        else if(EnemyCount == 0) // if the last number of enemies spawned was 5
        {
            // waveNumber is not incremented. 5 enemies are respawned for the new wave
            // the space in the dojo is limited so cannot spawn too many
            SpawnWave(waveNumber);
            // a powerUp prefab is spawned every wave
            Instantiate(powerupPrefab, RandomPosition(), powerupPrefab.transform.rotation);
        }
    }

    void SpawnWave(int enemiesToSpawn)
    {
        // spawns an initial number of enemies when the game starts based on
        // the number passed to "enemiesToSpawn"
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            // spawns every enemy on a random position
            Instantiate(enemyPrefab, RandomPosition(), enemyPrefab.transform.rotation);
        }

        UIManager.instance.roundNum++;
    }

    // returns a Vector3 random position in the X & Z axis to spawn the ball i.e.
    // spawn the ball somewhere on the platform
    private Vector3 RandomPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        Vector3 RandomPosition = new Vector3(spawnPosX, 0, spawnPosZ);
        return RandomPosition;
    }
}