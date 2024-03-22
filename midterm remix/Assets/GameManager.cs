using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    //the scores are publicly readable but can only be set by the game manager
    public int leftScore { get; private set; }
    [SerializeField] TextMeshProUGUI leftText;
    public int rightScore { get; private set; }
    [SerializeField] TextMeshProUGUI rightText;
    //serialize field lets you save a variable in the scene so you can change it quickly in the editor
    [SerializeField] float tickTime;
    float tickTimer;
    public UnityEvent tickEvent;
    public int verticalBound;
    public int horizontalBound;

    void Update()
    {
        //invokes tickEvent when the timer goes past the tickTime set in editor and sets it back to 0
        if(tickTimer > tickTime)
        {
            tickTimer = 0;
            tickEvent.Invoke();
        }
    }

    private void FixedUpdate()
    {
        //tried to put the tickTimer on fixed time because otherwise
        //at the start of the game the snakes would move at an inconsistent speed
        tickTimer += Time.fixedDeltaTime;
    }

    //Made set functions for when the left or right side score
    public void RightScored()
    {
        rightScore++;
        //UI is only updated when the score changes to take up less resources or whatever haha
        rightText.text = rightScore.ToString();
    }

    public void LeftScored()
    {
        leftScore++;
        leftText.text = leftScore.ToString();
    }

    //draws the bounding box of the game,
    //the other gameObjects just reference the manager's bounds when setting their own in Start()
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector2(horizontalBound*2, verticalBound*2));
    }
}
