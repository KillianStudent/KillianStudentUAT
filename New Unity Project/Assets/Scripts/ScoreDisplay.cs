using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{

    public Text scoreText;
    int score;

    void Start()
    {
        score = GetComponent<TankData>().Score;
        scoreText = GetComponent<Text>();
    }

    void Update()
    {
        scoreText.text = score.ToString();
    }
}