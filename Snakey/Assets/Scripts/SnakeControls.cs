using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
public class SnakeControls : MonoBehaviour
{

    List<Transform> tail = new List<Transform>();
    bool ateThisTick = false;
    public GameObject tailPrefab;
    public float tickTime;
    public float tickTimeScale;
    float tickTimer;
    Vector2 facing;
    Rigidbody2D rb;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        facing = Vector2.right;
        rb = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        tickTimer += Time.fixedDeltaTime * tickTimeScale;

        if (tickTimer > tickTime)
        {
            Move();
            tickTimer = 0;
        }

        if (Mathf.Abs(facing.x) > 0)
        {
            if (Input.GetButtonDown("up"))
            {
                facing = Vector2.up;
            }
            else if (Input.GetButtonDown("down"))
            {
                facing = Vector2.down;
            }
        }
        else
        {
            if (Input.GetButtonDown("right"))
            {
                facing = Vector2.right;
            }
            else if (Input.GetButtonDown("left"))
            {
                facing = Vector2.left;
            }
        }


    }

    void Move()
    {
        
        Vector2 gap = rb.position;
        

        rb.position += facing;

        if (ateThisTick)
        {
            GameObject tailSection = Instantiate(tailPrefab, gap, Quaternion.identity);
            tail.Insert(0, tailSection.transform);
            ateThisTick = false;
        }
        else if(tail.Count > 0)
        {
            //were moving it where the last square becomes the head and the rest shift down the list (it loops like a tread)
            tail.Last().position = gap;

            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count - 1);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.tag == "Player")
        {
            tickTimeScale = 0;
        }

        if(collision.tag == "fruit")
        {
            gameManager.score++;
            Destroy(collision.gameObject);
            ateThisTick = true;
        }

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            tickTimeScale = 0;
        }
    }
}
