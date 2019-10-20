using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float Damage = 5;

    void start()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);   // Destroy bullet
            TankData TankDataScript = collision.gameObject.GetComponent<TankData>();
            TankDataScript.Health = TankDataScript.Health - Damage;
            if (TankDataScript.Health <= 0)
            {
                Destroy(collision.gameObject);
                GameManager.PlayerScore++;
                Debug.Log("Score: " + GameManager.PlayerScore);
            }
        }
    }
}