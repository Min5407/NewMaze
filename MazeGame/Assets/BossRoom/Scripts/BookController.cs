using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public class BookController : MonoBehaviour
{
    public GameObject instruction;
    public GameObject keyTexture;
    private bool playerInTrigger = false;

    private bool bookClosed = true;
    private bool bookOpen = false;
    private bool bookOpening = false;
    private bool bookClosing = false;

    //timer
    Stopwatch stopWatch;
    public GameObject timer;
    public InputField username;
    private int timeTaken;


    // button
    public GameObject button;
    public GameObject menuButton;
    // Start is called before the first frame update
    void Start()
    {
        menuButton.SetActive(false);
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

        //timeTaken = (int)stopWatch.Elapsed.TotalSeconds;
        //timeTaken =(int)stopWatch.Elapsed.TotalSeconds;

        if (playerInTrigger)
        {
            if (Input.GetKeyDown("e"))
            {
                keyTexture.SetActive(false);
                instruction.SetActive(true);
            }
            

        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            instruction.SetActive(false);
        }



    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            print("player enter");
            if (keyTexture)
            {
                keyTexture.SetActive(true);
            }

            playerInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (keyTexture)
            {
                keyTexture.SetActive(false);
            }
            playerInTrigger = false;
        }
    }

    public void SubmitLeaderboard()
    {
        int timeFromLast = PlayerPrefs.GetInt("timeTaken");
        timeTaken = (int)stopWatch.Elapsed.TotalSeconds;
        timeTaken += timeFromLast;
        GetComponent<LeaderBoard>().CheckForHighScore(timeTaken, username.text);
        username.text = null;
        button.SetActive(false);
        menuButton.SetActive(true);

    }

    public void pauseStopWatch()
    {
        stopWatch.Stop();
    }

    public void resumeStopWatch()
    {
        stopWatch.Start();
    }

}
