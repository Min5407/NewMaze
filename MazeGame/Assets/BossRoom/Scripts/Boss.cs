using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{


    private Rigidbody _rigidbody;
    public enum FSMState
    {
        None,
        Patrol,
        Chase,
        Attack,
        Dead,
    }
    public FSMState curState;

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
    public int health = 100;

    //Ranges for chase and attack
    public float chaseRange = 30.0f;
    public float attackRange = 5.0f;
    public float attackRangeMin = 5.0f;

    //boss body
    public GameObject boss;
    public float bossRotSpeed = 15.0f;

    public int score = 0;

    public GameObject congratulationPanel;
    public Text scoreText;

    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
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
            case FSMState.Dead: UpdateDeadState(); break;
        }

        elapsedTime += Time.deltaTime;
        elapsedPathCheckTime += Time.deltaTime;

        //Go to dead state if no health left
        if (score == 7)
        {
            curState = FSMState.Dead;
            Invoke("mazeClear", 3f);
            
        }



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
                if (currentWaypoint > (waypointList.Length -1))
                    currentWaypoint = 0;


                setDest = false;

            }

            if (!setDest)
            {
                nav.SetDestination(waypointList[currentWaypoint].gameObject.transform.position);
                setDest = true;
            }

            if (boss)
            {
                if(transform.forward != boss.transform.forward)
                {
                    Quaternion turretRotation = Quaternion.LookRotation(transform.forward - boss.transform.forward);
                    boss.transform.rotation = Quaternion.Slerp(boss.transform.rotation, turretRotation, Time.deltaTime * bossRotSpeed);
                }
            }

        }

        //Check the distance with player
        //when the distance is near, transition to chase state
        if(Vector3.Distance(transform.position, playerTransform.position) <= chaseRange)
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
        if (boss)
        {
            if (transform.forward != boss.transform.forward)
            {
                Quaternion turretRotation = Quaternion.LookRotation(transform.forward - boss.transform.forward);
                boss.transform.rotation = Quaternion.Slerp(boss.transform.rotation, turretRotation, Time.deltaTime * bossRotSpeed);
            }
        }

        float dist = Vector3.Distance(transform.position, playerTransform.position);
        if(dist <= attackRange)
        {
            curState = FSMState.Attack;
        }

        else if(dist >= chaseRange)
        {
            curState = FSMState.Patrol;
            setDest = false;
        }
    }

    protected void UpdateAttackState()
    {
        anim.SetInteger("attack", 1);

        //check distance with player 
        float dist = Vector3.Distance(transform.position, playerTransform.position);
        if(dist >= attackRangeMin && dist<= attackRange)
        {
            //move toward target
            if(elapsedPathCheckTime >= pathCheckTime)
            {
                nav.isStopped = false;
                nav.SetDestination(playerTransform.position);
                elapsedPathCheckTime = 0f;
            }
        }
        else
        {
            nav.isStopped = true;
        }

        //return to chase state if player moves out of attack range
        if(dist > attackRange)
        {
            nav.isStopped = false;
            curState = FSMState.Chase;
        }

        // Always Turn the boss towards the player
        if (boss)
        {
            Quaternion turretRotation = Quaternion.LookRotation(playerTransform.position - transform.position);
            boss.transform.rotation = Quaternion.Slerp(boss.transform.rotation, turretRotation, Time.deltaTime * bossRotSpeed);
        }


    }

    protected void UpdateDeadState()
    {
        if (!bDead)
        {
            nav.isStopped = true;
            nav.enabled = false;
            bDead = true;
        }
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

    public void addScore(int point)
    {
        score += point;
        print(score);
        scoreText.text = "Test: " + score.ToString() + "/7";

    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            //_rigidbody.AddForce(-transform.forward * 300, ForceMode.Acceleration);
            print("collide with player");
            Invoke("Update", 5f);

        }
    }

    void mazeClear()
    {
        Time.timeScale = 0;
        congratulationPanel.SetActive(true);
    }

}
