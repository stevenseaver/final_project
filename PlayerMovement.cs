using UnityEngine;
using System.IO.Ports;

public class PlayerMovement : MonoBehaviour
{ 
    public CharacterController2D controller;
    float horizontalMove = 0f;      //default state
    public float runSpeed = 40f;    //running speed
    bool jump = false;

    int jumpState = 0;

    SerialPort sp = new SerialPort("/dev/cu.usbserial", 115200);

    void Start()
    {
        //open up serial from STM32
        sp.Open();
        sp.WriteLine("1");  //write '1' to enable STM32 Serial Port
        sp.ReadTimeout = 1; //timeout to stabilize serial readout
    }

    // Update is called once per frame
    void Update()
    {
        //move horizontally
        float v = 1 * runSpeed;  //for button use: Input.GetAxisRaw("Horizontal") * runSpeed;
        horizontalMove = v;

        //jump
        //manual jump
        /*if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }*/
        //using serial
        if (sp.IsOpen)
        {
            try
            {
                jumpState = int.Parse(sp.ReadLine());
                print(jumpState);
                if (jumpState == 1)
                {
                    jump = true;
                }
                if (jumpState == 0)
                {
                    jump = false;
                }

            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }

    void FixedUpdate()
    {
        //move your char
        float runAhead = horizontalMove * Time.fixedDeltaTime;
        controller.Move(runAhead, false, jump);

        jump = false;
    }
}
