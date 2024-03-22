using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fruitBehavior : MonoBehaviour
{

    GameManager gameManager;
    [SerializeField] bool player1Side;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        //fruit starts at a random position
        RandomizePosition();
    }

    //teleports fruit to a new random position set based on the bounds in the game manager
    void RandomizePosition()
    {
        int randomX = 0;
        int randomY = Random.Range(-gameManager.verticalBound + 1, gameManager.verticalBound - 1);
        //keeps the fruit from spawning on the other player's side
        if (player1Side)
        {
            randomX = Random.Range(-gameManager.horizontalBound + 1, -1);
        }
        else
        {
            randomX = Random.Range(1, gameManager.horizontalBound - 1);
        }
        

        transform.position = new Vector2(randomX, randomY);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //position is randomized every time the fruit collides with the snake head
        if (collision.TryGetComponent<SnakeHeadBehavior>(out SnakeHeadBehavior snake))
        {
            RandomizePosition();
        }
    }
}
