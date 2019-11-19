using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(TankMotor))]
public class AIController : MonoBehaviour
{
    public Transform[] waypoints;
    public float closeEnough = 1.0f;
    public enum LoopType { Stop, Loop, PingPong }
    public LoopType looptype;
    private int currentWaypoint = 0;
    public TankMotor motor;
    public TankData data;
    private bool isPatrolForward = true;
    private Transform tf;

    public void Awake()
    {
        tf = gameObject.GetComponent<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        motor = gameObject.GetComponent<TankMotor>();
        data = gameObject.GetComponent<TankData>();
    }

    // Update is called once per frame
    void Update()
    {
        if (motor.RotateTowards(waypoints[currentWaypoint].position, data.rotateSpeed))
        {
            // Do nothing!
        }
        else
        {
            // move forward
            motor.move(data.moveSpeed);
        }

        // If we're close enough to the waypoint
        if (Vector3.SqrMagnitude(waypoints[currentWaypoint].position - tf.position) < (closeEnough * closeEnough))
        {
            if (looptype == LoopType.Stop)
            {
                // Advance to the next waypoint, if we are still in range
                if (currentWaypoint < waypoints.Length - 1)
                {
                    currentWaypoint++;
                }
            }
            else if (looptype == LoopType.Loop)
            {
                //Advance to the next waypint
                if (currentWaypoint < waypoints.Length - 1)
                {
                    currentWaypoint++;
                }
                else
                {
                    currentWaypoint = 0;
                }
            }
            else if (looptype == LoopType.PingPong)
            {
                if (isPatrolForward)
                {
                    // Advance to the next waypoint, if we are still in range
                    if (currentWaypoint < waypoints.Length - 1)
                    {
                        currentWaypoint++;
                    }
                    else
                    {
                        //Otherwise reverse direction and decrement our current waypoint
                        isPatrolForward = false;
                        currentWaypoint--;
                    }
                }
                else
                {
                    //advance to next waypoint
                    if (currentWaypoint > 0)
                    {
                        currentWaypoint--;
                    }
                    else
                    {
                        //otherwise reverse direction
                        isPatrolForward = true;
                        currentWaypoint++;
                    }
                }
            }
        }
    }
}