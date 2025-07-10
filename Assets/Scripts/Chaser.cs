using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Chaser : MonoBehaviour
{
    NavMeshAgent myAgent;

    [SerializeField]
    Transform targetTransform;

    [SerializeField]
    Transform[] patrolPoints; // Array of patrol points (set in Inspector)
    int patrolIndex = 0; // Current patrol point index

    public string currentState;

    void Awake()
    {
        myAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        StartCoroutine(SwitchState("Idle"));
    }

    IEnumerator SwitchState(string newState)
    {
        if (currentState == newState)
        {
            yield break; // Exit if the state is already the same
        }

        currentState = newState;

        StartCoroutine(currentState);
    }

    IEnumerator Idle()
    {
        float idleTime = 2f; // Time to stay idle before patrolling
        float timer = 0f;

        while (currentState == "Idle")
        {
            // Perform idle behavior here
            if (targetTransform != null)
            {
                // If there is a target, go to the chasing state
                StartCoroutine(SwitchState("ChaseTarget"));
            }
            // Wait for the idle time, then start patrolling
            timer += Time.deltaTime;
            if (timer >= idleTime)
            {
                StartCoroutine(SwitchState("Patrol"));
            }
            yield return null; // Wait for the next frame
        }
    }

    IEnumerator Patrol()
    {
        while (currentState == "Patrol")
        {
            // If player appears, switch to chase immediately
            if (targetTransform != null)
            {
                StartCoroutine(SwitchState("ChaseTarget"));
                yield break;
            }

            // If no patrol points assigned, go idle
            if (patrolPoints.Length == 0)
            {
                StartCoroutine(SwitchState("Idle"));
                yield break;
            }

            // Move to current patrol point
            myAgent.SetDestination(patrolPoints[patrolIndex].position);

            // Check if arrived at patrol point
            if (!myAgent.pathPending && myAgent.remainingDistance <= myAgent.stoppingDistance)
            {
                // Move to next patrol point (loop around)
                patrolIndex = (patrolIndex + 1) % patrolPoints.Length;

                // Switch back to idle after reaching patrol point
                StartCoroutine(SwitchState("Idle"));
            }

            yield return null; // Wait for next frame
        }
    }

    IEnumerator ChaseTarget()
    {
        // while loop in a coroutine = mini Update function
        while (currentState == "ChaseTarget")
        {
            // Perform chasing behavior here
            if (targetTransform == null)
            {
                StartCoroutine(SwitchState("Idle"));
            }
            else
            {
                myAgent.SetDestination(targetTransform.position);
            }
            
            yield return null;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // If the chaser 'sees' the player, set the target to the player
        if (other.gameObject.CompareTag("Player"))
            targetTransform = other.transform;
    }

    void OnTriggerExit(Collider other)
    {
        // If the player leaves the chaser's trigger, set the target to null
        if (other.gameObject.CompareTag("Player"))
            targetTransform = null;
    }
}
