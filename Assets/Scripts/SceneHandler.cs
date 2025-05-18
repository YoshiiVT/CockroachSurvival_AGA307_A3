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
                SceneManager.LoadScene("Level_1");  StartCoroutine(_GM.LoadingLevel()); break;
            case 2:
                SceneManager.LoadScene("Level_2");  StartCoroutine(_GM.LoadingLevel()); break;
            case 3:
                SceneManager.LoadScene("Level_3");  StartCoroutine(_GM.LoadingLevel()); break;
        }
    }

    public void SceneLoad(string _Scene)
    {
        SceneManager.LoadScene(_Scene);
        _GM.gameState = GameState.Start;
        _UI.Toggle("null");
    }

    public void ReloadScene()
    {
        _GM.ReloadLevel();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
