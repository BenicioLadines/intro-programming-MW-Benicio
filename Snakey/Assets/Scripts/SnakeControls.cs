using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class SnakeControls : MonoBehaviour
{

    List<Transform> tail = new List<Transform>();
    bool ateThisTick = false;
    public GameObject tailPrefab;
    public float tickTime;
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
        tickTimer += Time.deltaTime;

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            facing = Vector2.right;
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            facing = Vector2.left;
        }
        else if (Input.GetAxisRaw("Vertical") > 0)
        {
            facing = Vector2.up;
        }
        else if (Input.GetAxisRaw("Vertical") < 0)
        {
            facing = Vector2.down;
        }

        if (tickTimer > tickTime)
        {
            Move();
            tickTimer = 0;
        }


    }

    void Move()
    {
        Vector2 gap = transform.position;
        

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
        gameManager.score++;
        Destroy(collision.gameObject);
        ateThisTick = true;
    }
}
