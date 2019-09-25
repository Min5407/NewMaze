using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{
    Animator anim;
    
    public enum FSMState
    {
        None,
        Patrol,
        Chase,
        Attack,
        Dead,
    }
    public FSMState curState;

    public GameObject[] waypointList;

    private NavMeshAgent nav;

    int currentWaypoint = 0;

    protected float elapsedTime;
    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        curState = FSMState.Patrol;


        elapsedTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        switch (curState)
        {
            case FSMState.Patrol: UpdatePatrolState(); break;
            //case FSMState.Chase: UpdateChaseState(); break;
            //case FSMState.Attack: UpdateAttackState(); break;
            //case FSMState.Dead: UpdateDeadState(); break;
        }

        elapsedTime += Time.deltaTime;
    }

    protected void UpdatePatrolState()
    {


        // NavMeshAgent move code goes here
        nav.SetDestination(waypointList[currentWaypoint].transform.position);
        float dist = Vector3.Distance(transform.position, waypointList[currentWaypoint].transform.position);
        if (dist < 0.5f)
        {
            currentWaypoint++;
            print(currentWaypoint);
            if (currentWaypoint == 7)
            {
                currentWaypoint = 0;
                //nav.SetDestination(waypointList[currentWaypoint].transform.position);
            }

        }

    }

}
