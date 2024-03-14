using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHeadBehavior : MonoBehaviour
{
    GameManager gameManager;
    Rigidbody2D rb;
    Vector2 facing;
    List<Transform> segments = new List<Transform>();
    public Transform segmentPrefab;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.tickEvent.AddListener(TickUpdate);
        segments.Add(this.transform);
        facing = Vector2.right;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("up") && facing != Vector2.down)
        {
            facing = Vector2.up;
        }

        if (Input.GetButtonDown("down") && facing != Vector2.up)
        {
            facing = Vector2.down;
        }

        if (Input.GetButtonDown("left") && facing != Vector2.right)
        {
            facing = Vector2.left;
        }

        if (Input.GetButtonDown("right") && facing != Vector2.left)
        {
            facing = Vector2.right;
        }

    }

    void Grow()
    {
        Transform segment = Instantiate(segmentPrefab, segments[segments.Count - 1].position,Quaternion.identity);
        segments.Add(segment);
    }

    public void TickUpdate()
    {

        for (int i = segments.Count - 1; i > 0; i--)
        {
            segments[i].position = segments[i - 1].position;
        }

        rb.position += facing;
        if(Mathf.Abs(rb.position.x) > gameManager.horizontalBound)
        {
            rb.position = new Vector2(-rb.position.x, rb.position.y);
        }
        if(Mathf.Abs(rb.position.y) > gameManager.verticalBound) 
        {
            rb.position = new Vector2(rb.position.x, -rb.position.y);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<fruitBehavior>(out fruitBehavior fruit))
        {
            Grow();
        }
    }
}
