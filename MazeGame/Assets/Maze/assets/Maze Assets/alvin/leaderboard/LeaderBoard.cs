using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script is to support Help script in outPost
/// Add, compare and update the list
/// by using PlayerPref
/// </summary>
public class LeaderBoard : MonoBehaviour
{
    public Text[] highScores;


    string[] highScoreNames;
    int[] highScoreValues;
    // Start is called before the first frame update

    // Set up highScoreValues and highScoreNames list
    void Start()
    {
        //PlayerPrefs.DeleteAll();
        highScoreValues = new int[highScores.Length];
        highScoreNames = new string[highScores.Length];
        // Retrieving high score board
        for(int i = 0; i < highScores.Length; i++)
        {
            highScoreValues[i] = PlayerPrefs.GetInt("highScoreValues" + i);
            highScoreNames[i] = PlayerPrefs.GetString("highScoreNames" + i);
            
        }
        DrawScores();
    }

    public void CheckForHighScore(int _value, string _name)
    {
        for(int i = 0; i < highScores.Length; i++)
        {
            if(_value > highScoreValues[i])
            {
                for(int x = highScores.Length - 1; x > i; x--)
                {
                    highScoreValues[x] = highScoreValues[x - 1];
                    highScoreNames[x] = highScoreNames[x - 1];
                }

                highScoreValues[i] = _value;
                highScoreNames[i] = _name;
                DrawScores();
                SaveScores();
                break;
            }
        }
    }

    // Save the score
    void SaveScores()
    {
        for (int i = 0; i < highScores.Length; i++)
            {
                PlayerPrefs.SetInt("highScoreValues" + i, highScoreValues[i]);
                PlayerPrefs.SetString("highScoreNames" + i, highScoreNames[i]);
        }
    }

    // Getting high score from PlayerPrefs
    // Assign the score in PlayerPrefs to highScore array
    void DrawScores()
    {
        for (int i = 0; i < highScores.Length; i++)
        {
            highScores[i].text = highScoreNames[i] + " : " +  highScoreValues[i].ToString() + " seconds";
        }
    }
}
