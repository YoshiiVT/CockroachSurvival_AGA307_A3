using UnityEngine;
using System.Collections.Generic;

public enum EnemyDifficulty
{
    Easy,
    Medium,
    Hard,
}

public class EnemyManager : Singleton<EnemyManager>
{
    EnemyDifficulty enemyDifficulty;

    [SerializeField]
    private GameObject[] spawnPoints;

    [SerializeField]
    private GameObject humanPrefab;

    public void Start()
    {
        StartLevel(enemyDifficulty);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartLevel(EnemyDifficulty _difficulty)
    {
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("Spawnpoints returned Null"); 
            return;
        }
            
        int spawnCount = 0;

        enemyDifficulty = _difficulty;

        switch (enemyDifficulty)
        {
            case EnemyDifficulty.Easy:
                spawnCount = 2;
                break;
            case EnemyDifficulty.Medium:
                spawnCount = 4;
                break;
            case EnemyDifficulty.Hard:
                spawnCount = 5;
                break;

        }

        SpawnHumans(spawnCount);
    }

    private void SpawnHumans(int _spawnCount)
    {
        List<int> usedSpawnPoints = new List<int>();

        for (int i = 0; i < _spawnCount; i++)
        {
            int usingSpawnPoint;
            //This logic avoids reusing the same spawnpoint more than once
            do
            {
                usingSpawnPoint = Random.Range(0, spawnPoints.Length);
            }
            
            while (usedSpawnPoints.Contains(usingSpawnPoint) && usedSpawnPoints.Count < spawnPoints.Length);
            // the do/while loop will do everything in do everytime it is run while while is true

            usedSpawnPoints.Add(usingSpawnPoint);

            GameObject spawnPoint = spawnPoints[usingSpawnPoint];

            if (spawnPoint != null) { Instantiate(humanPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation); }
            else { Debug.LogWarning("Spawn point at count " + usingSpawnPoint + "is null."); }

        }
    }


}
