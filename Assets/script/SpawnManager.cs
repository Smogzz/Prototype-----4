using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
        public GameObject EnemyPrefab;
        public GameObject Enemy2Prefab;
        public GameObject Enemy3Prefab;
        private float spawnRange = 9;
        public int enemyCount;
        public int waveNumber = 1;
        public GameObject PowerupPrefab;
    // Start is called before the first frame update
    void Start()
    {
       SpawnEnemyWave(waveNumber);
       Instantiate(PowerupPrefab, GenerateSpawnPosition(), PowerupPrefab.transform.rotation);
    }
    
    void SpawnEnemyWave (int enemiesToSpawn)
        {
          for (int i = 0; i < enemiesToSpawn; i++)
          {
            Instantiate(EnemyPrefab, GenerateSpawnPosition(), EnemyPrefab.transform.rotation);
            Instantiate(Enemy3Prefab, GenerateSpawnPosition(), Enemy3Prefab.transform.rotation);

          }  
          
        }
    

    private Vector3 GenerateSpawnPosition ()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    } 

    // Update is called once per frame
    void Update()
    {
      enemyCount = FindObjectsOfType<Enemy>().Length;
      if (enemyCount == 0) 
      {
       waveNumber++;
       SpawnEnemyWave(waveNumber);
       Instantiate(PowerupPrefab,GenerateSpawnPosition(), PowerupPrefab.transform.rotation);
      }
    }
}
