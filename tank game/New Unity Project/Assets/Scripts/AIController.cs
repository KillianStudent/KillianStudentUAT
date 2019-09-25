using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(TankMotor))]
public class AIController : MonoBehaviour
{
    public Transform[] waypoints;
    public float closeEnough = 1.0f;

    private int currentWaypoint = 0;
    private TankMotor motor;
    private TankData data;

    // Start is called before the first frame update
    void Start()
    {
        motor = gameObject.GetComponent<TankMotor>();
        data = gameObject.GetComponent<TankData>();
    }

    // Update is called once per frame
    void Update()
    {
        if (motor.RotateToward(waypoints[currentWaypoint].position, )
        {
            // Nothing
        }
        else
        {
            motor.move(1.0f);
        }
        if (Vector3.SqrMagnitude(transform.position, waypoints[currentWaypoint].position - transform.position) <= (closeEnough * closeEnough))
        {
            if (currentWaypoint < waypoints.Length - 1)
            {
                currentWaypoint++;
            }
        }
    }
}
