using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : Singleton<SceneHandler>
{
    public void LoadLevel(int _levelNo, int _difficultyNo)
    {
        switch (_difficultyNo)
        {
            case 0:
                _GM.UpdateDiffuculty(0); break;
            case 1:
                _GM.UpdateDiffuculty(1); break;
            case 2:
                _GM.UpdateDiffuculty(2); break;
        }

        switch (_levelNo)
        {
            case 1:
                SceneManager.LoadScene("Level_1"); _GM.gameState = GameState.Playing; _GM.StartLevel(_GM.gameDifficulty); break;
            case 2:
                SceneManager.LoadScene("Level_2"); _GM.gameState = GameState.Playing; _GM.StartLevel(_GM.gameDifficulty); break;
            case 3:
                SceneManager.LoadScene("Level_3"); _GM.gameState = GameState.Playing; _GM.StartLevel(_GM.gameDifficulty); break;
        }
    }

    public void SceneLoad(string _Scene)
    {
        SceneManager.LoadScene(_Scene);
    }


}
