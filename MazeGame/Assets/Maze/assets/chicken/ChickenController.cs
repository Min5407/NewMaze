using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class ChickenController : MonoBehaviour
{
    public Camera cam1;
    public Camera cam2;
    float speed = 5;
    float rotationspeed = 100;
    float gravity = 8;
    float runningSpeed = 50;
    float rotation = 0f;
    Vector3 movedirection = Vector3.zero;
    CharacterController controller;
    Animator anim;
    public GameObject keyImage;
    AudioSource walkSound;



    private float timePassed = 0.0f;
    private float timer = 10.0f;
    // Start is called before the first frame update

    private IEnumerator ChangeCamera()
    {
        cam1.gameObject.SetActive(false);
        cam2.gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        cam1.gameObject.SetActive(true);
        cam2.gameObject.SetActive(false);
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        walkSound = GetComponent<AudioSource>();
        walkSound.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        chickenMovement();

        timePassed += Time.deltaTime;
        if (Input.GetKeyUp(KeyCode.C))
        {
            
                StartCoroutine(ChangeCamera());
            
            
            
            //timePassed += Time.deltaTime;
            //cam1.gameObject.SetActive(false);
            //cam2.gameObject.SetActive(true);

            ////if(timePassed > timer)
            ////{
            ////    timePassed = 0.0f;
            //StartCoroutine("ChangeCamera");
            //cam1.gameObject.SetActive(true);
            //cam2.gameObject.SetActive(false);
            //}

            //yield return new WaitForSeconds(5);
        }
        //else
        //{
        //    cam1.gameObject.SetActive(true);
        //    cam2.gameObject.SetActive(false);
        //}
    }

    void chickenMovement()
    {
        if (controller.isGrounded)
        {


            if (Input.GetKey(KeyCode.W))
            {

                anim.SetInteger("check", 1);

                movedirection = new Vector3(0, 0, 1);
                movedirection = movedirection * speed;
                movedirection = transform.TransformDirection(movedirection);




            }

            if ((Input.GetKey(KeyCode.LeftShift)) && (Input.GetKey(KeyCode.W)))
            {

                speed = runningSpeed;
                anim.SetBool("isRunning", true);
                //movedirection = new Vector3(0, 0, 1);
                //movedirection = movedirection * speed;
                //movedirection = transform.TransformDirection(movedirection);
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                speed = 3;

                anim.SetBool("isRunning", false);

            }
            rotation += Input.GetAxis("Horizontal") * rotationspeed * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, rotation, 0);




            if (Input.GetKeyUp(KeyCode.W))
            {
                anim.SetInteger("check", 0);

                movedirection = new Vector3(0, 0, 0);
                walkSound.Stop();

            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                walkSound.Play();
            }

            if (anim.GetInteger("check") == 0)
            {
                anim.SetBool("isRunning", false);

            }


        }
        //else
        //{
        //    anim.SetInteger("check", 0);
        //    movedirection = new Vector3(0, 0, 0);

        //}


        movedirection.y -= gravity * Time.deltaTime;
        controller.Move(movedirection * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Food")
        {
            keyImage.SetActive(false);
        }
        if (other.gameObject.tag == "Door")
        {
            keyImage.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Door")
        {
            keyImage.SetActive(false);
           
        }
    }
}
