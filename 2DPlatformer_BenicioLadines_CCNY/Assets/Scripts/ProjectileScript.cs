using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{

    Rigidbody2D rb;
    [SerializeField] float speed;

    [SerializeField] float projectileLifetime;
    float projectileCountdown;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        projectileCountdown = projectileLifetime;
    }

    // Update is called once per frame
    void Update()
    {
        projectileCountdown -= Time.deltaTime;

        if(projectileCountdown < 0 )
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector2.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag != "Player")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
       
    }
}
