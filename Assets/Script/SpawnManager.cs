using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab for the enemy to spawn
    private float spawnRange = 9.0f; // Range within which enemies can spawn

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {        
        Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation); // Spawns an enemy at the origin
    }
     
    private Vector3 GenerateSpawnPosition ()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ); // Random position within the spawn range
        return randomPos;
    }
}
