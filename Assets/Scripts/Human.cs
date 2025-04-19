using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Human : GameBehaviour
{
    [SerializeField]
    NavMeshAgent agent;
    private float movepos;
    private float noticeDistance;
    // Update is called once per frame
    void Update()
    {
        //noticeDistance = Distance<>
        agent.SetDestination(_CM.transform.position);
    }
}
