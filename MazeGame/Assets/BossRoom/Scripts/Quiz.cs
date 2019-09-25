using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Quiz : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerInTrigger & quizComplete == false)
        {
            if (Input.GetKeyDown("e"))
            {
                keyTexture.SetActive(false);
                instruction.SetActive(true);
                bookActivated = true;
            }


        }

        if (bookActivated & quizComplete == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                instruction.SetActive(false);
            }else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
            {
                userAnswer = 2;
                quizComplete = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
            {
                userAnswer = 1;
                quizComplete = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
            {
                userAnswer = 3;
                quizComplete = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
            {
                userAnswer = 4;
                quizComplete = true;
            }
        }

        if (quizComplete)
        {
            if (userAnswer == answer)
            {
                instruction.SetActive(false);
                successPanel.SetActive(true);
                successPanelOpen = true;
                userAnswer = 0;
                
            }
            else if(userAnswer != 0 & userAnswer != answer)
            {
                print("user answer:" + userAnswer);

                instruction.SetActive(false);
                failPanel.SetActive(true);
                failPanelOpen = true;
                userAnswer = 0;
                
            }
        }

        if (successPanelOpen)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                successPanel.SetActive(false);
            }
           
        }

        if (failPanelOpen)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                failPanel.SetActive(false);
            }

        }



    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            print("player enter");
            if (keyTexture & quizComplete == false)
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
}
