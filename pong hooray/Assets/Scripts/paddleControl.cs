using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paddleControl : MonoBehaviour
{

    public float moveSpeed;
    private float playerInput;
    private Rigidbody2D _rb;
    public float maxDistanceAllowed;
    public bool firstPlayer;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (firstPlayer)
        {
            case true:
                Player1Control();
                break;
            case false:
                Player2Control();
                break;
        }
        
    }

    void Player1Control()
    {
        playerInput = Input.GetAxisRaw("VerticalP1");
    }

    void Player2Control()
    {
        playerInput = Input.GetAxisRaw("VerticalP2");
    }

    private void FixedUpdate()
    {
        _rb.velocity = Vector2.up * playerInput * moveSpeed;

        if(_rb.position.y > maxDistanceAllowed)
        {
            _rb.position = new Vector2(_rb.position.x, maxDistanceAllowed);
        }

        if(_rb.position.y < -maxDistanceAllowed)
        {
            _rb.position = new Vector2(_rb.position.x, -maxDistanceAllowed);
        }
    }
}
