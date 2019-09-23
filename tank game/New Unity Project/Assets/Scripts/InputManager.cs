using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    public enum InputScheme { WASD, arrowKey };
    public InputScheme input = InputScheme.WASD;
    public TankData data;
    public TankMotor motor;
    
    // Update is called once per frame
//    void Update()
//    {
//        float horizontalInput;
//        float verticalInput;
//        switch (input)
//        {
//            case InputScheme.arrowKey:  // for inputting with arrow keys
//                horizontalInput = Input.GetAxis("HorizontalArrows");
//                verticalInput = Input.GetAxis("VerticalArrows");
//                break;
//            case InputScheme.WASD:  // for inputting with WASD
//                horizontalInput = Input.GetAxis("horizontal WASD");
//               verticalInput = Input.GetAxis("vertical WASD");
//               break;
//        }
//  }
    void Update()
    {

        switch (input)
        {
            case InputScheme.arrowKey:
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    motor.move(data.moveSpeed);
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    motor.move(-data.moveSpeed);
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    motor.Rotate(data.rotateSpeed);
                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    motor.Rotate(-data.rotateSpeed);
                }
                break;
        }
    }
}