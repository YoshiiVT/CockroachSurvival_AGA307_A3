using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum GameState
{
    Start,
    Playing,
    Death,
    Score
}

public enum GameDifficulty
{
    Starting,
    Easy,
    Medium,
    Hard,
    Expert
}

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private float health = 100f;
    [SerializeField]
    private float starveValue = 1f;
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private Image healthBar;
    [SerializeField]
    private TextMeshProUGUI difficulty;
    [SerializeField]
    private TextMeshProUGUI timer;
    public GameState gameState;
    public GameDifficulty gameDifficulty; 

    void Awake()
    {
        maxHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        timer.text = "Time: " + _GT.gameTime;
        healthBar.fillAmount = health / 100;
        UpdateDiffuculty(19);


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
                gameDifficulty = GameDifficulty.Starting; break;
            case 1:
                gameDifficulty = GameDifficulty.Easy; break;
            case 2:
                gameDifficulty = GameDifficulty.Medium; break;
            case 3:
                gameDifficulty = GameDifficulty.Hard; break;
            case 4:
                gameDifficulty = GameDifficulty.Expert; break;
        }

        switch (gameDifficulty)
        {
            case GameDifficulty.Starting:
                starveValue = 0.5f;
                break;
            case GameDifficulty.Easy:
                starveValue = 1f;
                break;
            case GameDifficulty.Medium:
                starveValue = 1.5f;
                break;
            case GameDifficulty.Hard:
                starveValue = 2f;
                break;
            case GameDifficulty.Expert:
                starveValue = 3f;
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

    private void Die()
    {
        gameState = GameState.Death;
        _CM.Death();
    }
}
