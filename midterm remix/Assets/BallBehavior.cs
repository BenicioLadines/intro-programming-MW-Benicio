using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    //rigidbody updates transform with simulated physics
    Rigidbody2D rb;
    //ball's move speed
    [SerializeField] float moveSpeed;
    //game manager reference
    GameManager gameManager;
    //time the ball waits before being served
    [SerializeField] float waitTime;
    //timer for the waitTime
    float currentWaitTime;
    //velocity is stored here on SetBall()
    Vector2 storedVelocity;
    //transform the ball is teleported to on SetBall()
    [SerializeField] Transform waitingZone;
    //true when the ball is waiting to be served
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
            //adds time between this frame and the last to the timer every frame
            currentWaitTime += Time.deltaTime;
        }

        //when the timer passes it's waitTime it sets waiting to false and sets the timer back to 0
        if (currentWaitTime > waitTime)
        {
            //puts ball directly in center of game space
            transform.position = Vector2.zero;
            //applies the velocity stored when the ball was set
            rb.velocity = storedVelocity;
            //no longer waiting cuz it's back in play
            waiting = false;
            currentWaitTime = 0;
        }
    }

    //launches ball either up or down, left or right
    //just the same as on our pong project lol
    void LaunchBall()
    {
        //initialize new launch direction
        Vector3 direction = new Vector3(0, 0, 0);
        //picks 0 or 1 randomly
        int xDirection = Random.Range(0, 2);
        //picks 0 or 1 randomly
        int yDirection = Random.Range(0, 2);

        if (xDirection == 0)
        {
            //if the random x was 0 ball moves left
            direction.x = -1;
        }
        else
        {
            //if it was 1 the ball moves right
            direction.x = 1;
        }

        if (yDirection == 0)
        {
            //if the random y was 0 the ball moves down
            direction.y = -1;
        }
        else
        {
            //if the random y was 1 the ball moves up
            direction.y = 1;
        }

        //uses addforce cuz it's a dynamic rigidbody
        //direction is normalized so that it's magnitude is 1, makes it so
        //when the force is applied the vector is only as long as moveSpeed
        // otherwise (1,1) makes a vector with a magnitude of sqrt(2) or something
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
        //if the ball collides with a goal it runs the goal functions on the game manager and sets the ball again
        if (collision.gameObject.tag == "player2Goal")
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
        //stores velocity
        storedVelocity = rb.velocity;
        //sets ball in waiting zone
        transform.position = waitingZone.position;
        //sets ball's velocity to 0
        rb.velocity = Vector2.zero;
        //ball is ready and waiting
        waiting = true;
    }

}
