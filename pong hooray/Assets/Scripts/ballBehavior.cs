using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballBehavior : MonoBehaviour
{
    private Rigidbody2D _rb;
    public float moveForce;
    private float xDirection;
    private float yDirection;
    private bool inPlay;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        _rb = GetComponent<Rigidbody2D>();
        Launch();
    }

    private void Update()
    {
        if (!inPlay)
        {
            transform.position = Vector3.zero;
            Launch();
        }
    }

    void Launch()
    {
        Vector3 direction = new Vector3(0, 0, 0);
        xDirection = Random.Range(0, 2);
        yDirection = Random.Range(0, 2);

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

        Debug.Log(direction.ToString());
        _rb.velocity = direction.normalized * moveForce;
        inPlay = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if(collision.name == "Right Wall")
        {
            Debug.Log("right");
            gameManager.Player1Scored();
        }

        if(collision.name == "Left Wall")
        {
            Debug.Log("left");
            gameManager.Player2Scored();
        }

        inPlay = false;
    }

}
