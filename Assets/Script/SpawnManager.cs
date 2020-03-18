using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private bool _stopSpawning = false;
    // enemy spawning related
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _spawnedEnemies; // to collect the enemies
    
    // power-up spawning ralated
    [SerializeField] private GameObject[] _powerUp;
    [SerializeField] private GameObject _spawnedPowerUps; // to collect the powerUps
    
    // to start the enemy wave
    public void StartEnemyWave()
    {
        StartCoroutine(EnemySpawnRoutine());

        StartCoroutine(PowerUpSpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        // check that the astroid is still exists
    }
    
    IEnumerator EnemySpawnRoutine()
    {
        yield return new WaitForSeconds(2.0f);
        while (_stopSpawning == false) 
        {
            EnemySpawn();
            yield return new WaitForSeconds(3.0f); // spawn game objects every 3 sec
        }        
    }

    // to spawn the enemies
    private void EnemySpawn() 
    {
        Vector3 enemyPosSpawn = new Vector3 (Random.Range(-8.0f, 8.0f), 7.0f, 0);
        GameObject newEnemy = Instantiate(_enemyPrefab, enemyPosSpawn, Quaternion.identity);
        newEnemy.transform.parent = _spawnedEnemies.transform; // to spawn the enemies under a game object
    }

    IEnumerator PowerUpSpawnRoutine() 
    {
        yield return new WaitForSeconds(6.0f);
        while (_stopSpawning == false) 
        {
            PowerUpSpawn();
            yield return new WaitForSeconds(20);
        }
    }

    // to spawn the power ups
    private void PowerUpSpawn () 
    {
        Vector3 powerUpSpawnPos = new Vector3 (Random.Range(-8.0f,8.0f), 7.0f, 0.0f);
        GameObject newPowerUp = Instantiate(_powerUp[Random.Range(0, 3)], powerUpSpawnPos, Quaternion.identity);
        newPowerUp.transform.parent = _spawnedPowerUps.transform;
    }

    public void StopSpawning() 
    {
        _stopSpawning = true;
    }
}