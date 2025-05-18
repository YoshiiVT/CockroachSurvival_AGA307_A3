using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Human : GameBehaviour
{
    [Header("References")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private CapsuleCollider stompCollider;
    [SerializeField] private AudioSource stompSound;

    [Header("Variables")]
    [SerializeField] private float distance;
    [SerializeField] private float perceptionDistance;
    [SerializeField] private int myDamage;
    [SerializeField] private float mySpeed;
    [SerializeField] private float attackRange = 10f;
    [SerializeField] private bool isAttacking = false;

    [Header("IdleState")]
    [SerializeField] private float wanderRadius;
    [SerializeField] private float wanderTimer = 5f;
    private float wanderTimerCounter;

    [Header("SearchState")]
    [SerializeField] private float searchDuration;
    private float searchTimer;
    private bool isSearching = false;
    private Vector3 lastKnownPlayerPosition;

    /// <summary>
    /// This sets the conditions for the Human on Summon
    /// </summary>
    /// <param name="_enemyDifficulty"></param>
    public void Initialize(EnemyDifficulty _enemyDifficulty)
    {
        agent = GetComponent<NavMeshAgent>();
        wanderTimerCounter = wanderTimer;
        stompCollider.enabled = false;

        switch (_enemyDifficulty)
        {
            case EnemyDifficulty.Easy:
                perceptionDistance = 10f;
                wanderRadius = 10f;
                searchDuration = 3f;
                myDamage = 10;
                mySpeed = 3.5f;
                break;
            case EnemyDifficulty.Medium:
                perceptionDistance = 10f;
                wanderRadius = 25f;
                searchDuration = 6f;
                mySpeed = 7f;
                myDamage = 50;
                break;
            case EnemyDifficulty.Hard:
                perceptionDistance = 10f;
                wanderRadius = 50f;
                searchDuration = 9f;
                myDamage = 100;
                mySpeed = 10f;
                break;
        }

        agent.speed = mySpeed;
    }

    void Update()
    {
        if (_GM.gameState != GameState.Playing) { return; }
        distance = Vector3.Distance(transform.position, _CM.transform.position);

        if (distance < attackRange && !isAttacking)
        {
            Debug.Log("In Attack Range");
            StartCoroutine(StompAttack());
        }

        // Player is within perception distance we follow them
        if (distance <= perceptionDistance)
        {
            lastKnownPlayerPosition = _CM.transform.position;
            agent.SetDestination(lastKnownPlayerPosition);
            isSearching = true;
            searchTimer = searchDuration;
            return;
        }

        // Player is out of range but we're in search mode
        if (isSearching)
        {
            searchTimer -= Time.deltaTime;

            if (searchTimer > 0f)
            {
                agent.SetDestination(lastKnownPlayerPosition); // keep heading to last seen position
                return;
            }
            else
            {
                isSearching = false; // end search
            }
        }

        // Idle wandering
        wanderTimerCounter += Time.deltaTime;

        if (wanderTimerCounter >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            wanderTimerCounter = 0f;
        }
    }

    /// <summary>
    /// I... Dont actually know what this code does... Its not mine lol
    /// All I know is that it is used for locating the player.
    /// </summary>
    /// 
    /// <param name="origin"></param>
    /// <param name="dist"></param>
    /// <param name="layermask"></param>
    /// <returns></returns>
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection.y = 0f; // keep it flat on ground
        randDirection += origin;

        NavMeshHit navHit;
        if (NavMesh.SamplePosition(randDirection, out navHit, dist, layermask))
        {
            return navHit.position;
        }

        return origin;
    }
    /// <summary>
    /// Imagine the foot raising before the attack. Thats the start of the corutine. After the return thats the foot coming down.
    /// </summary>
    /// <returns></returns>
    public IEnumerator StompAttack()
    {
        if (isAttacking) {yield return null; }
        isAttacking = true;
        Debug.Log("Attack Starting");
        agent.speed = 0;
        yield return new WaitForSeconds(0.1f);
        stompSound.Play();
        stompCollider.enabled = true;
        StartCoroutine(StompCooldown());
    }
    /// <summary>
    /// After 0.1 Seconds the foot goes back up and is ready to attack / move again.
    /// </summary>
    /// <returns></returns>
    public IEnumerator StompCooldown()
    {
        yield return new WaitForSeconds(0.1f);
        stompCollider.enabled = false;
        agent.speed = mySpeed;
        Debug.Log("Attack Ending");
        isAttacking = false;
    }

    /// <summary>
    /// This function is activated when the Stomp Gameobject has detected a player in its trigger and sends through the damage the player takes.
    /// </summary>
    public void HitPlayer()
    { 
        if (isAttacking) { _GM.BeenHit(myDamage); }
        else { Debug.LogError("Player Been Randomly Hit"); }
    }


}


