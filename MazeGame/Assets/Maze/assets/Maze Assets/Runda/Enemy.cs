using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public enum FSMState
    {
        None,
        Patrol,
        Chase,
        Attack,
        Dead,
    }
    public FSMState curState;

    Animator anim;
    protected Transform playerTransform; //Player Transform
    public GameObject[] waypointList;

    private NavMeshAgent nav;

    //current waypoint in list
    int currentWaypoint = -1;
    private bool setDest = false;

    protected float elapsedTime;
    public float pathCheckTime = 1.0f;
    private float elapsedPathCheckTime;

    //Whether the NPC is destroyed or not
    protected bool bDead;

    //Ranges for chase and attack
    public float chaseRange = 30.0f;
    public float attackRange = 7.0f;
    public float attackRangeMin = 4.0f;

    public GameObject enemy;
    public float enemyRotSpeed = 15.0f;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        //reference the navmeshagent so we can access it
        nav = GetComponent<NavMeshAgent>();

        curState = FSMState.Patrol;
        bDead = false;

        elapsedTime = 0.0f;

        //Get the target enemy (Player)
        GameObject objPlayer = GameObject.FindGameObjectWithTag("Player");
        playerTransform = objPlayer.transform;

        //if there are waypoints in the list set our destination to be the current waypoint
        if (waypointList.Length > 0)
            currentWaypoint = 0;


        if (!playerTransform)
            print("Player doesn't exist.. Please add one with Tag named 'Player'");

        elapsedPathCheckTime = pathCheckTime;
    }

    // Update is called once per frame
    void Update()
    {
        switch (curState)
        {
            case FSMState.Patrol: UpdatePatrolState(); break;
            case FSMState.Chase: UpdateChaseState(); break;
            case FSMState.Attack: UpdateAttackState(); break;
            //case FSMState.Dead: UpdateDeadState(); break;
        }

        elapsedTime += Time.deltaTime;
        elapsedPathCheckTime += Time.deltaTime;

        //Go to dead state if no health left

        



    }

    protected void UpdatePatrolState()
    {
        anim.SetInteger("attack", 0);

        // NavMeshAgent move code goes here
        //nav.SetDestination(waypointList[currentWaypoint].transform.position);
        if (currentWaypoint > -1)
        {

            float dist = Vector3.Distance(transform.position, waypointList[currentWaypoint].transform.position);
            if (dist < 0.5f)
            {
                currentWaypoint++;
                if (currentWaypoint > (waypointList.Length - 1))
                    currentWaypoint = 0;


                setDest = false;

            }

            if (!setDest)
            {
                nav.SetDestination(waypointList[currentWaypoint].gameObject.transform.position);
                setDest = true;
            }

            if (enemy)
            {
                if (transform.forward != enemy.transform.forward)
                {
                    Quaternion turretRotation = Quaternion.LookRotation(transform.forward - enemy.transform.forward);
                    enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, turretRotation, Time.deltaTime * enemyRotSpeed);
                }
            }


        }

        //Check the distance with player
        //when the distance is near, transition to chase state
        if (Vector3.Distance(transform.position, playerTransform.position) <= chaseRange)
        {
            curState = FSMState.Chase;
        }


    }

    protected void UpdateChaseState()
    {
        anim.SetInteger("attack", 0);

        //NavMeshAgent move
        if (elapsedPathCheckTime >= pathCheckTime)
        {
            nav.SetDestination(playerTransform.position);
            elapsedPathCheckTime = 0f;
        }
        if (enemy)
        {
            if (transform.forward != enemy.transform.forward)
            {
                Quaternion turretRotation = Quaternion.LookRotation(transform.forward - enemy.transform.forward);
                enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, turretRotation, Time.deltaTime * enemyRotSpeed);
            }
        }

        float dist = Vector3.Distance(transform.position, playerTransform.position);
        if (dist <= attackRange)
        {
            curState = FSMState.Attack;
        }

        else if (dist >= chaseRange)
        {
            curState = FSMState.Patrol;
            setDest = false;
        }
    }

    protected void UpdateAttackState()
    {

        //check distance with player 
        float dist = Vector3.Distance(transform.position, playerTransform.position);
        if (dist >= attackRangeMin && dist <= attackRange)
        {
            anim.SetInteger("attack", 1);


            //move toward target
            if (elapsedPathCheckTime >= pathCheckTime)
            {
                nav.isStopped = false;
                nav.SetDestination(playerTransform.position);
                elapsedPathCheckTime = 0f;
            }
        }
        else
        {
            anim.SetInteger("attack", 0);

            nav.isStopped = true;
        }

        //return to chase state if player moves out of attack range
        if (dist > attackRange)
        {
            nav.isStopped = false;
            curState = FSMState.Chase;
        }

        // Always Turn the boss towards the player
        if (enemy)
        {
            Quaternion turretRotation = Quaternion.LookRotation(playerTransform.position - transform.position);
            enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, turretRotation, Time.deltaTime * enemyRotSpeed);
        }


    }


    void OnTriggerEnter(Collider other)
    {
 
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRangeMin);
    }



}
