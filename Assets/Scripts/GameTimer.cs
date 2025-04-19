using System.Collections;
using UnityEngine;

public class GameTimer : Singleton<GameTimer>
{
    public int gameTime = 0;
    [SerializeField]
    private int easyStart = 10;
    [SerializeField]
    private int mediumStart = 30;
    [SerializeField]
    private int hardStart = 60;
    [SerializeField]
    private int expertStart = 120;

    void Start()
    {
        StartCoroutine(Timer());
        _GM.UpdateDiffuculty(0);
    }

    private void Update()
    {
        if (_GM.gameState != GameState.Playing) StopAllCoroutines();
        if (gameTime == easyStart)
            _GM.UpdateDiffuculty(1);
        else if (gameTime == mediumStart)
            _GM.UpdateDiffuculty(2);
        else if (gameTime == hardStart)
            _GM.UpdateDiffuculty(3);
        else if (gameTime == expertStart)
            _GM.UpdateDiffuculty(4);
    }

    private IEnumerator Timer()
    {
        gameTime++;
        yield return new WaitForSeconds(1);
        StartCoroutine(Timer());
    }
}
