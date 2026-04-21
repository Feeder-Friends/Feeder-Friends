using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class CatBehavior : MonoBehaviour
{
    public AudioSource source;
    public int detectionRange = 5;
    public int patrolRange = 10;
    bool audioToggle = false;
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
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        destination = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case (CatState.Patrol):
                Patrol();
                break;
            case (CatState.Idle):
                Idle();
                break;
        }

    }

    void Patrol()
    {
        //Debug.Log("Distance from player: " + Vector3.Distance(transform.position, player.position) + "vs detectionRange: " + detectionRange);
        if (Vector3.Distance(transform.position, player.position) <= detectionRange && HasLineOfSight())
        {
            audioToggle = true;
            currentState = CatState.Idle;
        }
        else
        {
            if (audioToggle)
            {
                source.Stop();
                audioToggle = false;
            }
            animator.SetFloat("Vert", 1);
            agent.isStopped = false;
            if (Vector3.Distance(transform.position, destination) <= 0.2f)
            {
                //Debug.Log("Distance is " + Vector3.Distance(transform.position, destination) + ", recalculating destination");
                destination = FindDestination();
                //Debug.Log("New destination is " + destination);
                agent.SetDestination(destination);
            }
        }
    }

    void Idle()
    {
        if (Vector3.Distance(transform.position, player.position) > detectionRange && !HasLineOfSight())
        {
            audioToggle = true;
            currentState = CatState.Patrol;
        }
        else
        {
            if (audioToggle)
            {
                source.Play();
                audioToggle = false;
            }
            transform.LookAt(player);
            animator.SetFloat("Vert", 0);
            agent.isStopped = true;
        }
    }

    bool HasLineOfSight()
    {
        Vector3 direction = (player.position - transform.position).normalized;

        if (Physics.Raycast(transform.position, direction, out RaycastHit hit, detectionRange))
        {
            if (hit.collider.CompareTag("PlayerBody"))
            {
                //Debug.Log("Player is in sight: " + hit.collider.name);
                return true;
            }
        }
        //Debug.Log("Player is not in sight");
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
        Vector3 direction = (player.position - transform.position).normalized;
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, direction);
    }
}
