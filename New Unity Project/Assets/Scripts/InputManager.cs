using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    public enum InputScheme { WASD, arrowKey };
    public InputScheme input = InputScheme.WASD;
    public TankData data;
    public TankMotor motor;

    private void Start()
    {
        motor = gameObject.GetComponent<TankMotor>();
        data = gameObject.GetComponent<TankData>();
    }



    void Update()
    {
            switch (input)  // Movement
        {
            case InputScheme.arrowKey:
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    motor.move(data.moveSpeed);
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    motor.move(-data.reverseSpeed);
                }
                if (Input.GetKey(KeyCode.RightArrow))   // Rotation
                {
                    motor.Rotate(data.rotateSpeed);
                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    motor.Rotate(-data.rotateSpeed);
                }
                if (Input.GetKeyDown("space"))
                {
                    gameObject.GetComponent<BulletShoot>().shoot(); // shoots bullet
                }
                if (Input.GetKeyDown(KeyCode.H))    // self harm cheat
                {
                    data.Health = data.Health - 5;
                    if (data.Health <= 0)
                    {
                        motor.DestroyTank();
                    }
                }
                break;
            case InputScheme.WASD:
                if (Input.GetKey(KeyCode.W))
                {
                    motor.move(data.moveSpeed);
                }
                if (Input.GetKey(KeyCode.S))
                {
                    motor.move(-data.reverseSpeed);
                }
                if (Input.GetKey(KeyCode.D))   // Rotation
                {
                    motor.Rotate(data.rotateSpeed);
                }
                if (Input.GetKey(KeyCode.A))
                {
                    motor.Rotate(-data.rotateSpeed);
                }
                if (Input.GetKeyDown(KeyCode.F))
                {
                    gameObject.GetComponent<BulletShoot>().shoot(); // shoots bullet
                }
                if (Input.GetKeyDown(KeyCode.G))    // self harm cheat
                {
                    data.Health = data.Health - 5;
                    if (data.Health <= 0)
                    {
                        motor.DestroyTank();
                    }
                }
                break;
        }
    }
}