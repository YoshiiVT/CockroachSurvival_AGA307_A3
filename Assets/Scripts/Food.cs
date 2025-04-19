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
    [SerializeField]
    private Collider Collider;

    void Start()
    {
        switch (foodType)
        {
            case(FoodType.Large):
            {
                foodValue = 30;
                break;
            }
            case(FoodType.Medium):
            {
                foodValue = 20;
                break;
            }
            case(FoodType.Small):
            {
                foodValue = 10;
                break;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E))
        {
            _GM.Eating(foodValue);
            Destroy(gameObject);
        }
    }

}
