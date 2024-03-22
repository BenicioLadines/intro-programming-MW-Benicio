using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float moveSpeed;
    GameManager gameManager;
    [SerializeField] float waitTime;
    float currentWaitTime;
    Vector2 storedVelocity;
    [SerializeField] Transform waitingZone;
    bool waiting;

    void Start()
    {
        //references other component on this gameObject
        rb = GetComponent<Rigidbody2D>();
        //finds first object with this component in the scene (theres only one which makes it easy)
        gameManager = FindObjectOfType<GameManager>();
        //launches ball but sets it right after so it stays in place at the beginning of the round.
        //gives players some time to move around before the ball just gets goin.
        LaunchBall();
        SetBall();
    }

    private void Update()
    {
        //bool switch for the waiting timer, if the ball is waiting the timer updates
        if(waiting)
        {
            currentWaitTime += Time.deltaTime;
        }

        //when the timer passes it's waitTime it sets waiting to false and sets the timer back to 0
        if (currentWaitTime > waitTime)
        {
            transform.position = Vector2.zero;
            rb.velocity = storedVelocity;
            waiting = false;
            currentWaitTime = 0;
        }
    }

    //launches ball either up or down, left or right
    //just the same as on our pong project lol
    void LaunchBall()
    {
        Vector3 direction = new Vector3(0, 0, 0);
        int xDirection = Random.Range(0, 2);
        int yDirection = Random.Range(0, 2);

        if (xDirection == 0)
        {
            direction.x = -1;
        }
        else
        {
            direction.x = 1;
        }

        if (yDirection == 0)
        {
            direction.y = -1;
        }
        else
        {
            direction.y = 1;
        }

        //uses addforce cuz it's a dynamic rigidbody
        rb.AddForce(direction.normalized * moveSpeed,ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        //if the ball collides with a goal it runs the goal functions on the game manager and sets the ball again
        if(collision.gameObject.tag == "player1Goal")
        {
            gameManager.RightScored();
            SetBall();

        }

        if(collision.gameObject.tag == "player2Goal")
        {
            gameManager.LeftScored();
            SetBall();
        }

        
        
    }
    //puts ball in it's waiting position and holds it in place for waitTime.
    //it's velocity is stored so it can be applied again once the waitTime is over.
    //since it's still colliding with the goal walls it bounces and automatically is served
    //to the side that just scored, convenient!!
    void SetBall()
    {
        storedVelocity = rb.velocity;
        transform.position = waitingZone.position;
        rb.velocity = Vector2.zero;
        waiting = true;
    }

}
