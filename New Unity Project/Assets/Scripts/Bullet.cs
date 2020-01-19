using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float Damage = 5;

    public GameObject Shooter;
    public GameManager gameManager;
    public AudioClip FireSound;
    public AudioClip HitSound;
    public AudioSource audioSource;
    public GameObject SoundEffectObject;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        audioSource.clip = FireSound;
        audioSource.Play();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject != Shooter)    // ensures the bullet does not collide with the player tank
        {
            if (collision.gameObject.tag != "Untagged")
            collision.gameObject.GetComponent<AudioSource>().Play();    // plays audiosource of getting hit
            
            if (collision.gameObject.tag != "Terrain")
            {
                Destroy(this.gameObject);   // Destroy bullet
                collision.gameObject.GetComponent<TankData>().Health = collision.gameObject.GetComponent<TankData>().Health - Damage;

                if (collision.gameObject.GetComponent<TankData>().Health <= 0) // for when the tank's HP is 0
                {
                    Destroy(collision.gameObject);
                    Shooter.GetComponent<TankData>().Score++;  // Sets score higher
                    Debug.Log("Score: " + Shooter.GetComponent<TankData>().Score); // Displays score in debug
                }
            }
        }
    }
}