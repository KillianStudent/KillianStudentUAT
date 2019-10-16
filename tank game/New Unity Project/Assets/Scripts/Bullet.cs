using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    
    void start()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);   // Destroy bullet
            Destroy(collision.gameObject);  // Destroy tank
            GameManager.PlayerScore++;
        }
    }
}