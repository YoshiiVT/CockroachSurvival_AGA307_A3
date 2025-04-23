using UnityEngine;

public enum EnemyDifficulty
{
    Easy,
    Medium,
    Hard,
    Killer
}

public class EnemyManager : Singleton<EnemyManager>
{

    EnemyDifficulty enemyDifficulty;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
