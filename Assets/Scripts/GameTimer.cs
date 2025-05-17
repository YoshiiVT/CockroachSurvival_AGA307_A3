using System.Collections;
using UnityEngine;

public class GameTimer : Singleton<GameTimer>
{
    public int gameTime = 0;
    

    void Start()
    {
        StartCoroutine(Timer());
        _GM.UpdateDiffuculty(0);
    }

    private void Update()
    {
        if (_GM.gameState != GameState.Playing) StopAllCoroutines();
    }

    private IEnumerator Timer()
    {
        gameTime++;
        yield return new WaitForSeconds(1);
        StartCoroutine(Timer());
    }
}
