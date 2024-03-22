using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fruitBehavior : MonoBehaviour
{
    //reference to game manager
    GameManager gameManager;
    //true if fruit is for player 1, false if for player 2
    [SerializeField] bool player1Side;

    private void Start()
    {
        //finds game manager in scene
        gameManager = FindObjectOfType<GameManager>();
        //fruit starts at a random position
        RandomizePosition();
    }

    //teleports fruit to a new random position set based on the bounds in the game manager
    void RandomizePosition()
    {
        //initializes random x
        int randomX = 0;
        //gets random point between vertical bounds set in game manager
        int randomY = Random.Range(-gameManager.verticalBound + 1, gameManager.verticalBound - 1);
        //keeps the fruit from spawning on the other player's side
        if (player1Side)
        {
            //gets random point between the left bound and the middle of the screen
            randomX = Random.Range(-gameManager.horizontalBound + 1, -1);
        }
        else
        {
            //gets random point between right bound and middle of scren
            randomX = Random.Range(1, gameManager.horizontalBound - 1);
        }
        
        //sets fruit's new position
        transform.position = new Vector2(randomX, randomY);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //true if object collided with has snake head component
        if (collision.TryGetComponent<SnakeHeadBehavior>(out SnakeHeadBehavior snake))
        {
            //randomizes fruit's position
            RandomizePosition();
        }
    }
}
