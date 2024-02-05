using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumperGuy : MonoBehaviour
{

    public enum playerState { grounded, airborne}
    public playerState currentState;
    private Rigidbody2D _rb;
    private Vector2 _directionalInput;
    public float walkSpeed;
    public float jumpPwr;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3.down * 1.1f));
    }

    // Update is called once per frame
    void Update()
    {
        _directionalInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
        if(_directionalInput.x < 0)
        {
            _rb.AddForce(Vector2.left * walkSpeed * Time.fixedDeltaTime);
        }
        if( _directionalInput.x > 0)
        {
            _rb.AddForce(Vector2.right * walkSpeed * Time.fixedDeltaTime);
        }

        if (Input.GetButtonDown("Jump") && currentState == playerState.grounded)
        {
            _rb.AddForce(Vector2.up * jumpPwr, ForceMode2D.Impulse);
        }

        if (Physics2D.Linecast(transform.position, transform.position + (Vector3.down * 1.1f), LayerMask.GetMask("Level Geometry")))
        {
            currentState = playerState.grounded;
        }
        else
        {
            currentState = playerState.airborne;
        }
        
    }

}
