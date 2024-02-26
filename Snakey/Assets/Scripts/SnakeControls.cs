using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SnakeControls : MonoBehaviour
{


    public float tickTime;
    float tickTimer;
    Vector2 facing;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tickTimer += Time.deltaTime;

        if(tickTimer > tickTime)
        {
            Move();
            tickTimer = 0;
        }
    }

    void Move()
    {
        if(Input.GetAxisRaw("Horizontal") > 0)
        {
            facing = Vector2.right;
        }
        else if(Input.GetAxis("Horizontal") < 0)
        {
            facing = Vector2.left;
        }
        else if(Input.GetAxisRaw("Vertical") > 0)
        {
            facing = Vector2.up;
        }
        else if(Input.GetAxisRaw("Vertical") < 0)
        {
            facing = Vector2.down;
        }

        transform.Translate(facing);

    }
}
