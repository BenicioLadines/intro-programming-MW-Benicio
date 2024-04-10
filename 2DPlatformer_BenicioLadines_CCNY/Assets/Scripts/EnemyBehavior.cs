using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{

    [SerializeField] int damage;
    public PlayerControl playerControlScript;
    [SerializeField] float moveSpeed;
    public Transform[] patrolPoints;
    int patrolDestination;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        EnemyMovement();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerControlScript.TakeDamage(damage);
        }
    }

    void EnemyMovement()
    {
        rb.position = 
            Vector3.MoveTowards(rb.position, patrolPoints[patrolDestination].position, moveSpeed);

        if(rb.position == (Vector2) patrolPoints[patrolDestination].position )
        {
            patrolDestination = (patrolDestination + 1) % patrolPoints.Length;
        }
    }
}
