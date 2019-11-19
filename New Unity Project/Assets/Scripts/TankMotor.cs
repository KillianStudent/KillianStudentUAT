using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TankData))]
public class TankMotor : MonoBehaviour
{
    public TankData data;
    public CharacterController CharacterController;
    public Transform tf;

    void start()
    {
        CharacterController = gameObject.GetComponent<CharacterController>();
        tf = gameObject.GetComponent<Transform>();
        data = gameObject.GetComponent<TankData>();
    }

 
    public void move(float speed)   // Moves tank forward
    {
        Vector3 MoveVector = tf.forward * speed * Time.deltaTime;
        CharacterController.SimpleMove(MoveVector); // Simple move
    }
    public void Rotate(float speed) // Rotate
    {
        tf.Rotate(Vector3.up * speed * Time.deltaTime);
    }
    public bool RotateTowards(Vector3 target, float rotateSpeed)
    {
        Vector3 vectorToTarget;

        vectorToTarget = target - tf.position;

        Quaternion targetRotation = Quaternion.LookRotation(vectorToTarget);
        tf.rotation = Quaternion.RotateTowards(tf.rotation, targetRotation, data.rotateSpeed * Time.deltaTime);
        if (targetRotation == tf.rotation)
        {
            return false;
        }
        return true;
    }
}
