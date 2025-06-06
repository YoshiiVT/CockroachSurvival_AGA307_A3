using UnityEngine;

public class GameBehaviour : MonoBehaviour
{
    protected static CharacterManager _CM { get { return CharacterManager.instance; } }

    protected static GameManager _GM { get { return GameManager.instance; } }

    protected static GameTimer _GT { get { return GameTimer.instance; } }

    protected static EnemyManager _EM { get { return EnemyManager.instance; } }

    protected static TrashManager _TM { get { return TrashManager.instance; }}

    protected static ButtonManager _BM { get { return ButtonManager.instance; }}

    protected static SceneHandler _SM { get { return SceneHandler.instance; }}

    protected static UIManager _UI { get { return UIManager.instance; }}
}

 