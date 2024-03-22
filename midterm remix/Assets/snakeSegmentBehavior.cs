using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snakeSegmentBehavior : MonoBehaviour
{
    SnakeHeadBehavior head;

    private void Start()
    {
        //gets component from parent, was the most convenient way I could think of
        //to store the segments locally per player
        head = GetComponentInParent<SnakeHeadBehavior>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //the segments needed their own rigidbodies for this logic
        //my idea was that whenever you hit the ball you lose a segment
        //so I made Shrink() and CutOff() on the head take these objects' transforms
        //as a quick and easy passthrough reference to it. Otherwise the segment would need some way of
        //determining its place in the list, which is already handled by the head
        if (collision.gameObject.TryGetComponent<BallBehavior>(out BallBehavior ball))
        {
            head.Shrink(this.transform);
        }

        //CutOff makes it so when the player runs into themselves they don't die but instead lose all the segments
        //that came after the one they just bit off
        if(collision.gameObject.TryGetComponent<SnakeHeadBehavior>(out SnakeHeadBehavior snake))
        {
            head.CutOff(this.transform);
        }
    }


}
