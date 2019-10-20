using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float Damage = 5;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")    // ensures the bullet does not collide with the player tank
        {
            Destroy(this.gameObject);   // Destroy bullet
            TankData TankDataScript = collision.gameObject.GetComponent<TankData>();    // gets the TankData from the object collided with
            TankDataScript.Health = TankDataScript.Health - Damage; // decreases HP
            if (TankDataScript.Health <= 0) // for when the tank's HP is 0
            {
                Destroy(collision.gameObject);
                GameManager.PlayerScore++;  // Sets score higher
                Debug.Log("Score: " + GameManager.PlayerScore); // Displays score in debug
            }
        }
    }
}