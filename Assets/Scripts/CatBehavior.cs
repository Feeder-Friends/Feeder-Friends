using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class CatBehavior : MonoBehaviour
{
    public int detectionRange = 5;
    public int patrolRange = 10;
    Animator animator;
    public enum CatState
    {
        Patrol, Idle
    }
    public Transform player;
    NavMeshAgent agent;
    private CatState currentState = CatState.Patrol;
    private Vector3 destination;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        if(!player)
        {   
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        destination = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case(CatState.Patrol):
                Patrol();
                break;
            case(CatState.Idle):
                Idle();
                break;
        }
        
    }

    void Patrol()
    {
        if(Vector3.Distance(transform.position, player.position) <= detectionRange && HasLineOfSight())
        {
            currentState = CatState.Idle;
        }
        animator.SetFloat("Vert", 1);
        agent.isStopped = false;
        if(Vector3.Distance(transform.position, destination) <= 0.2f)
        {
            Debug.Log("Distance is " + Vector3.Distance(transform.position, destination) + ", recalculating destination");
            destination = FindDestination();
            Debug.Log("New destination is " + destination);
            agent.SetDestination(destination);
        }
    }

    void Idle()
    {
        if(Vector3.Distance(transform.position, player.position) > detectionRange && !HasLineOfSight())
        {
            currentState = CatState.Patrol;
        }
        animator.SetFloat("Vert", 0);
        agent.isStopped = true;
    }

    bool HasLineOfSight()
    {
        Vector3 direction = (player.position - transform.position).normalized;

        if (Physics.Raycast(transform.position, direction, out RaycastHit hit, detectionRange))
        {
            if(hit.collider.CompareTag("Player"))
            {
                Debug.Log("Player is in sight: " + hit.collider.name);
                return true;
            }
        }
        return false;
    }

    Vector3 FindDestination()
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = transform.position + Random.insideUnitSphere * patrolRange;
            if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
            {
                Debug.DrawRay(hit.position, Vector3.up, Color.red, 1.0f);
                return hit.position;
            }
        }
        return transform.position;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, detectionRange);
    }
}
