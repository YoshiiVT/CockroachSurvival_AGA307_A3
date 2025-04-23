using UnityEngine;
using UnityEngine.AI;

public class Human : GameBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float distance;
    [SerializeField] private float perceptionDistance = 10f;

    [Header("IdleState")]
    [SerializeField] private float wanderRadius = 10f;
    [SerializeField] private float wanderTimer = 5f;
    private float wanderTimerCounter;

    [Header("SearchState")]
    [SerializeField] private float searchDuration = 3f;
    private float searchTimer;
    private bool isSearching = false;
    private Vector3 lastKnownPlayerPosition;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        wanderTimerCounter = wanderTimer;
    }

    void Update()
    {
        _EM

        distance = Vector3.Distance(transform.position, _CM.transform.position);

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
}
