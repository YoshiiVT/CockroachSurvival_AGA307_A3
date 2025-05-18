using System.Collections;
using UnityEngine;

public class WinZone : GameBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) { StartCoroutine(WinProcess()); }
    }

    /// <summary>
    /// WinProcess() Is a half second delay to allow the player to walk into the room to win, instead of just winning at the door.
    /// </summary>
    /// <returns></returns>
    public IEnumerator WinProcess()
    {
        yield return new WaitForSeconds(0.5f);
        _GM.Win();
    }
}
