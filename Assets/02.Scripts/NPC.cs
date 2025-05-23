using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public enum AIState
{
    Idle,
    Wandering,
    Attacking
}

public class NPC : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] int health;
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;

    [Header("AI")]
    NavMeshAgent agent;
    AIState state;
    [SerializeField] float detectDistance;
    [SerializeField] Player target;
    

    [Header("Wandering")]
    [SerializeField] float minWanderDistance;
    [SerializeField] float maxWanderDistance;
    [SerializeField] float minWanderWaitTime;
    [SerializeField] float maxWanderWaitTime;

    [Header("Combat")]
    [SerializeField] int damage;
    [SerializeField] float attackRate;
    float lastAttackTime;
    [SerializeField] float attackDistance;

    float playerDistance;
    [SerializeField] float fieldOfView = 120f;

    Animator anim;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        if (target == null)
        {
            target = FindObjectOfType<Player>();
        }
    }

    void Start()
    {
        SetState(AIState.Wandering);
    }

    void Update()
    {
        playerDistance = Vector3.Distance(transform.position, target.transform.position);

        anim.SetBool("Moving", state != AIState.Idle);

        switch (state)
        {
            case AIState.Idle:
            case AIState.Wandering:
                PassiveUpdate();
                break;

            case AIState.Attacking:
                AttackingUpdate();
                break;
        }
    }

    public void SetState(AIState state)
    {
        this.state = state;

        switch (this.state)
        {
            case AIState.Idle:
                agent.speed = walkSpeed;
                agent.isStopped = true;
                break;

            case AIState.Wandering:
                agent.speed = walkSpeed;
                agent.isStopped = false;
                break;

            case AIState.Attacking:
                agent.speed = runSpeed;
                agent.isStopped = false;
                break;
        }

        anim.speed = agent.speed / walkSpeed;
    }

    void PassiveUpdate()
    {
        if (state == AIState.Wandering && agent.remainingDistance < 0.1f)
        {
            SetState(AIState.Idle);
            Invoke("WanderToNewLocation", Random.Range(minWanderWaitTime, maxWanderWaitTime));
        }

        if (playerDistance < detectDistance)
        {
            SetState(AIState.Attacking);
        }
    }

    void WanderToNewLocation()
    {
        if (state != AIState.Idle) return;

        SetState(AIState.Wandering);
        agent.SetDestination(GetWanderLocation());
    }

    Vector3 GetWanderLocation()
    {
        NavMeshHit hit;

        NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * Random.Range(minWanderDistance, maxWanderDistance)), out hit, maxWanderDistance, NavMesh.AllAreas);

        int i = 0;

        while (Vector3.Distance(transform.position, hit.position) < detectDistance)
        {
            NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * Random.Range(minWanderDistance, maxWanderDistance)), out hit, maxWanderDistance, NavMesh.AllAreas);
            i++;
            if (i == 30) break;
        }

        return hit.position;
    }

    void AttackingUpdate()
    {
        if (playerDistance < attackDistance && IsPlayerInFieldOfView())
        {
            agent.isStopped = true;

            if (Time.time - lastAttackTime > attackRate)
            {
                lastAttackTime = Time.time;
                target.TakeDamaged(damage);
                anim.speed = 1;
                anim.SetTrigger("Attack");
            }
        }

        else
        {
            if (playerDistance < detectDistance)
            {
                agent.isStopped = false;
                NavMeshPath path = new NavMeshPath();

                if (agent.CalculatePath(target.transform.position, path))
                {
                    agent.SetDestination(target.transform.position);
                }
                else
                {
                    agent.SetDestination(transform.position);
                    agent.isStopped = true;
                    SetState(AIState.Wandering);
                }
            }

            else
            {
                agent.SetDestination(transform.position);
                agent.isStopped = true;
                SetState(AIState.Wandering);
            }
        }
    }

    bool IsPlayerInFieldOfView()
    {
        Vector3 directionToPlayer = target.transform.position - transform.position;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);
        return angle < fieldOfView * 0.5f;
    }
}
