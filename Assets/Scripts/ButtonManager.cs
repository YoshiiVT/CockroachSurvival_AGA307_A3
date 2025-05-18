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
                _SM.SceneLoad("Level_2"); break;
            case "LoadLevel2M":
                _SM.SceneLoad("Level_2"); break;
            case "LoadLevel2H":
                _SM.SceneLoad("Level_2"); break;
            case "LoadLevel3E":
                _SM.SceneLoad("Level_3"); break;
            case "LoadLevel3M":
                _SM.SceneLoad("Level_3"); break;
            case "LoadLevel3H":
                _SM.SceneLoad("Level_3"); break;
            case "LoadLevelSelect":
                _SM.SceneLoad("Level_Select"); break;
            case "QuitGame":
                Application.Quit(); break;
            case "LoadLevelCredits":
                _SM.SceneLoad("Credits"); break;
            case "LoadMainMenu":
                _SM.SceneLoad("Title_Menu"); break; 

            case null:
                    Debug.LogWarning("Button Command Not Recognised");
                    break;
                }
    }   
}
