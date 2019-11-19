using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController2 : MonoBehaviour
{

    public enum AttackMode { Chase, Flee };
    public AttackMode attackMode;
    public Transform target;    ///
    private TankData data;
    private TankMotor motor;
    private Transform tf;

    public float FleeDistance = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        motor = gameObject.GetComponent<TankMotor>();
        data = gameObject.GetComponent<TankData>();
        tf = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attackMode == AttackMode.Chase)
        {
            //Rotate
            motor.RotateTowards(target.position, data.rotateSpeed);
            //move
            motor.move(data.moveSpeed);
        }
        if (attackMode == AttackMode.Flee)
        {
            // The vector from ai to target = target position - our position
            Vector3 vectorToTarget = target.position - tf.position;

            // flip by -1
            Vector3 vectorAwayFromTarget = -1 * vectorToTarget;

            // normalize
            vectorAwayFromTarget.Normalize();

            vectorAwayFromTarget *= FleeDistance;

            Vector3 fleePosition = vectorAwayFromTarget + tf.position;
            motor.RotateTowards(fleePosition, data.rotateSpeed);
            motor.move(data.moveSpeed);
        }
    }
}
