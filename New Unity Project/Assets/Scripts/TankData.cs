using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankData : MonoBehaviour
{
    public Text ScoreDisplay;
    public Text LiveDisplay;

    public float MaxHealth = 20;
    public float Health = 20;
    public float moveSpeed = 200.0f;
    public float fireRate = 0.5f;
    public float reverseSpeed = 100.0f;
    public float rotateSpeed = 180.0f;
    public int Lives = 3;
    public int Score = 0;

    void Update()   // displays score and lives remaining
    {
        ScoreDisplay.text = "Score: " + Score;
        LiveDisplay.text = "Lives remaining: " + Lives;
    }
}