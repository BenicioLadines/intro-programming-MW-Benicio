using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    //player 1's score, the scores are publicly readable but can only be set by the game manager
    public int leftScore { get; private set; }
    //UI score text for player 1
    [SerializeField] TextMeshProUGUI leftText;
    //player 2's score
    public int rightScore { get; private set; }
    //UI score text for player 2
    [SerializeField] TextMeshProUGUI rightText;
    //time between each tick, serialize field lets you save a variable in the scene so you can change it quickly in the editor
    [SerializeField] float tickTime;
    //timer for each tick
    float tickTimer;
    //unity event that signals to other objects when a tick happened
    public UnityEvent tickEvent;
    //vertical boundary of game scene
    public int verticalBound;
    //horizontal boundary of game scene
    public int horizontalBound;

    void Update()
    {
        //invokes tickEvent when the timer goes past the tickTime set in editor and sets it back to 0
        if(tickTimer > tickTime)
        {
            //timer back to 0
            tickTimer = 0;
            //updates both players' tickUpdate those are the only listeners really
            tickEvent.Invoke();
        }
    }

    private void FixedUpdate()
    {
        //tried to put the tickTimer on fixed time because otherwise
        //at the start of the game the snakes would move at an inconsistent speed.
        //time.fixedDeltaTime gets time between last fixedUpdate frame and the next
        //this means the snakes will move with the physics before the actual game display
        //which means it'll collide better with a dynamic body like the ball
        tickTimer += Time.fixedDeltaTime;
    }

    //Made set functions for when the left or right side score
    public void RightScored()
    {
        //adds a point to player 2's side
        rightScore++;
        //UI is only updated when the score changes to take up less resources or whatever haha
        rightText.text = rightScore.ToString();
    }

    public void LeftScored()
    {
        //adds a point to player 1's side
        leftScore++;
        //UI is only updated when the score changes
        leftText.text = leftScore.ToString();
    }

    private void OnDrawGizmos()
    {
        //sets the next gizmo's color to yellow
        Gizmos.color = Color.yellow;
        //draws the bounding box of the game
        Gizmos.DrawWireCube(transform.position, new Vector2(horizontalBound*2, verticalBound*2));
    }
}
