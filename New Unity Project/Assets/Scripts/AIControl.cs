using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControl : MonoBehaviour
{
    public Transform[] waypoints;
    private int currentWaypoint = 0;    // waypoints
    public float closeEnough = 1.0f;    // distance for waypoints to switch to next waypoint
    public enum AIState { Chase, ChaseAndFire, CheckForFlee, Flee, Rest, Patrol, };
    public AIState aiState = AIState.Chase;
    public float stateEnterTime;
    public float aiSenseRadius;
    public float restingHealRate;
    public int avoidanceStage = 0;
    public float avoidanceTime = 2.0f;
    public float WallStop = 5.0f;   // for AI navigation to avoid walls
    public float FleeDistance = 1.0f;
    public Transform Target;
    public TankData data;
    public TankMotor motor;
    public Transform tf;

    public float FOVAngle = 45;
    public float FOVDistance = 10;

    private float exitTime;

    void start()
    {
        motor = gameObject.GetComponent<TankMotor>();
        data = gameObject.GetComponent<TankData>();
        tf = gameObject.GetComponent<Transform>();
    }

    public void CheckForFlee()
    {

    }

    public void DoRest()
    {
        // Increase health, per second
        data.Health += restingHealRate * Time.deltaTime;

        //don't go over max HP
        data.Health = Mathf.Min(data.Health, data.MaxHealth);
    }

    public void ChangeState(AIState newState)
    {
        // Change state
        aiState = newState;

        // save time we changed states
        stateEnterTime = Time.time;
    }

    public bool CanSee()
    {
        Vector3 TargetDirection = Target.position - transform.position;
        float angle = Vector3.Angle(TargetDirection, transform.forward);

        if (angle < FOVAngle)
        {
            Debug.Log("CanSeePlayer");
            return true;
        }

        return false;
    }


    // Update is called once per frame
    void Update()
    {
        CanSee();
            if (aiState == AIState.Chase)
        {
            //perform behaviours
            if (avoidanceStage != 0)
            {
                DoAvoidance();
            }
            else
            {
                DoChase();
            }

            // Check for transitions
            if (data.Health < data.MaxHealth * 0.5f)
            {
                ChangeState(AIState.CheckForFlee);
            }
        }
        else if (aiState == AIState.ChaseAndFire)
        {
            //perform behaviors
            if (avoidanceStage != 0)
            {
                DoAvoidance();
            }
            else
            {
                DoChase();

                if (Time.time > gameObject.GetComponent<BulletShoot>().FireDelay)
                {
                    gameObject.GetComponent<BulletShoot>().shoot(); // shoots bullet
                }
            }
            //Check for transitions
            if (data.Health < data.MaxHealth * 0.5f)
            {
                ChangeState(AIState.CheckForFlee);
            }
            else if (Vector3.Distance(Target.position, tf.position) > aiSenseRadius)
            {
                ChangeState(AIState.Chase);
            }
        }
        else if (aiState == AIState.Flee)
        {
            //Perform behaviors
            if (avoidanceStage != 0)
            {
                DoAvoidance();
            }
            else
            {
                DoFlee();
            }

            if (Time.time >= stateEnterTime + 30)
            {
                ChangeState(AIState.CheckForFlee);
            }
        }
        else if (aiState == AIState.CheckForFlee)
        {
            CheckForFlee();

            if (Vector3.Distance(Target.position, tf.position) <= aiSenseRadius)
            {
                ChangeState(AIState.Flee);
            }
            else
            {
                ChangeState(AIState.Rest);
            }
        }
        else if (aiState == AIState.Rest)
        {
            DoRest();

            if (Vector3.Distance(Target.position, tf.position) < aiSenseRadius)
            {
                ChangeState(AIState.Flee);
            }
            else if (data.Health >= data.MaxHealth)
            {
                ChangeState(AIState.Chase);
            }
        }
        else if (aiState == AIState.Patrol)
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
            if (Vector3.SqrMagnitude(waypoints[currentWaypoint].position - tf.position) < (closeEnough * closeEnough))
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
        }
    }

    void DoChase()
    {
        motor.RotateTowards(Target.position, data.rotateSpeed);
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
    bool CanMove(float speed)
    {
        // Cast a ray forward in teh distance we sent in
        // if the Raycast hits something
        RaycastHit wallhit;
        if (Physics.Raycast(tf.position, tf.forward, out wallhit, WallStop))
        {
            if (!wallhit.collider.CompareTag("Player"))
            {
                return false;
            }
        }
        //otherwise
        return true;
    }
    void DoFlee()
        {
            // The vector from ai to target = target position - our position
            Vector3 vectorToTarget = Target.position - tf.position;

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
