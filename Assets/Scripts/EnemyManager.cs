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
    [Header("Variables")]
    [SerializeField]
    EnemyDifficulty enemyDifficulty;

    [Header("References")]
    [SerializeField]
    private GameObject humanPrefab;

    [Header("Spawn Points List")]
    [SerializeField]
    private GameObject[] spawnPoints;


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) { StartLevel(_GM.gameDifficulty); }
    }

    /// <summary>
    /// This function will be called when a level loaded. It determines how many enemies will spawn according to difficulty, and then spawns them using SpawnHumans();
    /// </summary>
    /// <param name="_difficulty"></param>
    public void StartLevel(GameDifficulty _difficulty)
    {
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("Spawnpoints returned Null"); 
            return;
        }
            
        int spawnCount = 0;

        enemyDifficulty = (EnemyDifficulty)_difficulty;

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

    /// <summary>
    /// This function goes through the spawnpoints list at random and spawns a human in choosen spawnpoint. It will not spawn in a spawn point that has already been used.
    /// </summary>
    /// <param name="_spawnCount"></param>
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

            if (spawnPoint != null) 
            { 
                GameObject spawnedHuman = Instantiate(humanPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
                spawnedHuman.GetComponent<Human>().Initialize(enemyDifficulty);
            }
            else { Debug.LogWarning("Spawn point at count " + usingSpawnPoint + "is null."); }

        }
    }


}
