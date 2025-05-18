using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum GameState
{
    Start,
    Playing,
    Death,
}

public enum GameDifficulty
{
    Easy,
    Medium,
    Hard,
}

public class GameManager : SingletonDontDestroy<GameManager>
{
    [Header("Player Health")]
    [SerializeField]
    private float health = 100f;

    [Header("Dev View")]
    [SerializeField]
    private float starveValue = 1f;
    [SerializeField]
    private float maxHealth;
    public GameState gameState;
    public GameDifficulty gameDifficulty;

    [Header("References")]
    [SerializeField]
    private GameObject playerOverlay; 
    [SerializeField]
    private Image healthBar;
    [SerializeField]
    private TextMeshProUGUI difficulty;
    [SerializeField]
    private TextMeshProUGUI timer;
    

    void Start()
    {
        maxHealth = health;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q)) {StartLevel(gameDifficulty);}

        timer.text = "Time: " + _GT.gameTime;
        healthBar.fillAmount = health / 100;


        if (gameState == GameState.Playing)
        {
            float imaStarveValue = starveValue;
            if (_CM.isSprinting == true) imaStarveValue *= 10;

            if (health > maxHealth)
                health = maxHealth;

            Starving(imaStarveValue);
        }
    }

    public void UpdateDiffuculty(int _difficultyScore)
    {
        switch (_difficultyScore)
        {
            case 0:
                gameDifficulty = GameDifficulty.Easy; break;
            case 1:
                gameDifficulty = GameDifficulty.Medium; break;
            case 2:
                gameDifficulty = GameDifficulty.Hard; break;
        }

        switch (gameDifficulty)
        {
            case GameDifficulty.Easy:
                starveValue = 1f;
                break;
            case GameDifficulty.Medium:
                starveValue = 1.5f;
                break;
            case GameDifficulty.Hard:
                starveValue = 2f;
                break;
        }

        difficulty.text = "Difficulty: " + gameDifficulty.ToString();
    }

    private void Starving(float imaStarveValue)
    {
        if (health <= 0)
        {
            health = 0;
            Die();
            return;
        }
        health -= imaStarveValue * Time.deltaTime;
    }

    public void Eating(int food)
    {
        health += food;
    }

    public void BeenHit(int _damage)
    {
        health -= _damage;
    }
    private void Die()
    {
        gameState = GameState.Death;
        _CM.Death();
    }

    public void StartLevel (GameDifficulty _LevelDifficulty)
    {
        playerOverlay.gameObject.SetActive(true);
        _EM.StartEnemies(_LevelDifficulty);
        _TM.StartTrash(_LevelDifficulty);

    }
}
