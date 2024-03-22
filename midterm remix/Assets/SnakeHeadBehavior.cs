using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SnakeHeadBehavior : MonoBehaviour
{
    GameManager gameManager;
    Rigidbody2D rb;
    Vector2 facing;
    List<Transform> segments = new List<Transform>();
    public Transform segmentPrefab;
    [SerializeField] bool player1;
    int leftBound;
    int rightBound;
    bool ateThisTick;
    //setting keycodes with variables makes multiplayer really easy cuz its just saved in the scene
    [SerializeField] KeyCode upKey;
    [SerializeField] KeyCode downKey;
    [SerializeField] KeyCode leftKey;
    [SerializeField] KeyCode rightKey;
    [SerializeField] int startLength;



    private void Awake()
    {
        //grabs rigidbody when gameObject is awake (Wakes at start I think?)
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        //Find game manager and add this snake's TickUpdate as a listener to the manager's tickEvent
        gameManager = FindObjectOfType<GameManager>();
        gameManager.tickEvent.AddListener(TickUpdate);
        //set this as head of the snake
        segments.Add(this.transform);
        //facing up so they both make a wall at the beginning
        facing = Vector2.up;

        //sets the player's bounds for the game, neither can go on the other player's side
        if(player1)
        {
            leftBound = -gameManager.horizontalBound;
            rightBound = -1;
        }
        else
        {
            leftBound = 1;
            rightBound = gameManager.horizontalBound;
        }

        //grows snake to the set starting length
        while(startLength > 0)
        {
            Grow();
            startLength--;
        }
    }

    void Update()
    {
        //facing is changed with each directional input, but the snake only moves on tickUpdate
        //Added logic so the player can't turn right around since that doesn't really work
        if (Input.GetKeyDown(upKey) && facing != Vector2.down)
        {
            facing = Vector2.up;
        }

        if (Input.GetKeyDown(downKey) && facing != Vector2.up)
        {
            facing = Vector2.down;
        }

        if (Input.GetKeyDown(leftKey) && facing != Vector2.right)
        {
            facing = Vector2.left;
        }

        if (Input.GetKeyDown(rightKey) && facing != Vector2.left)
        {
            facing = Vector2.right;
        }

    }

    void Grow()
    {
        //creates a segment, sets it as a child of the head, and adds it to the list
        Transform segment = Instantiate(segmentPrefab, segments[segments.Count - 1].position, Quaternion.identity);
        segment.parent = transform;
        segments.Add(segment);
    }

    public void Shrink(Transform segment)
    {
        //doesn't shrink if the snake would be shrinking it's head off
        if(segment == this.transform)
        {
            return;
        }
        //removes the segment from the list and then destroys it
        segments.Remove(segment);
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

    //made a TickUpdate so I could move the ball physics and the snakes seperately
    //If there wasn't a TickEvent for the whole game I would maybe add something where
    //the longer your snake is the slower you go, but I didn't wanna overcomplicate things right now
    void TickUpdate()
    {
        //moves the segments in line with the snake, starts at the last one and iterates
        //through the list till it reaches the head
        for (int i = segments.Count - 1; i > 0; i--)
        {
            segments[i].position = segments[i - 1].position;
        }

        //moves the head by the direction it's facing
        rb.position += facing;


        //was using this for debug because grabbing fruits was getting annoying lol
        /*if (Input.GetButton("Jump"))
        {
            ateThisTick = true;
        }*/

        //basically a switch where once ateThisTick is true it only runs once because it sets the bool back to false after
        if (ateThisTick)
        {
            Grow();
            ateThisTick = false;
        }

        //if the head moves past it's bounds it teleports to the other side
        if(rb.position.x < leftBound)
        {
            rb.position = new Vector2(rightBound, rb.position.y);
        }

        if(rb.position.x > rightBound)
        {
            rb.position = new Vector2(leftBound, rb.position.y);
        }

        if(Mathf.Abs(rb.position.y) > gameManager.verticalBound) 
        {
            rb.position = new Vector2(rb.position.x, -rb.position.y + Mathf.Sign(rb.position.y));

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //triggers when the other object has a fruit script component
        //quicker than using tags in my opinion lol
        if(collision.TryGetComponent<fruitBehavior>(out fruitBehavior fruit))
        {
            ateThisTick = true;
        }
    }


}
