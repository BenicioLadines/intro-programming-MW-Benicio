using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    Rigidbody2D rb;
    [SerializeField]float playerSpeed;
    [SerializeField] float jumpPower;
    bool isJumping = false;
    [SerializeField] int maxHealth;
    [SerializeField] int currentHealth;
    HealthBarUI healthBarUI;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthBarUI = FindObjectOfType<HealthBarUI>();
        currentHealth = maxHealth;
        healthBarUI.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        Jump();
        healthBarUI.SetHealth(currentHealth);

    }

    void MovePlayer()
    {
        Vector3 newPos = transform.position;

        if(Input.GetKey(KeyCode.A))
        {
            newPos.x -= playerSpeed;
        }
        else if(Input.GetKey (KeyCode.D)) 
        {
            newPos.x += playerSpeed;
        }

        rb.position = newPos;
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            rb.AddForce(Vector3.up * jumpPower);
            isJumping = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Surface")
        {
            isJumping = false;
        }

        if(collision.gameObject.tag == "Lava")
        {
            TakeDamage(2);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBarUI.SetHealth(currentHealth);
    }
}
