using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DoorController : MonoBehaviour
{

    public GameObject door;
    public GameObject moveToObj;
    public GameObject keyTexture;

    private Vector3 initialPosition;
    private Vector3 moveToPosition;
    private float totalDist;

    public float moveSpeed = 4.0f;

    private bool playerInTrigger = false;
    public int tanksInTrigger = 0;

    private bool doorClosed = true;
    private bool doorOpen = false;
    private bool doorOpening = false;
    private bool doorClosing = false;

    protected Transform playerTransform;
    protected Transform bossTransform;

    public float dist;

    void Start()
    {

        initialPosition = door.transform.position;
        moveToPosition = moveToObj.transform.position;

        totalDist = Vector3.Distance(initialPosition, moveToPosition);

        GameObject objPlayer = GameObject.FindGameObjectWithTag("Player");
        playerTransform = objPlayer.transform;
        GameObject objBoss = GameObject.FindGameObjectWithTag("Enemy");
        bossTransform = objBoss.transform;
    }


    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.tag == "Player")
        {
            print("player enter");
            if (doorClosed)
            {
                if (keyTexture)
                {
                    keyTexture.SetActive(true);
                }
            }
            playerInTrigger = true;
            tanksInTrigger++;
        }
        else if (col.gameObject.tag == "Enemy")
        {
            print(dist);
            if (doorClosing)
            {
                doorOpening = true;
            }
            if(dist > 30.0f)
            {
                print("Boss enter");
                float distCovered = (Vector3.Distance(door.transform.position, initialPosition) + moveSpeed * Time.deltaTime) / totalDist;
                door.GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(initialPosition, moveToPosition, distCovered));
            }
            tanksInTrigger++;
        }
    }


    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (keyTexture)
            {
                keyTexture.SetActive(false);
            }
            playerInTrigger = false;
            tanksInTrigger--;
        }

        else if (col.gameObject.tag == "Enemy")
        {
            tanksInTrigger--;
        }
    }


    void Update()
    {
        dist = Vector3.Distance(bossTransform.position, playerTransform.position);
        if (playerInTrigger)
        {
            if ((doorClosed) & (Input.GetKeyDown("e")))
            {
                keyTexture.SetActive(false);
                doorClosed = false;
                doorOpening = true;
            }
            else if (doorClosing)
            {
                doorOpening = true;
            }
        }

        if ((doorOpening) & (!doorOpen))
        {
            float distCovered = (Vector3.Distance(door.transform.position, initialPosition) + moveSpeed * Time.deltaTime) / totalDist;
            door.GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(initialPosition, moveToPosition, distCovered));

            if (distCovered >= 1.0f)
            {
                doorOpen = true;
                doorOpening = false;
            }
        }

        if ((tanksInTrigger < 1) & (!doorClosed))
        {
            doorOpen = false;
            doorOpening = false;
            doorClosing = true;

            if ((doorClosing) & (!doorClosed))
            {
                float distCovered = (Vector3.Distance(door.transform.position, moveToPosition) + moveSpeed * Time.deltaTime) / totalDist;
                door.GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(moveToPosition, initialPosition, distCovered));

                if (distCovered >= 1.0f)
                {
                    doorClosed = true;
                    doorClosing = false;
                }
            }
        }
        

    }
}

