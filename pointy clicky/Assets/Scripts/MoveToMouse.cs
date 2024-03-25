using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToMouse : MonoBehaviour
{

    [SerializeField] float speed;
    Vector3 target;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        target = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = transform.position.z;
        }

       if(transform.position != target)
        {
            rb.position = Vector3.MoveTowards(transform.position, target, speed);

        }

    }


}
