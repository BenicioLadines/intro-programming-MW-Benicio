using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHeadBehavior : MonoBehaviour
{
    GameManager gameManager;
    Rigidbody2D rb;
    Vector2 facing;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.tickEvent.AddListener(TickUpdate);
        facing = Vector2.right;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("up"))
        {
            facing = Vector2.up;
        }

        if (Input.GetButtonDown("down"))
        {
            facing = Vector2.down;
        }

        if (Input.GetButtonDown("left"))
        {
            facing = Vector2.left;
        }

        if (Input.GetButtonDown("right"))
        {
            facing = Vector2.right;
        }

    }

    public void TickUpdate()
    {
        rb.position += facing;
    }
}
