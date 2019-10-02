using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TankData))]
public class TankMotor : MonoBehaviour
{
    public TankData data;
    public CharacterController CharacterController;
    public float MoveSpeed = 5.0f;
    public float RotateSpeed = 10.0f;
    public Transform tf;

    void start()
    {
        CharacterController = gameObject.GetComponent<CharacterController>();
        tf = gameObject.GetComponent<Transform>();
        data = gameObject.GetComponent<TankData>();
    }

 
    public void move(float speed)
    {
        Vector3 MoveVector = tf.forward * speed * Time.deltaTime;
        CharacterController.SimpleMove(MoveVector);
    }
    public void Rotate(float speed)
    {
        Vector3 rotateVector = tf.up * speed * Time.deltaTime;
    }
    public bool RotateTorward(Vector3 target, float rotateSpeed)
    {
        return true;
    }
}
