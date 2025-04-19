using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private float health = 100f;
    [SerializeField]
    private float starveValue = 1f;
    [SerializeField]
    private float maxHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        float imaStarveValue = starveValue;
        if (_CM.isSprinting == true) imaStarveValue *= 10;

        if (health > maxHealth)
            health = maxHealth;

        Starving(imaStarveValue);
    }

    private void Starving(float imaStarveValue)
    {
        health -= imaStarveValue * Time.deltaTime;
    }

    public void Eating(int food)
    {
        health += food;
    }
}
