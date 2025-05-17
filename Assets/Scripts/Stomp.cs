using UnityEngine;

public class Stomp : GameBehaviour
{
    [SerializeField] private GameObject human;

    private void Start()
    {
        if (human == null || human.GetComponent<Human>() == null) { Debug.LogError("Human Not Found"); }
    }
    /// <summary>
    /// This code is applied to the GameObject the collider is on. The Collider will turn on / off depending on the status of the attack. But while on, it checks if there is a player inside,
    /// if so it lets the attacked human know its it the player.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Triggered");
        if (other.CompareTag("Player"))
        {
            human.GetComponent<Human>().HitPlayer();
        }
    }
}
