using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{

    public int leftScore { get; private set; }
    public int rightScore { get; private set; }
    [SerializeField] float tickTime;
    float tickTimer;
    public UnityEvent tickEvent;
    public int verticalBound;
    public int horizontalBound;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        tickTimer += Time.fixedDeltaTime;
        if(tickTimer > tickTime)
        {

            tickTimer = 0;
            tickEvent.Invoke();
        }
    }

    void RightScored()
    {
        rightScore++;
    }

    void LeftScored()
    {
        leftScore++;
    }
}
