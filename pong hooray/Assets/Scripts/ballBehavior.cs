using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballBehavior : MonoBehaviour
{
    private Rigidbody2D _rb;
    public float moveForce;
    private float xDirection;
    private float yDirection;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Vector3 direction = new Vector3(0, 0, 0);
        xDirection = Random.Range(0, 2);
        yDirection = Random.Range(0, 2);

        if(xDirection == 0)
        {
            direction.x = -1;
        }
        else
        {
            direction.x = 1;
        }

        if(yDirection == 0)
        {
            direction.y = -1;
        }
        else
        {
            direction.y = 1;
        }

        _rb.AddForce(direction * moveForce, ForceMode2D.Impulse);
    }
    /*
     * ／l、
     （ﾟ､ ｡７
      l、ﾞ~ヽ
      じしf_,)ノ MEOWMEOWMEOWMEOWMEOWMEOWME
     * 
     * */
    
}
