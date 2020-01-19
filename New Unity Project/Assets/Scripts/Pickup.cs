using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Powerup powerup;
    public GameObject pickupPrefab;
    public float spawnDelay;
    private float nextSpawnTime;
    private Transform tf;
    GameObject spawnedPickup;
    public GameObject SoundEffectObject;
    public GameManager gameManager;
    //private float SoundEffectVolume;

    private bool HasSpawned;

    // Start is called before the first frame update
    void Start()
    {
        tf = gameObject.GetComponent<Transform>();
        nextSpawnTime = Time.time + spawnDelay;
        gameManager = FindObjectOfType<GameManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        //Variable to store other object's PowerupController
        PowerupController powCon = other.GetComponent<PowerupController>();

        // if the other object has a PowerupController
        if (powCon != null)
        {
            powCon.Add(powerup);
            // Play powerup sound effect ny spawning an object to play the sound
            Instantiate(SoundEffectObject);
            //Destroy the powerup
            Destroy(gameObject);
        }
    }
}
