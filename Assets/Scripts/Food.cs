using Unity.VisualScripting;
using UnityEngine;

public enum FoodType
{
    Large,
    Medium,
    Small
}
public class Food : GameBehaviour
{
    [SerializeField]
    FoodType foodType;
    [SerializeField]
    private int foodValue;
    private int pointValue;
    [SerializeField]
    private Collider Collider;

    private bool playerInRange = false;

    void Start()
    {
        switch (foodType)
        {
            case FoodType.Large:
                foodValue = 30;
                pointValue = 300;
                break;
            case FoodType.Medium:
                foodValue = 20;
                pointValue = 200;
                break;
            case FoodType.Small:
                foodValue = 10;
                pointValue = 100;
                break;
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetButtonDown("Interact"))
        {
            _GM.Eating(foodValue);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}