using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.Threading;
using UnityEngine.UI;


public class Help : MonoBehaviour
{
    public GameObject keyImage;
    public GameObject helpCanvas;
    public GameObject chickenCanvas;
    bool help;
    Stopwatch stopWatch;
    public GameObject timer;

    // Start is called before the first frame update
    void Start()
    {
        stopWatch = new Stopwatch();

        stopWatch.Start();
        

    }

    // Update is called once per frame
    void Update()
    {
        string elapsedTime = string.Format("{0:00}:{1:00}:{2:00}",
              stopWatch.Elapsed.Hours, stopWatch.Elapsed.Minutes, stopWatch.Elapsed.Seconds);

        Text time = timer.GetComponent<Text>();
        time.text = "Timer: " + elapsedTime;
   


        if (help)
        {
            if (Input.GetKeyUp(KeyCode.E))
            {

                helpCanvas.SetActive(true);
                keyImage.SetActive(false);
                chickenCanvas.SetActive(false);

            }
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            helpCanvas.SetActive(false);
            chickenCanvas.SetActive(true);

        }


    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            keyImage.SetActive(true);
            help = true;
            //Destroy(this.gameObject);
            //other.gameObject.SendMessage("AmmoPickUp");




            //transform.localPosition = new Vector3(0, (float)0.8, 0);



        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            keyImage.SetActive(false);
            help = false;
            chickenCanvas.SetActive(true);
            helpCanvas.SetActive(false);





        }
    }
}
