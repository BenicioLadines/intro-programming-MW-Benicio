using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SnakeHeadBehavior : MonoBehaviour
{
    //reference to game manager
    GameManager gameManager;
    //reference to rigidbody
    Rigidbody2D rb;
    //direction the head will move next tickUpdate
    Vector2 facing;
    //list of all the segments of the snake
    List<Transform> segments = new List<Transform>();
    //prefab reference for making new snake segments, stored with a reference to it's transform
    //so we don't have to convert it when adding it to the list
    //(transform has all the relevant info we need for each of our segments)
    public Transform segmentPrefab;
    //true if player is player 1 false if player is player 2
    [SerializeField] bool player1;
    //local left bound for this player
    int leftBound;
    //local right bound for this player
    int rightBound;
    //whether or not the head collided with a fruit this past tick
    bool ateThisTick;
    //setting keycodes with variables makes multiplayer really easy cuz its just saved in the scene
    [SerializeField] KeyCode upKey;
    [SerializeField] KeyCode downKey;
    [SerializeField] KeyCode leftKey;
    [SerializeField] KeyCode rightKey;
    //starting length of each player's snake
    [SerializeField] int startLength;



    private void Awake()
    {
        //grabs rigidbody when gameObject is awake (Wakes at Start(), but also when the object is enabled I think?
        //basically it can be set to wake at different times)
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        //Find game manager and add this snake's TickUpdate as a listener to the manager's tickEvent
        gameManager = FindObjectOfType<GameManager>();
        //adds this player's TickUpdate() as a listener for game manager's tickEvent,
        //will be called every time the event is invoked
        gameManager.tickEvent.AddListener(TickUpdate);
        //set this as head of the snake
        segments.Add(this.transform);
        //facing up so they both make a wall at the beginning
        facing = Vector2.up;

        //sets the player's bounds for the game, neither can go on the other player's side
        //one player has this set to true and the other to false of course
        if(player1)
        {
            //leftBound is at the left bound of the whole game scene
            leftBound = -gameManager.horizontalBound;
            //rightBound is at the center but can't overlap player 2's area
            rightBound = -1;
        }
        else
        {
            //leftBound is at the center but can't overlap player 1's area
            leftBound = 1;
            //rightBound is at the right bound of the whole game scene
            rightBound = gameManager.horizontalBound;
        }

        //grows snake to the set starting length
        while(startLength > 0)
        {
            //adds a segment
            Grow();
            //iterates so the loop doesn't go on forever ( will always eventually reach 0)
            startLength--;
        }
    }

    void Update()
    {
        //facing is changed with each directional input, but the snake only moves on tickUpdate
        //Added logic so the player can't move the opposite direction since that doesn't really work

        //player hits up and isn't facing down
        if (Input.GetKeyDown(upKey) && facing != Vector2.down)
        {
            //facing up
            facing = Vector2.up;
        }

        //player hits down and isn't facing up
        if (Input.GetKeyDown(downKey) && facing != Vector2.up)
        {
            //facing down
            facing = Vector2.down;
        }

        //player hits left and isn't facing right
        if (Input.GetKeyDown(leftKey) && facing != Vector2.right)
        {
            //facing left
            facing = Vector2.left;
        }

        //player hits right and isn't facing left
        if (Input.GetKeyDown(rightKey) && facing != Vector2.left)
        {
            //facing right
            facing = Vector2.right;
        }

    }

    void Grow()
    {
        //creates a segment at the last segment's current position
        Transform segment = Instantiate(segmentPrefab, segments[segments.Count - 1].position, Quaternion.identity);
        //makes new segment child of the head
        segment.parent = transform;
        //adds segment to list of segments
        segments.Add(segment);
    }

    public void Shrink(Transform segment)
    {
        //doesn't shrink if the snake would be shrinking it's head off
        if(segment == this.transform)
        {
            return;
        }
        //removes the segment from the list of segments
        segments.Remove(segment);
        //destroys segment
        Destroy(segment.gameObject);
    }

    public void CutOff(Transform segment)
    {
        //gets index of segment the head collided with
        int indexToCutFrom = segments.IndexOf(segment);
        //if the index is the head cancel the function
        if(indexToCutFrom <= 0)
        {
            return;
        }
        //shrinks the snake down until it's count is equal to the index of the segment cut off
        //this makes it so when you run over your own body
        //you basically cut off anything that was behind the part you ran over
        while( segments.Count >= indexToCutFrom)
        {
            Shrink(segments[segments.Count - 1]);
        }
    }

    //made a TickUpdate so I could move the ball's physics and the snakes' seperately
    //If there wasn't a TickEvent for the whole game I would maybe add something where
    //the longer your snake is the slower you go, but I didn't wanna overcomplicate things right now
    void TickUpdate()
    {
        //moves the segments in line with the snake,
        //starts at the last one and iterates
        //through the list till it reaches the head
        for (int i = segments.Count - 1; i > 0; i--)
        {
            //each segment gets moved to the position of the one before it
            segments[i].position = segments[i - 1].position;
        }

        //moves the head by the direction it's facing
        rb.position += facing;

        //there's a problem right now where the ball doesn't always react to the head bumping into it because
        //updating the rigidbody's position is more like a teleport than an actual physics movement.
        //I could maybe fix it by making the snakes kinematic and using rb.MovePosition(), since that does
        //calculate the physics of the movement, but then the snakes would be able to push the ball to go way
        //too fast if they're moving fast enough. Also, because the first new segment spawns on top of the head,
        //it was making it so Grow() and CutOff() would run the same frame and crash unity so I really didn't feel
        //like trying to fix it right now when the ball works right for the most part (it works better if it's going at a fast
        //enough speed and can't accidentally collide with the snake more than once)

        //was using this for debug because grabbing fruits was getting annoying lol
        /*if (Input.GetButton("Jump"))
        {
            ateThisTick = true;
        }*/

        //basically a switch where once ateThisTick is true it only runs once because it sets the bool back to false after
        if (ateThisTick)
        {
            //grows once
            Grow();
            //closes if statement until ateThisTick is true again
            ateThisTick = false;
        }

        //if the head moves past it's bounds it teleports to the other side
        if(rb.position.x < leftBound)
        {
            //if player moves too far left teleport them to the right
            rb.position = new Vector2(rightBound, rb.position.y);
        }

        if(rb.position.x > rightBound)
        {
            //if player moves too far right teleport them left
            rb.position = new Vector2(leftBound, rb.position.y);
        }

        //absolute value of y is more than verticalBound (accounts for top and bottom bounds)
        if(Mathf.Abs(rb.position.y) > gameManager.verticalBound) 
        {
            //if player moves too far up or down teleport them to the other side
            rb.position = new Vector2(rb.position.x, -rb.position.y + Mathf.Sign(rb.position.y));

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //triggers when the other object has a fruit script component
        //quicker than using tags in my opinion lol
        if(collision.TryGetComponent<fruitBehavior>(out fruitBehavior fruit))
        {
            //sets true so the switch is open again on TickUpdate()
            ateThisTick = true;
        }
    }


}
