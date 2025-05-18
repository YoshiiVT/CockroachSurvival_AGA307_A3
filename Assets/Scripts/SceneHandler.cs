using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : Singleton<SceneHandler>
{
    [SerializeField] Animator transitionAnim;
    [SerializeField] GameObject transitionCanvas;

    public void Start()
    {
        StartCoroutine(DisableTransition());
    }

    private IEnumerator DisableTransition()
    {
        yield return new WaitForSeconds(1);
        transitionCanvas.SetActive(false);
    }

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
                StartCoroutine(SceneTransition("Level_1")); StartCoroutine(_GM.LoadingLevel()); break;
            case 2:
                StartCoroutine(SceneTransition("Level_2")); StartCoroutine(_GM.LoadingLevel()); break;
            case 3:
                StartCoroutine(SceneTransition("Level_3"));  StartCoroutine(_GM.LoadingLevel()); break;
        }
    }

    
    public void SceneLoad(string _Scene)
    {
        StartCoroutine(SceneTransition(_Scene));
        _GM.OutofGame.UnPause();
        _GM.gameState = GameState.Start;
        _UI.Toggle("null");
    }

    public IEnumerator ReloadScene()
    {
        transitionCanvas.SetActive(true);
        _GM.ReloadLevel();
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        transitionAnim.SetTrigger("Start");
        transitionCanvas.SetActive(false);
    }

    private IEnumerator SceneTransition(string _name)
    {
        transitionCanvas.SetActive(true);
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(_name);
        transitionAnim.SetTrigger("Start");
        transitionCanvas.SetActive(false);
    }

}
