using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreTracker : MonoBehaviour
{
    public GameManager gameManager;
    public Text Player1Score;
    public Text Player2Score;
    public int RunHighScore;
    public int highScoreInt;
    public Text highScoreText;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        Player1Score.text = gameManager.Player1Score.ToString();
        Player2Score.text = gameManager.Player2Score.ToString();
        highScoreInt = PlayerPrefs.GetInt("HighScore");

        if (gameManager.Player1Score > gameManager.Player2Score)    // gets what the current high score is
        {
            RunHighScore = gameManager.Player1Score;
        }
        else // if both scores are the same or if player 2's score is higher (no difference if they're the same)
        {
            RunHighScore = gameManager.Player2Score;
        }

        if (RunHighScore > highScoreInt)    // checks if the run's high score is higher than the saved high score
        {
            PlayerPrefs.SetInt("HighScore", RunHighScore);  // sets the new high score
        }
        highScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString(); // displays the high score
    }

}