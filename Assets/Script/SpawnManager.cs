using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab for the enemy to spawn
    public GameObject powerupPrefab;
    private float spawnRange = 9.0f; // Range within which enemies can spawn
    public int enemyCount; // Total number of enemies in the game
    public int waveNumber = 1; // Current wave number

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start ()
    {
        SpawnEnemyWave(waveNumber);
        GeneratePowerup();
    }

    void Update ()
    {
        // Fix: Use FindObjectsOfType instead of FindObjectOfType to get all instances of Enemy
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (enemyCount == 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
            GeneratePowerup();
            if (waveNumber == 10) // Check if the player has won the game
            {
                ShowGameWinPanel(); // Show the game win panel
            }
        }
    }
    private void ShowGameWinPanel ()
    {
        GameObject.Find("GameWinManager").GetComponent<UIManager>().ShowGameOver();
    }

    private void GeneratePowerup ()
    {
        Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation); // Spawn a power-up at a random position
    }

    private void SpawnEnemyWave ( int enemiesToSpawn )
    {
        for (int i = 0; i < enemiesToSpawn; i++) // Spawns 5 enemies at the start
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation); // Spawns an enemy at the origin position
        }
    }

    private Vector3 GenerateSpawnPosition ()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ); // Random position within the spawn range
        return randomPos;
    }
}
