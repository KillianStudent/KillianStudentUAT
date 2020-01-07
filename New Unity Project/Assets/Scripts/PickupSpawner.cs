using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    public Powerup powerup;
    public GameObject pickupPrefab;
    public float spawnDelay = 5;
    private float nextSpawnTime;
    private Transform tf;
    GameObject spawnedPickup;

    public bool HasSpawned;

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
            Debug.Log("spawn time hit");
            // If it is there is nothing spawns
            if (HasSpawned == false)
            {
                if (Time.time > nextSpawnTime)
                {
                    // Spawn it and set the next time
                    spawnedPickup = Instantiate(pickupPrefab, tf.position, Quaternion.identity) as GameObject;
                    nextSpawnTime = Time.time + spawnDelay;
                    HasSpawned = true;
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
        PowerupController powCon = other.GetComponent<PowerupController>();

        // if the other object has a PowerupController
        if (powCon != null)
        {
            HasSpawned = false;
        }
    }
}
