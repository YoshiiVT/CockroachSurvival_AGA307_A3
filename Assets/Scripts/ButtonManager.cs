using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : Singleton<ButtonManager>
{
    public void ButtonActivation(string _buttonCommand)
    {
        switch (_buttonCommand)
        {
            case "LoadLevel1E":
                _SM.LoadLevel(1, 0);
                break;
            case "LoadLevel1M":
                _SM.LoadLevel(1, 1);
                break;
            case "LoadLevel1H":
                _SM.LoadLevel(1, 2);
                break;
            case "LoadLevel2E":
                _SM.LoadLevel(2, 0);
                break;
            case "LoadLevel2M":
                _SM.LoadLevel(2, 1);
                break;
            case "LoadLevel2H":
                _SM.LoadLevel(2, 2);
                break;
            case "LoadLevel3E":
                _SM.LoadLevel(3, 0);
                break;
            case "LoadLevel3M":
                _SM.LoadLevel(3, 1);
                break;
            case "LoadLevel3H":
                _SM.LoadLevel(3, 2);
                break;
            case "LoadLevelSelect":
                _SM.SceneLoad("Level_Select"); break;

            case null:
                Debug.LogWarning("Button Command Not Recognised");
                break;
        }
    }   
}
