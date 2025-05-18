using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private GameObject playerOverlay;
    [SerializeField]
    private GameObject deathScreen;
    [SerializeField]
    private GameObject winScreen;

    public void Toggle(string exept)
    {
        switch (exept)
        {
            case "playerOverlay":
                playerOverlay.SetActive(true);
                deathScreen.SetActive(false);
                winScreen.SetActive(false); 
                break;
            case "deathScreen":
                playerOverlay.SetActive(false);
                deathScreen.SetActive(true);
                winScreen.SetActive(false);
                break;
            case "winScreen":
                playerOverlay.SetActive(false);
                deathScreen.SetActive(false);
                winScreen.SetActive(true);
                break;
            case "null":
                playerOverlay.SetActive(false);
                deathScreen.SetActive(false);
                winScreen.SetActive(false);
                break;

        }
    }
}
