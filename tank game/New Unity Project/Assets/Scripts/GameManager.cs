using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<GameObject> enemies = new List<GameObject>();
    public double PlayerScore = 0;

    void Awake() // called when the game starts
    {
        if (instance == null)   // if there is no Game Manager, creates one
        {
            instance = this;
        }
        else
        {
            Debug.LogError("ERROR: There can only be one GameMnanager");
            Destroy(gameObject);
        }
    }
    void update()
    {

    }
}

