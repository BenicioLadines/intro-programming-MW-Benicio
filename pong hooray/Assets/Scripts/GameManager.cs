using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    int player1Score;
    int player2Score;
    public TextMeshProUGUI player1ScoreText;
    public TextMeshProUGUI player2ScoreText;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        player1ScoreText.text = player1Score.ToString();

        player2ScoreText.text = player2Score.ToString();
    }

    public void Player1Scored() => player1Score++;

    public void Player2Scored() => player2Score++;
}
