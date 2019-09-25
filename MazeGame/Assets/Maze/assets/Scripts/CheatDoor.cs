using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatDoor : MonoBehaviour
{
    public GameObject Player;

    private Vector3 initialPos;
    private Vector3 movePos;
    private float totalDist;

    private bool playerInTrigger = false;
    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
        movePos = new Vector3(transform.position.x, transform.position.y + 20.0f, transform.position.z);
        totalDist = Vector3.Distance(initialPos, movePos);
    }

    // Update is called once per frame
    void Update()
    {
        
            if (playerInTrigger) {
                float distCovered = (Vector3.Distance(transform.position, initialPos) + 3.0f * Time.deltaTime) / totalDist;
                GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(initialPos, movePos, distCovered));
            }
            
        
    }

    

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            playerInTrigger = true;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInTrigger = false;
        }
    }
}
