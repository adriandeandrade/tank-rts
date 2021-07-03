using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitController : MonoBehaviour
{
    private NavMeshAgent agent;

    // Properties
    public bool IsMoving => !agent.isStopped;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.Warp(transform.position);
    }

    public void ChangeDestination(Vector3 destination)
    {
        if (Input.GetKey(KeyCode.Space))
        {
            agent.updateRotation = false;
        }
        else
        {
            agent.updateRotation = true;
        }

        agent.destination = destination;
    }
}
