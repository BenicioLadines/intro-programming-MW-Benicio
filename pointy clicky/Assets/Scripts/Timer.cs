using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngineInternal;

public class Timer : MonoBehaviour
{

    public float timeRemaining;
    public TextMeshProUGUI timerText;
    string timerString;
    public bool timerRunning = false;
    // Start is called before the first frame update
    void Start()
    {
        timerString = timerText.text;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }

        if(timeRemaining <= 0)
        {
            timeRemaining = 0;
            Debug.Log("GAME OVER MAN! GAME OVER");
        }

        timerText.text = timerString + DisplayTime(timeRemaining);

    }

    string DisplayTime(float timeInput)
    {
        float minutes = Mathf.FloorToInt(timeRemaining / 60);
        float seconds = Mathf.FloorToInt(timeRemaining % 60);
        return minutes + ":" + seconds;
    }
}
