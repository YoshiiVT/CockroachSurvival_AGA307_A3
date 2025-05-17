using System.Collections.Generic;
using UnityEngine;

public class TrashManager : Singleton<TrashManager>
{
    [Header("References")]
    [SerializeField]
    private GameObject smallTrashPrefab;
    [SerializeField]
    private GameObject mediumTrashPrefab;
    [SerializeField]
    private GameObject largeTrashPrefab;

    [Header("Spawn Points List")]
    [SerializeField]
    private GameObject[] trashPoints;

    /// <summary>
    /// This function was adapted from Enemy Manager, it will run on load. Specifying how much of each trash spawns on load.
    /// </summary>
    /// <param name="_difficulty"></param>
    public void StartTrash(GameDifficulty _difficulty)
    {
        if (trashPoints == null || trashPoints.Length == 0)
        {
            Debug.LogError("Spawnpoints returned Null");
            return;
        }

        int smallTrashCount = 5;
        int mediumTrashCount = 5;
        int largeTrashCount = 5;

            switch (_difficulty)
        {
            case GameDifficulty.Easy:
                smallTrashCount = 5;
                mediumTrashCount = 5;
                largeTrashCount = 5;
                break;
            case GameDifficulty.Medium:
                smallTrashCount = 4;
                mediumTrashCount = 4;
                largeTrashCount = 3;
                break;
            case GameDifficulty.Hard:
                smallTrashCount = 3;
                mediumTrashCount = 2;
                largeTrashCount = 1;
                break;
        }

        SpawnTrash(smallTrashCount, mediumTrashCount, largeTrashCount);
    }

    /// <summary>
    /// This function was adapted from Enemy Manager, it will instantiate trash on load. Starting with the Largest Trash, spawning as many as required. Then Medium, then Small.
    /// </summary>
    /// <param name="_difficulty"></param>
    private void SpawnTrash(int _smallTrashCount, int _mediumTrashCount, int _largeTrashCount)
    {
        List<int> usedSpawnPoints = new List<int>();

        int totalSpawnCount = _smallTrashCount + _mediumTrashCount + _largeTrashCount;

        for (int i = 0; i < totalSpawnCount; i++)
        {
            int usingSpawnPoint;
            
            do
            {
                usingSpawnPoint = Random.Range(0, trashPoints.Length);
            }
            while (usedSpawnPoints.Contains(usingSpawnPoint) && usedSpawnPoints.Count < trashPoints.Length);

            usedSpawnPoints.Add(usingSpawnPoint);

            GameObject trashPoint = trashPoints[usingSpawnPoint];

            if (trashPoint != null)
            {
                GameObject spawnedTrash;
                if (_largeTrashCount != 0)
                {
                    spawnedTrash = Instantiate(largeTrashPrefab, trashPoint.transform.position, trashPoint.transform.rotation);
                    _largeTrashCount--;
                }
                if (_mediumTrashCount != 0)
                {
                    spawnedTrash = Instantiate(mediumTrashPrefab, trashPoint.transform.position, trashPoint.transform.rotation);
                    _mediumTrashCount--;
                }
                if (_smallTrashCount != 0)
                {
                    spawnedTrash = Instantiate(smallTrashPrefab, trashPoint.transform.position, trashPoint.transform.rotation);
                    _smallTrashCount--;
                }

            }
            else { Debug.LogWarning("Spawn point at count " + usingSpawnPoint + "is null."); }
        }
    }

    

    /// <summary>
    /// This function goes through the spawnpoints list at random and spawns a human in choosen spawnpoint. It will not spawn in a spawn point that has already been used.
    /// </summary>
    /// <param name="_spawnCount"></param>
    
}
