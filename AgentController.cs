using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject waypoint;
    public float soldierDamage = 10f;
    public int iD;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        iD = GetHashCode();
    }

    private void Update()
    {
        agent.SetDestination(waypoint.transform.position);
    }
}
