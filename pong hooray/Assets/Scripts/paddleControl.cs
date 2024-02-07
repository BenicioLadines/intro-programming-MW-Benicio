using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paddleControl : MonoBehaviour
{

    public float moveSpeed;
    private float playerInput;
    private Rigidbody2D _rb;
    public float maxDistanceAllowed;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInput = Input.GetAxisRaw("Vertical");
        
    }

    private void FixedUpdate()
    {
        _rb.velocity = Vector2.up * playerInput * moveSpeed;
    }
}
