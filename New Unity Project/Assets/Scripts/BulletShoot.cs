using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShoot : MonoBehaviour
{

    public GameObject Bullet_Emitter;
    public GameObject Bullet;
    public GameObject Bullet_Shooter;
    
    public float Bullet_forward_force;
    public float Bullet_Despawn_timer = 3.0f;
    private bool shotFired = false;
    public float FireDelay = 0.5f;

    IEnumerator waiter()    // Waits for some time before the bullet can be fired again
    {
        yield return new WaitForSeconds(FireDelay);
        shotFired = false;
    }

    void start()
    {
        Bullet_Shooter = this.gameObject;
    }

    // Update is called once per frame
    

    public void shoot()
    {
        if (shotFired == false)
        {
            GameObject Temporary_Bullet_Handler;
            Temporary_Bullet_Handler = Instantiate(Bullet, Bullet_Emitter.transform.position, Bullet_Emitter.transform.rotation) as GameObject;

            Temporary_Bullet_Handler.gameObject.GetComponent<Bullet>().Shooter = Bullet_Shooter;    // tracks what shot the bullet

            Rigidbody Temporary_RigidBody;
            Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();

            Temporary_RigidBody.AddForce(transform.forward * Bullet_forward_force);

            Destroy(Temporary_Bullet_Handler, Bullet_Despawn_timer);

            shotFired = true;

            StartCoroutine(waiter());
        }
    }
}