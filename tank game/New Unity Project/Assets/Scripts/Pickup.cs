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

    // Start is called before the first frame update
    void Start()
    {
        tf = gameObject.GetComponent<Transform>();
        nextSpawnTime = Time.time + spawnDelay;
    }


    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawnTime)
        {
            // If it is there is nothing spawns
            if (spawnedPickup == null)
            {
                if (Time.time > nextSpawnTime)
                {
                    // Spawn it and set the next time
                    spawnedPickup = Instantiate(pickupPrefab, tf.position, Quaternion.identity) as GameObject;
                    nextSpawnTime = Time.time + spawnDelay;
                }
            }
            else
            {
                // Otherwise, the object still exists, so postpone the spawn
                nextSpawnTime = Time.time + spawnDelay;
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        //Variable to store other object's PowerupController
        PowerupController powCon = other.GetComponent<PowerupController>();

        // if the other object has a PowerupController
        if (powCon != null)
        {
            powCon.Add(powerup);
            // Play feedback (TODO)
            //if (feedback != null)
            //{
            //    AudioSource.PlayClipAtPoint(feedback, tf.position, 1.0f);
            //}
            //Destroy the powerup
            Destroy(gameObject);
        }
    }
}
