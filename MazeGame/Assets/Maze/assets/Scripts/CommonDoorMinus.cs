using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Audio;

public class CommonDoorMinus
    : MonoBehaviour
{
    public GameObject keyImage;
    public GameObject Player;
    public string foodfound;
   AudioSource DoorSound;

    

    private Vector3 initialPos;
    private Vector3 movePos;
    private float totalDist;

    private bool doorOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
        movePos = new Vector3(transform.position.x + 8.0f, transform.position.y, transform.position.z);
        //movePos = moveToObject.transform.position;
        totalDist = Vector3.Distance(initialPos, movePos);
        DoorSound = GameObject.Find("Doors&Food").GetComponent<AudioSource>();
        DoorSound.Stop();
        //keyImage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
 
        if (Player.transform.Find(transform.name + "food") == null)
        {
        }
        else
        {
            commentDoorCheck();

        }


        if (doorOpen)
        {
            float distCovered = (Vector3.Distance(transform.position, initialPos) + 4.0f * Time.deltaTime) / totalDist;
            GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(initialPos, movePos, distCovered));

        }
        else
        {
            //keyImage.SetActive(true);
        }





    }


    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "Player" && (foodfound == transform.name + "food") && Input.GetKeyDown(KeyCode.E))
        {
            doorOpen = true;
            print(DoorSound);
            DoorSound.Play();
            print("music");

            //Invoke("mute", 2.8f);

            //Destroy(this.gameObject);
            //var oldPos = new Vector3(transform.position.x + 5.0f, transform.position.y, transform.position.z)
            //var newPos = new Vector3(transform.position.x + 5.0f, transform.position.y, transform.position.z);
            //transform.position = new Vector3(transform.position.x + 10.0f, transform.position.y, transform.position.z);

            //GetComponent<Rigidbody>().velocity = transform.right * 10.0f;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        //GetComponent<BoxCollider>().isTrigger = false;
        keyImage.SetActive(false);

    }

    private void OnTriggerEnter(Collider other)
    {
        if(!doorOpen && other.gameObject.tag == "Player")
        {
            keyImage.SetActive(true);
        }

    }

    private void commentDoorCheck()
    {
        foodfound = Player.transform.Find(transform.name + "food").gameObject.name;
    }
    void mute()
    {
        DoorSound.Stop();
    }
}
