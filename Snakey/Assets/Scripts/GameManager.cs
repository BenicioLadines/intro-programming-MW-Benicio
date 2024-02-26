using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int score;
    public TextMeshProUGUI scoreText;
    public GameObject fruitPrefab;
    public Vector2 spawnBounds;
    public float waitToSpawnFruit;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        InvokeRepeating("SpawnFruit", waitToSpawnFruit, waitToSpawnFruit);
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = " " + score;
    }

    void SpawnFruit()
    {
        int xPos = (int) Random.Range(-spawnBounds.x, spawnBounds.x);
        int yPos = (int) Random.Range(-spawnBounds.y, spawnBounds.y);
        Instantiate(fruitPrefab,new Vector2(xPos, yPos),Quaternion.identity);
    }
}
