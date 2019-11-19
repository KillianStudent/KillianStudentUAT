using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController3 : MonoBehaviour
{

    public Transform target;
    private TankMotor motor;
    private TankData data;
    private Transform tf;
    public int avoidanceStage = 0;
    public float avoidanceTime = 2.0f;
    private float exitTime;
    public enum AttackMode { Chase };
    public AttackMode attackMode;
    public float WallStop = 5.0f;

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
            if (avoidanceStage != 0)
            {
                DoAvoidance();
            }
            else
            {
                DoChase();
            }
        }
    }

    bool CanMove(float speed)
    {
        // Cast a ray forward in teh distance we sent in
        // if the Raycast hits something
        RaycastHit hit;
        if (Physics.Raycast (tf.position, tf.forward, out hit, speed))
        {
            if (!hit.collider.CompareTag("Player"))
            {
                return false;
            }
        }
        //otherwise
        return true;
    }

    void DoChase()
    {
        motor.RotateTowards(target.position, data.rotateSpeed);
        // checks if we can move 'data.movespeed' units away
        if (CanMove(WallStop))
        {
            motor.move(data.moveSpeed);
        }
        else
        {
            avoidanceStage = 1;
        }
    }
    void DoAvoidance()
    {
        if (avoidanceStage == 1)
        {
            //left rotation
            motor.Rotate(-1 * data.rotateSpeed);

            if (CanMove(data.moveSpeed))
            {
                avoidanceStage = 2;

                exitTime = avoidanceTime;
            }
        }
        else if (avoidanceStage == 2)
        {
            if (CanMove(WallStop))
            {
                exitTime -= Time.deltaTime;
                motor.move(data.moveSpeed);

                if (exitTime <= 0)
                {
                    avoidanceStage = 0;
                }
            }
        }
        else
        {

            avoidanceStage = 1;
        }
    }
 }
