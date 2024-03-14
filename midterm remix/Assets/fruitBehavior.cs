using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fruitBehavior : MonoBehaviour
{

    GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        RandomizePosition();
    }
    void RandomizePosition()
    {

        int randomX = Random.Range(-gameManager.horizontalBound + 1, gameManager.horizontalBound - 1);
        int randomY = Random.Range(-gameManager.verticalBound+ 1, gameManager.verticalBound-1);

        transform.position = new Vector2(randomX, randomY);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<SnakeHeadBehavior>(out SnakeHeadBehavior snake))
        {
            RandomizePosition();
        }
    }
}
