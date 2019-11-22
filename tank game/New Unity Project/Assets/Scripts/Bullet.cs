using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float Damage = 5;

    public GameObject Shooter;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject != Shooter)    // ensures the bullet does not collide with the player tank
        {
            if (collision.gameObject.tag != "Terrain")
            {
                Destroy(this.gameObject);   // Destroy bullet
                collision.gameObject.GetComponent<TankData>().Health = collision.gameObject.GetComponent<TankData>().Health - Damage;

                if (collision.gameObject.GetComponent<TankData>().Health <= 0) // for when the tank's HP is 0
                {
                    Destroy(collision.gameObject);
                    GameManager.PlayerScore++;  // Sets score higher
                    Debug.Log("Score: " + GameManager.PlayerScore); // Displays score in debug
                }
            }
        }
    }
}