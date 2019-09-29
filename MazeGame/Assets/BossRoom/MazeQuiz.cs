using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MazeQuiz : MonoBehaviour
{
    public GameObject instruction;
    public GameObject keyTexture;
    private bool playerInTrigger = false;
    public int answer;
    private bool bookClosed = true;
    private bool bookOpen = false;
    private bool bookOpening = false;
    private bool bookClosing = false;
    private bool bookActivated = false;
    private int userAnswer;
    private bool quizComplete = false;
    public GameObject successPanel;
    public GameObject failPanel;
    private bool successPanelOpen = false;
    private bool failPanelOpen = false;

    protected int score;
    public GameObject boss;
    public GameObject door;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                quizComplete = false;
                keyTexture.SetActive(false);
                instruction.SetActive(true);
                bookActivated = true;
            }


        }

        if (bookActivated)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                instruction.SetActive(false);
                bookActivated = false;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
            {
                userAnswer = 2;
                quizComplete = true;
                bookActivated = false;
                quizCompleted();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
            {
                userAnswer = 1;
                quizComplete = true;
                bookActivated = false;
                quizCompleted();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
            {
                userAnswer = 3;
                quizComplete = true;
                bookActivated = false;
                quizCompleted();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
            {
                userAnswer = 4;
                quizComplete = true;
                bookActivated = false;
                quizCompleted();
            }
        }

        if (successPanelOpen)
        {
            Invoke("closesuccessPanel", 2f);

        }

        if (failPanelOpen)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                failPanel.SetActive(false);
                failPanelOpen = false;

                instruction.SetActive(true);
                bookActivated = true;
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                failPanel.SetActive(false);
                failPanelOpen = false;
            }

            //Invoke("closefailPanel", 2.0f);

        }



    }

    private void OnTriggerEnter(Collider other)
    {
        print("PLAYERPLAYER");
        if (other.gameObject.tag == "Player" & quizComplete == false)
            
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
                instruction.SetActive(false);
            }
            playerInTrigger = false;
        }
    }

    void closefailPanel()
    {
        failPanel.SetActive(false);
        failPanelOpen = false;
    }

    void closesuccessPanel()
    {
        successPanel.SetActive(false);
        successPanelOpen = false;
    }

    void quizCompleted()
    {
        if (quizComplete)
        {
            if (userAnswer == answer)
            {
                instruction.SetActive(false);
                successPanel.SetActive(true);
                successPanelOpen = true;
                userAnswer = 0;
                door.SendMessage("Open");
                //boss.SendMessage("addScore", 1);

            }
            else if (userAnswer != 0 & userAnswer != answer)
            {
                print("user answer:" + userAnswer);

                instruction.SetActive(false);
                failPanel.SetActive(true);
                failPanelOpen = true;
                quizComplete = false;
                userAnswer = 0;

            }
        }

        
    }
}
